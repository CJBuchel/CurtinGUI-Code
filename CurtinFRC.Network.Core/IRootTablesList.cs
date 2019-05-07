using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDash
{
    public interface IRootTablesList
    {
        IEnumerable<string> RootTables { get; }
    }
}
