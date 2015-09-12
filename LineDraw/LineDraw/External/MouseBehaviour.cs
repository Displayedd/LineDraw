/*
 * Based upon online source found at
 * https://social.msdn.microsoft.com/Forums/vstudio/en-US/b59975f9-3039-42af-b6b6-9c6d17079d24/binding-mouse-position-in-mvvm-is-it-possible?forum=wpf.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LineDraw.External
{
    public class MouseBehaviour
    {
        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached("MouseUpCommand", typeof(ICommand), 
            typeof(MouseBehaviour), 
            new FrameworkPropertyMetadata(new PropertyChangedCallback(MouseUpCommandChanged)));

        private static void MouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;

            element.MouseUp += new MouseButtonEventHandler(element_MouseUp);
        }

        static void element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;

            ICommand command = GetMouseUpCommand(element);

            command.Execute(e);
        }

        public static void SetMouseUpCommand(UIElement element, ICommand value)
        {
            element.SetValue(MouseUpCommandProperty, value);
        }

        public static ICommand GetMouseUpCommand(UIElement element)
        {
            return (ICommand)element.GetValue(MouseUpCommandProperty);
        }
    }
}
