using System;
using System.ComponentModel.Composition;

namespace DotNetDash
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class CustomViewFactoryAttribute : ExportAttribute, ICustomViewFactoryMetadata
    {
        public CustomViewFactoryAttribute()
            :base(typeof(IViewProcessorFactory))
        {
        }

        public string Name { get; set; }
    }
}
