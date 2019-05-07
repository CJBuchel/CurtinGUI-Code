using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DotNetDash
{
    public class XamlView : ContentControl
    {
        public static readonly DependencyProperty DashboardTypeProperty = DependencyProperty.Register(nameof(DashboardType), typeof(string), typeof(XamlView));

        public string DashboardType
        {
            get
            {
                return (string)GetValue(DashboardTypeProperty);
            }
            set
            {
                SetValue(DashboardTypeProperty, value);
            }
        }
    }
}
