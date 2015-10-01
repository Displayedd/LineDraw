/*
 * https://tdanemar.wordpress.com/2009/11/15/using-the-visualstatemanager-with-the-model-view-viewmodel-pattern-in-wpf-or-silverlight/
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LineDraw.External
{
    public class StateManager : DependencyObject
    {
        public static string GetVisualStateProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(VisualStatePropertyProperty);
        }

        public static void SetVisualStateProperty(DependencyObject obj, string value)
        {
            obj.SetValue(VisualStatePropertyProperty, value);
        }

        public static readonly DependencyProperty VisualStatePropertyProperty =
            DependencyProperty.RegisterAttached(
            "VisualStateProperty",
            typeof(string),
            typeof(StateManager),
            new PropertyMetadata((s, e) =>
            {
                var propertyName = (string)e.NewValue;
                if (string.IsNullOrEmpty(propertyName))
                    return;

                if (s is FrameworkElement)
                    System.Windows.VisualStateManager.GoToElementState(s as FrameworkElement, propertyName, true);
                else if(s is Control)
                    System.Windows.VisualStateManager.GoToState(s as Control, propertyName, true);
                else
                    throw new InvalidOperationException("This attached property only supports types derived from Control or FrameworkElement.");
            }));
    }
}
