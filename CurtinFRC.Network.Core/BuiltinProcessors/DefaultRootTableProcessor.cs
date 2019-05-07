using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using NetworkTables.Tables;

namespace DotNetDash.BuiltinProcessors
{
    public class DefaultRootTableProcessor : TableProcessor
    {
        public DefaultRootTableProcessor(string name, ITable table, IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories)
            : base(name, table, processorFactories)
        {
            KeyToMultiProcessorMap.Add(name,
                new ObservableCollection<IViewProcessor>(GetSortedTableProcessorsForType(table, name, name)));
        }

        protected override FrameworkElement GetViewCore()
        {
            return CreateSubTableHolder("RootTableStyle");
        }

        public override string Name => "Root Table Processor";
    }
    
    [DashboardType(typeof(IRootTableProcessorFactory), "")]
    public sealed class DefaultRootTableProcessorFactory : IRootTableProcessorFactory
    {
        private readonly IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories;

        [ImportingConstructor]
        public DefaultRootTableProcessorFactory([ImportMany] IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories)
        {
            this.processorFactories = processorFactories;
        }

        public TableProcessor Create(string subTable, ITable table)
        {
            return new DefaultRootTableProcessor(subTable, table, processorFactories);
        }
    }
}
