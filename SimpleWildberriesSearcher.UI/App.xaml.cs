using Microsoft.Extensions.DependencyInjection;
using SimpleWildberriesSearcher.Core.Services.ExportService;
using SimpleWildberriesSearcher.Core.Services.SearchService;
using System;
using System.Windows;

namespace SimpleWildberriesSearcher.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        #region Constructors
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        #endregion

        #region Assistants
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();

            services.AddScoped<ISearchService, WildberriesSearchService>();
            services.AddScoped<IExportService, ExcelExportService>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
        #endregion
    }
}
