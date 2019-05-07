using System;
using System.Diagnostics;
using System.Threading;
using NetworkTables;
using NetworkTables.Tables;

namespace DotNetDash
{
    public static class NetworkTablesExtensions
    {
        public static void AddSubTableListenerOnSynchronizationContext(this ITable table, SynchronizationContext context, Action<ITable, string, NotifyFlags> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            table.AddSubTableListener((tbl, name, _, flags) =>
            {
                if (context != null)
                {
                    context.Post(state => callback(tbl, name, flags), null);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(state => callback(tbl, name, flags), null);
                }
            });
        }

        public static void AddTableListenerOnSynchronizationContext(this ITable table, SynchronizationContext context, Action<ITable, string, Value, NotifyFlags> callback, bool immediateNotify = false)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            table.AddTableListener((tbl, name, value, flags) =>
            {
                if (context != null)
                {
                    context.Post(state => callback(tbl, name, value, flags), null);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(state => callback(tbl, name, value, flags), null);
                }
            }, immediateNotify);
        }

        public static void AddTableListenerOnSynchronizationContext(this ITable table, SynchronizationContext context, Action<ITable, string, Value, NotifyFlags> callback, NotifyFlags flags)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }
            table.AddTableListenerEx((tbl, name, value, _flags) =>
            {
                if (context != null)
                {
                    context.Post(state => callback(tbl, name, value, _flags), null);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(state => callback(tbl, name, value, _flags), null);
                }
            }, flags);
        }

        public static void AddGlobalConnectionListenerOnSynchronizationContext(SynchronizationContext context, Action<IRemote, ConnectionInfo, bool> callback, bool notifyImmediate)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            NetworkTable.AddGlobalConnectionListener((remote, info, connected) =>
            {
                if (context != null)
                {
                    context.Post(state => callback(remote, info, connected), null);
                }
                else
                {
                    ThreadPool.QueueUserWorkItem(state => callback(remote, info, connected), null);
                }
            }, notifyImmediate);
        }
    }
}
