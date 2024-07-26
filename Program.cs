using BlazorApp10.Components;
using BlazorApp10.Services;
using Radzen;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSignalR(); // Add SignalR
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<Radzen.ThemeService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.ContextMenuService>();
builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<IWebService, WebService>();
builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
//app.UseRouting();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapBlazorHub(); // Blazor Hub ╦егн
//    endpoints.MapFallbackToPage("/_Host");
//});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
