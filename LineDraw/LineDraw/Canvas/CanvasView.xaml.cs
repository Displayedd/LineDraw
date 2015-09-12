using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LineDraw.Canvas
{
    /// <summary>
    /// Interaction logic for CanvasView.xaml
    /// </summary>
    public partial class CanvasView : Window
    {
        /// <summary>
        /// Instatiate a new instance of this class.
        /// </summary>
        /// <param name="viewmodel">DataContext for this window.</param>
        public CanvasView(CanvasViewModel viewmodel)
        {
            this.DataContext = viewmodel;
            InitializeComponent();
        }
    }
}
