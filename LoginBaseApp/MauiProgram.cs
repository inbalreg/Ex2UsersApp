using LoginBaseApp.Service;
using Microsoft.Extensions.Logging;

namespace LoginBaseApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MaterialSymbolsOutlined.ttf","MaterialSymbols");

					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<Views.LoginPage>();
            builder.Services.AddTransient<ViewModels.LoginPageViewModel>();

			builder.Services.AddSingleton<ILoginService, DBMokup>();




#if DEBUG
			builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
