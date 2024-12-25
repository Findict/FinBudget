using FinBudget.App.Pages;
using FinBudget.App.ViewModels;
using FinBudget.Repository.Database;
using FinBudget.Repository.Processors;
using FinBudget.Repository.Processors.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FinBudget.App
{
    public static class MauiProgram
    {
        private static readonly string _dbPath = "FinBudget\\Database.db";
        private static readonly string _dbPathBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome.otf", "FontAwesome");
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            // Database Contexts
            builder.Services.AddDbContext<BudgetDbContext>(options => options.UseSqlite($"Data Source={Path.Combine(_dbPathBase, _dbPath)};"));
            
            // Database Services
            builder.Services.AddScoped<ICategoryProcessor, CategoryProcessor>();
            builder.Services.AddScoped<IAccountProcessor, AccountProcessor>();
            builder.Services.AddScoped<IFinancialTransactionProcessor, FinancialTransactionProcessor>();
            
            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<CategoriesPageViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<CategoriesPage>();

            return builder;
        }
    }
}
