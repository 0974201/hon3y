using hon3y.Data;
using hon3y.Services;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/test-.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

Log.Information("Starting up...");

try
{
    Log.Information("Testing");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    //log connections

    builder.Services.AddSerilog(); //logging
    builder.Services.AddRazorPages();
    builder.Services.AddHttpContextAccessor();
    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedForHeaderName = "X-Coming-From";
    });
    builder.Services.AddTransient<DataService>();

    IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

   // builder.Services.AddDbContext<FormulierenContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true"));

    var app = builder.Build();

    //Database
    DbInit dbInit = new DbInit(configuration);
    dbInit.CreateDatabase(); //maakt database voor honeypot aan
    dbInit.CreateTables(); //maakt de tabels voor de database aan
    dbInit.CreateLogsDatabase(); //maakt de database voor de logs aan
    dbInit.CreateLogDBTable(); //maakt de tabel voor de log database aan

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    //app.Logger();
    // https://stackoverflow.com/questions/72940591/how-to-display-clientip-in-logs-using-serilog-in-net-core

    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "/* ------- */ {RemoteIpAddress} {RequestScheme}:{RequestHost} /* ------------ */ \n HTTP Headers: {Headers}";

        //options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("Headers", httpContext.Request.Headers);
        };
    });



    app.UseForwardedHeaders();
    //app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Something went wrong");
}
finally
{
    Log.CloseAndFlush();
}