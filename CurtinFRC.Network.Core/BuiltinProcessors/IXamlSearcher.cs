using System.Collections.Generic;
using System.IO;

namespace DotNetDash.BuiltinProcessors
{
    public interface IXamlSearcher
    {
        IEnumerable<Stream> GetXamlDocumentStreams();
    }
}