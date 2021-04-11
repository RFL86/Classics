using System;

namespace Classics.Infra
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreToDatatableAttribute : Attribute
    {
        public bool IgnorePropertyToDatatable { get; set; }

        public IgnoreToDatatableAttribute()
        {
        }

    }
}
