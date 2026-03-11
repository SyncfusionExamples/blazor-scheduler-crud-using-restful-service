using SfSchedulerApp.Components;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();










// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using SfSchedulerApp.Components;
// using Syncfusion.Blazor;
// // using Syncfusion.Licensing; // uncomment if you register a Syncfusion license

// var builder = WebApplication.CreateBuilder(args);

// // 1) Blazor Web App services (Interactive Server render mode)
// builder.Services.AddRazorComponents()
//     .AddInteractiveServerComponents();

// // 2) Antiforgery services (recommended to be explicit in .NET 8+)
// builder.Services.AddAntiforgery();

// // 3) Syncfusion services (remove if not using Syncfusion components)
// builder.Services.AddSyncfusionBlazor();

// var app = builder.Build();

// // 4) Syncfusion license (optional)
// // SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// // If you use authentication/authorization, they should go here:
// // app.UseAuthentication();
// // app.UseAuthorization();

// // 5) **Antiforgery middleware** must run after auth and before endpoints
// app.UseAntiforgery(); // <-- required for Blazor SSR forms/endpoints in .NET 8+

// // 6) Map Razor Components (App.razor is the host – no _Host.cshtml)
// app.MapRazorComponents<App>()
//    .AddInteractiveServerRenderMode();

// app.Run();  