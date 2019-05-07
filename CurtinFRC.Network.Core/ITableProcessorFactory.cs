using NetworkTables.Tables;

namespace DotNetDash
{
    public interface ITableProcessorFactory
    {
        TableProcessor Create(string subTable, ITable table);
    }
}
