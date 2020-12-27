using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace MachineScheduler.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            // MainWindow mainView = new MainWindow();
           //  mainView.Show();

            if(e.Args.Length > 0)
            {
                ParseStartupArgs(e.Args);
            }

            MainWindow mainWindow = ServiceProvider.GetService<MainWindow>();
            MainWindow = mainWindow;
            mainWindow.Show();
            
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            
           // services.AddScoped<IExcelReader, ExcelReader>();
            //services.AddScoped<SimpleScheduler>();
        }
        
        private void ParseStartupArgs(string[] args)
        {
            Dictionary<string, string> argsShortcut = new Dictionary<string, string>
            {
                {"-c", "StartupConsignmentsPath"},
                {"-m", "StartupMachinesPath"},
                {"-n", "StartupNomenclaturesPath"},
                {"-o", "StartupOperationsPath"}
            };

            for (int i = 0; i < args.Length - 1; i++)
            {
                MachineScheduler.UI.Properties.Settings.Default[argsShortcut[args[i]]] = args[++i];
            }            
        }
    }
}
