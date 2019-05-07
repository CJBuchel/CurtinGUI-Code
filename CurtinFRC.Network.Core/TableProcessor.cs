using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Hellosam.Net.Collections;
using NetworkTables.Tables;
using Serilog;

namespace DotNetDash
{
    public abstract class TableProcessor : IViewProcessor, INotifyPropertyChanged
    {
        protected readonly ITable baseTable;
        protected ObservableDictionary<string, ObservableCollection<IViewProcessor>> keyToMultiProcessorMap = new ObservableDictionary<string, ObservableCollection<IViewProcessor>>();
        protected readonly ILogger logger;

        private readonly IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories;

        private FrameworkElement view;

        protected TableProcessor(string name, ITable table, IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories)
        {
            logger = Log.ForContext(GetType()).ForContext("Table", name);
            logger.Information("Creating table processor for table");
            TableName = name;
            baseTable = table;
            this.processorFactories = processorFactories;
            InitCurrentSubTables();
            InitProcessorListener();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TableName { get; }

        public ObservableDictionary<string, ObservableCollection<IViewProcessor>> KeyToMultiProcessorMap
        {
            get { return keyToMultiProcessorMap; }
            set { keyToMultiProcessorMap = value; NotifyPropertyChanged(); }
        }

        public FrameworkElement View
        {
            get
            {
                if (view != null)
                {
                    return view;
                }
                else
                {
                    return view = GetBoundView();
                }
            }
            private set
            {
                view = value;
                TryBindView(value);
                NotifyPropertyChanged();
            }
        }

        public abstract string Name { get; }

        internal void AddViewProcessorToView(string name, IViewProcessor processor)
        {
            logger.Information($"Adding custom view processor {name} to the view");
            KeyToMultiProcessorMap.Add(name, new ObservableCollection<IViewProcessor> { processor });
        }


        protected FrameworkElement CreateSubTableHolder(string styleName)
        {
            logger.Information("Creating subtable holder");
            var content = new ItemsControl
            {
                Style = (Style)Application.Current.Resources[styleName],
                DataContext = this
            };
            content.SetBinding(ItemsControl.ItemsSourceProperty, nameof(KeyToMultiProcessorMap));
            return new ContentControl { Content = content };
        }

        protected virtual NetworkTableContext GetTableContext(string name, ITable table) => new NetworkTableContext(name, table);

        protected virtual string DefaultTableType => string.Empty;

        protected abstract FrameworkElement GetViewCore();

        private void AddProcessorOptionsForTable(string subTableName)
        {
            logger.Information($"Creating processors for subtable {subTableName}");
            var subTable = baseTable.GetSubTable(subTableName);
            var tableType = subTable.GetString("~TYPE~", DefaultTableType);
            logger.Information($"Subtable {subTableName} has a Dashboard type of '{tableType}'");
            var selectedProcessors = new ObservableCollection<IViewProcessor>(GetSortedTableProcessorsForType(subTable, subTableName, tableType));
            logger.Information($"Found {selectedProcessors.Count} applicable subprocessors for table '{subTableName}'");
            if (!keyToMultiProcessorMap.ContainsKey(subTableName))
            {
                keyToMultiProcessorMap.Add(subTableName, selectedProcessors);
            }
        }

        private FrameworkElement GetBoundView()
        {
            logger.Information("Creating bound view");
            var view = GetViewCore();
            TryBindView(view);
            return view;
        }

        protected IEnumerable<TableProcessor> GetSortedTableProcessorsForType(ITable table, string tableName, string tableType)
        {
            var matchedProcessorFactories = processorFactories.Where(factory => factory.Metadata.IsMatch(tableType)).ToList();
            matchedProcessorFactories.Sort((factory1, factory2) =>
            {
                if (factory1.Metadata.IsWildCard())
                    return factory2.Metadata.IsWildCard() ? 0 : 1;
                else if (factory2.Metadata.IsWildCard())
                    return factory1.Metadata.IsWildCard() ? 0 : -1;
                return 0;
            });
            return matchedProcessorFactories.Select(factory => factory.Value.Create(tableName, table));
        }

        private void InitCurrentSubTables()
        {
            foreach (var subTableName in baseTable.GetSubTables())
            {
                AddProcessorOptionsForTable(subTableName);
            }
        }

        private void InitProcessorListener()
        {
            baseTable.AddSubTableListenerOnSynchronizationContext(SynchronizationContext.Current,
                (table, newTableName, flags) => AddProcessorOptionsForTable(newTableName));
        }

        private void NotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void TryBindView(FrameworkElement view)
        {
            if (view != null)
            {

                view.DataContext = GetTableContext(TableName, baseTable);
            }
        }
    }
}
