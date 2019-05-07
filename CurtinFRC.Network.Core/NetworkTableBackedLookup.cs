using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NetworkTables;
using NetworkTables.Tables;

namespace DotNetDash
{
    public sealed class NetworkTableBackedLookup<T> : INotifyPropertyChanged
    {
        private readonly ITable table;

        public NetworkTableBackedLookup(ITable table)
        {
            bool validType = Value.GetSupportedValueTypes().Contains(typeof(T));
            
            if (!validType)
            {
                throw new InvalidOperationException($"Generic type {typeof(T)} is not supported");
            }

            this.table = table;
            table.AddTableListenerOnSynchronizationContext(SynchronizationContext.Current,
                (changedTable, key, value, flags) =>
                    NotifyPropertyChanged(System.Windows.Data.Binding.IndexerName));
        }

        public T this[string key]
        {
            get
            {
                var value = table.GetValue(key, null);
                if (value == null) return default(T);
                try
                {
                    return (T)value.GetObjectValue();
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }
            set
            {
                var val = Value.MakeValue(value);
                if (val == null) return;
                table.PutValue(key, val);
                NotifyPropertyChanged(System.Windows.Data.Binding.IndexerName);
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
