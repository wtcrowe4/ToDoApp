using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using ToDoApp.DataServices;
using ToDoApp.Views;

namespace ToDoApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IRestDataService, RestDataService>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<NewList>();
        builder.Services.AddSingleton<ViewList>();

        return builder.Build();
    }
}
