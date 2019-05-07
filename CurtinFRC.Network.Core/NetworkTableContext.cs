using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NetworkTables.Tables;

namespace DotNetDash
{
    public class NetworkTableContext : INotifyPropertyChanged
    {
        private ITable table;

        public NetworkTableContext(string tableName, ITable table)
        {
            Name = tableName;
            this.table = table;
            Numbers = new NetworkTableBackedLookup<double>(table);
            Booleans = new NetworkTableBackedLookup<bool>(table);
            Strings = new NetworkTableBackedLookup<string>(table);
            Raw = new NetworkTableBackedLookup<IList<byte>>(table);
            StringArrays = new NetworkTableBackedLookup<IList<string>>(table);
            BooleanArrays = new NetworkTableBackedLookup<IList<bool>>(table);
            NumberArrays = new NetworkTableBackedLookup<IList<double>>(table);
        }

        public string Name { get; }

        public NetworkTableBackedLookup<double> Numbers { get; }

        public NetworkTableBackedLookup<bool> Booleans { get; }

        public NetworkTableBackedLookup<string> Strings { get; }

        public NetworkTableBackedLookup<IList<byte>> Raw { get; }

        public NetworkTableBackedLookup<IList<string>> StringArrays { get; }

        public NetworkTableBackedLookup<IList<bool>> BooleanArrays { get; }

        public NetworkTableBackedLookup<IList<double>> NumberArrays { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}