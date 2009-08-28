using System;


namespace Opgenorth.RangeLog.Core.Domain.Impl
{
    public class SimpleRangeTrip : ITrip
    {
        public SimpleRangeTrip()
        {
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public DateTime DateOfTrip { get; set; }
        public string Comments { get; set; }
        public int RoundsFired { get; set; }
        public string Firearm { get; set; }
    }
}