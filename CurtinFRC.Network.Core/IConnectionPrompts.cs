using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDash
{
    public interface IConnectionPrompts
    {
        int? PromptTeamNumber();
        string PromptServerName();
    }
}
