using System;
using System.ComponentModel.Composition;

namespace DotNetDash
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class DashboardTypeAttribute : ExportAttribute, IDashboardTypeMetadata
    {
        public DashboardTypeAttribute(Type exportType, string type)
            :base(exportType)
        {
            Type = type;
        }

        public string Type { get; }
    }
}
