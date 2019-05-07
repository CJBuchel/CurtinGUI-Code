using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Markup;
using NetworkTables.Tables;
using Serilog;

namespace DotNetDash.BuiltinProcessors
{
    /// <summary>
    /// This factory attempts to load a XAML document from the Extensions subdirectories and use a processor on that to create the view.
    /// If this a suitable XAML file is not found, then it will fallback to a processor that just creates text boxes with labels.
    /// </summary>
    [DashboardType(typeof(ITableProcessorFactory), "")]
    public sealed class FallbackProcessorFactory : ITableProcessorFactory
    {
        private readonly IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories;
        private IXamlSearcher searcher;
        private ILogger logger;
        
        [ImportingConstructor]
        public FallbackProcessorFactory(IXamlSearcher searcher, [ImportMany] IEnumerable<Lazy<ITableProcessorFactory, IDashboardTypeMetadata>> processorFactories)
        {
            this.searcher = searcher;
            this.processorFactories = processorFactories;
            logger = Log.ForContext<FallbackProcessorFactory>();
        }

        public TableProcessor Create(string subTable, ITable table)
        {
            var xamlViews = LoadXamlDocs();
            var matchingView = xamlViews.FirstOrDefault(view => view.DashboardType == table.GetString("~TYPE~", ""));
            return matchingView != null ? CreateProcessorForFirstView(subTable, table, matchingView) :
                (TableProcessor)new DefaultProcessor(subTable, table, processorFactories);
        }

        private XamlProcessor CreateProcessorForFirstView(string subTable, ITable table, XamlView view)
        {
            return new XamlProcessor(subTable, table, processorFactories, view);
        }

        private IEnumerable<XamlView> LoadXamlDocs()
        {
            var views = new List<XamlView>();
            foreach (var stream in searcher.GetXamlDocumentStreams())
            {
                try
                {
                    using (stream)
                    {
                        views.Add((XamlView)XamlReader.Load(stream));
                    }
                }
                catch (Exception ex)
                {
                    logger.Warning(ex, "Failed to load XAML view.");
                }
            }
            return views;
        }
    }
}
