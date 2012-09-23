using System;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public interface ISyncObject
    {
        DateTime LastChanged { get; set; }
    }
}
