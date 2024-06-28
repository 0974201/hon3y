using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hon3y.Data;
using hon3y.Services;
using Microsoft.Data.Sqlite;
using Serilog;
using Serilog.Events;
using System.Data;

namespace hon3y
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //clientside validation uitzetten zodat het makkelijker wordt om sql injecties uit te voeren
            services.AddRazorPages().AddViewOptions(options => 
            {
                options.HtmlHelperOptions.ClientValidationEnabled = false;
            });

            services.AddHttpContextAccessor();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedForHeaderName = "X-Coming-From";
            });

            services.AddTransient<DataService>();

            //connectie met de database
            services.AddScoped<IDbConnection>(conn =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection");
                return new SqliteConnection(connString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //configuratie voor de logger
            app.UseSerilogRequestLogging(options =>
            {
                options.MessageTemplate = "/* ------- */ {RemoteIpAddress} {RequestScheme}:{RequestHost} /* ------------ */ \n HTTP Headers: {Headers}";

                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress); //logt ip adres
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value); //logt wat er opgevraagd wordt
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("Headers", httpContext.Request.Headers);
                };
            });

            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}