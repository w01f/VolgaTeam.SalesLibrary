using System;

namespace SalesDepot.CoreObjects
{
    public interface ISyncObject
    {
        DateTime LastChanged { get; set; }
    }
}
