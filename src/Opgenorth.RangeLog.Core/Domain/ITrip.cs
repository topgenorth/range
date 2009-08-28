using System;


namespace Opgenorth.RangeLog.Core.Domain
{
    public interface ITrip : IDomainObject
    {
        DateTime DateOfTrip { get; set; }
        int RoundsFired { get; set; }
        string Firearm { get; set; }
        string Comments { get; set; }
    }
}