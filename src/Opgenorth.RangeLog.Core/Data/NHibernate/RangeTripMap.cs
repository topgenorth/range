using FluentNHibernate.Mapping;

using Opgenorth.RangeLog.Core.Domain.Impl;


namespace Opgenorth.RangeLog.Core.Data.NHibernate
{
    public class RangeTripMap: ClassMap<SimpleRangeTrip>
    {
        public RangeTripMap()
        {
            Id(x => x.ID);
            Map(x => x.Comments);
            Map(x => x.DateOfTrip);
            Map(x => x.Firearm);
            Map(x => x.RoundsFired);
        }
    }
}