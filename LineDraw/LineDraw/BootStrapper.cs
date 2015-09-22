using LineDraw.Canvas;
using LineDraw.Interfaces;
using LineDraw.Misc;
using LineDraw.Models;
using LineDraw.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LineDraw
{
    /// <summary>
    /// This class is used for initializing application dependecies 
    /// such as Inversion of control container. Inspired by the
    /// UnityBootStrapper used in the Prism framework.
    /// </summary>
    public class BootStrapper
    {
        DependencyObject Shell { get; set; }
        IUnityContainer Container { get; set; }

        /// <summary>
        /// Initialize inversion of control container.
        /// </summary>
        protected virtual void InitContainer()
        {
            this.Container = new UnityContainer();
            
            this.Container.RegisterType<ILineCalculator, AStarLineCalculator>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ICanvasModel, GraphCanvasModel>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<ILineService, LineService>(new ContainerControlledLifetimeManager());

            // Types not explicitly registered are auto-discovered by Unity.
        }

        /// <summary>
        ///Initialize main window.
        /// </summary>
        protected virtual void InitializeShell()
        {
            this.Shell = this.Container.Resolve<CanvasView>();

            Application.Current.MainWindow = (CanvasView)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Run the bootstrapper logic.
        /// </summary>
        public void Run()
        {
            this.InitContainer();
            this.InitializeShell();
        }
    }
}
