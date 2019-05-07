using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDash
{
    [Export]
    public class NetworkTablesRoot : INotifyPropertyChanged
    {
        private IEnumerable<Lazy<IRootTableProcessorFactory, IDashboardTypeMetadata>> rootTableFactories;
        private INetworkTablesInterface ntInterface;
        private IRootTablesList rootTablesList;

        [ImportingConstructor]
        public NetworkTablesRoot(
            [Import] INetworkTablesInterface ntInterface,
            [Import] IRootTablesList rootTablesList,
            [Import] IConnectionPrompts prompts,
            [ImportMany] IEnumerable<Lazy<IRootTableProcessorFactory, IDashboardTypeMetadata>> rootTableFactories,
            [ImportMany] IEnumerable<Lazy<IViewProcessorFactory, ICustomViewFactoryMetadata>> customViewFactories)
        {
            this.rootTableFactories = rootTableFactories;
            this.ntInterface = ntInterface;
            this.rootTablesList = rootTablesList;

            foreach (var factory in customViewFactories)
            {
                CustomViews.Add(new CustomViewEntry(factory.Metadata.Name, new Command(() =>
                    CurrentlyViewedRootTable.AddViewProcessorToView($"{factory.Metadata.Name}_{Guid.NewGuid()}", factory.Value.Create()))));
            }

            ntInterface.OnClientConnectionAttempt += (obj, args) => SetupRootTables();
            ntInterface.OnConnectionStatus += (obj, args) => Connected = args.Connected;
            ntInterface.OnDisconnect += (obj, args) => Connected = false;

            ConnectToRoboRioCommand = new Command(() =>
            {
                var team = prompts.PromptTeamNumber();
                if (team != null)
                {
                    ntInterface.Disconnect();
                    ntInterface.ConnectToTeam(team.Value);
                }
            });

            ConnectToServerCommand = new Command(() =>
            {
                var server = prompts.PromptServerName();
                if (server != null)
                {
                    ntInterface.Disconnect();
                    ntInterface.ConnectToServer(server);
                }
            });

            SetupRootTables();
        }

        private void SetupRootTables()
        {
            RootTables.Clear();
            foreach (var rootTableName in rootTablesList.RootTables)
            {
                var matchedProcessors = rootTableFactories.Where(factory => factory.Metadata.IsMatch(rootTableName));
                var processor = (matchedProcessors.FirstOrDefault(factory => !factory.Metadata.IsWildCard()) ?? matchedProcessors.First())
                                      .Value.Create(rootTableName, ntInterface.GetTable(rootTableName));
                RootTables.Add(processor);
            }
        }

        public ObservableCollection<TableProcessor> RootTables { get; } = new ObservableCollection<TableProcessor>();
        public ObservableCollection<CustomViewEntry> CustomViews { get; } = new ObservableCollection<CustomViewEntry>();

        public Command ConnectToRoboRioCommand { get; }
        public Command ConnectToServerCommand { get; }

        private TableProcessor currentlyViewedRootTable;

        public TableProcessor CurrentlyViewedRootTable
        {
            get { return currentlyViewedRootTable; }
            set { currentlyViewedRootTable = value; NotifyPropertyChanged(); }
        }

        private bool connected;

        public bool Connected
        {
            get { return connected; }
            set { connected = value; NotifyPropertyChanged(); }
        }
        
        private void NotifyPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class CustomViewEntry
    {
        public CustomViewEntry(string name, Command command)
        {
            Name = name;
            Command = command;
        }

        public string Name { get; }
        public Command Command { get; }
    }
}
