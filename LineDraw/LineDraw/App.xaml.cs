using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LineDraw
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // We use a bootstrapper to initilize application dependencies.
            BootStrapper bootStrapper = new BootStrapper();
            bootStrapper.Run();

            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
