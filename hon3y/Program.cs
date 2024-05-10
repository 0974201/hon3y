using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("test.txt", rollingInterval: RollingInterval.Hour)
    .CreateLogger();

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

    var app = builder.Build();

    //okay maar hoe sloop ik alles hier in sldkfjsldk

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
        options.MessageTemplate = "/* ------- */ \n {RemoteIpAddress} {RequestScheme}:{RequestHost}";

        //options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
            diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress);
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
