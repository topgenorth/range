using System;


namespace Opgenorth.RangeLog.Core.Domain
{
    public interface IIdentifiable
    {
        Guid ID { get; set; }
    }
}