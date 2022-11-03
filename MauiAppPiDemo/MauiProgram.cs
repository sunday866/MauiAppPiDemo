using Microsoft.AspNetCore.Components.WebView.Maui;
using MauiAppPiDemo.Data;
using MauiAppPiDemo.Base;
using MauiAppPiDemo.Service;

namespace MauiAppPiDemo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
        builder.Services.AddSingleton<ILocalStorgeService, SecureStorageService>();
        builder.Services.AddMasaBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddAppSettings();
        return builder.Build();
    }

    private static IServiceCollection AddAppSettings(this IServiceCollection services)
    {
        //var assembly = Assembly.GetExecutingAssembly();
        //var resName = assembly.GetManifestResourceNames()?.FirstOrDefault(r =>
        //    r.EndsWith("appsettings.json", StringComparison.OrdinalIgnoreCase));
        //using var file = assembly.GetManifestResourceStream(resName);
        //using var sr = new StreamReader(file);
        //var json = sr.ReadToEnd();
        //file.Dispose();
        //file.Close();
        services.AddSingleton(new AgentAppSettings
        {
            BaseApiUrl = "http://101.34.1.127:18083/api/v5",
        });

        return services;
    }
}
