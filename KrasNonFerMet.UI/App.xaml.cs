using System;
using System.Windows;
using KrasNonFerMet.Core;
using KrasNonFerMet.DAL.Files.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KrasNonFerMet.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // MainWindow mainView = new MainWindow();
            // mainView.Show();
            ServiceProvider.GetService<MainWindow>().Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddScoped<IExcelReader, ExcelReader>();
            services.AddScoped<SimpleScheduler>();
        }
    }
}
