var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//log connections

builder.WebHost.ConfigureKestrel((context, serverOptions) =>
{
    var section = context.Configuration.GetSection("Kestrel");

    serverOptions.ListenLocalhost(8000, listenOptions =>
    {
        listenOptions.UseConnectionLogging();
    });
});


builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
