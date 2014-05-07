using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;



namespace SASolutions
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) 
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;

            SASolutionsBootstrapper bootstrapper = new SASolutionsBootstrapper();
            bootstrapper.Run();
        }
    }

}
