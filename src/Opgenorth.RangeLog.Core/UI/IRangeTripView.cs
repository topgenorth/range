using System;


namespace Opgenorth.RangeLog.Core.UI
{
    public interface IRangeTripView
    {
        DateTime DateOfTrip { get; }
        int RoundsFired { get; }
        string Firearm { get; }
        string Comments { get; }
    }
}