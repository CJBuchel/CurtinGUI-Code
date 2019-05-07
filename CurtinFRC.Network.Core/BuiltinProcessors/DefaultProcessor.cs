using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using NetworkTables;
using NetworkTables.Tables;

namespace DotNetDash.BuiltinProcessors
{
    class DefaultProcessor : TableProcessor
    {
        public DefaultProcessor(string name, ITable table, IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories)
            : base(name, table, processorFactories)
        {
            baseTable.AddTableListenerOnSynchronizationContext(SynchronizationContext.Current, (sendingTable, key, value, flags) =>
            {
                if (key == "~TYPE~") return;
                logger.Verbose($"Adding property view for {name}");
                var stackPanel = (StackPanel)View;
                stackPanel.Children.Add(CreateNewElementView(key, value));
            }, NotifyFlags.NotifyImmediate | NotifyFlags.NotifyNew);
        }

        protected override FrameworkElement GetViewCore()
        {
            var panel = new StackPanel { Orientation = Orientation.Vertical };
            panel.Children.Add(new TextBlock { Text = TableName, HorizontalAlignment = HorizontalAlignment.Center });
            return panel;
        }

        public override string Name => "Default Table View";

        private UIElement CreateNewElementView(string key, Value value)
        {
            var keyValueLine = new StackPanel { Orientation = Orientation.Horizontal };
            keyValueLine.Children.Add(new Label { Content = key, VerticalAlignment = VerticalAlignment.Center });
            
            var typeCategory = DetermineValueNetworkType(value);
            var bindingPath = $"{typeCategory}[{key}]";
            if (value.Type != NtType.Boolean)
            {
                var valueBox = new TextBox { VerticalAlignment = VerticalAlignment.Center };
                valueBox.SetBinding(TextBox.TextProperty, bindingPath);
                keyValueLine.Children.Add(valueBox); 
            }
            else
            {
                var checkBox = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
                checkBox.SetBinding(CheckBox.IsCheckedProperty, bindingPath);
                keyValueLine.Children.Add(checkBox);
            }
            return keyValueLine;
        }

        private static string DetermineValueNetworkType(Value value)
        {
            switch (value.Type)
            {
                case NtType.Boolean:
                    return nameof(NetworkTableContext.Booleans);
                case NtType.Double:
                    return nameof(NetworkTableContext.Numbers);
                case NtType.String:
                    return nameof(NetworkTableContext.Strings);
                case NtType.BooleanArray:
                    return nameof(NetworkTableContext.BooleanArrays);
                case NtType.DoubleArray:
                    return nameof(NetworkTableContext.NumberArrays);
                case NtType.StringArray:
                    return nameof(NetworkTableContext.StringArrays);
                case NtType.Raw:
                    return nameof(NetworkTableContext.Raw);
                default:
                    return string.Empty;
            }
        }
    }
}