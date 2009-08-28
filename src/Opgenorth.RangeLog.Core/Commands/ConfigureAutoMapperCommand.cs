using AutoMapper;

using Opgenorth.RangeLog.Core.Domain.Impl;
using Opgenorth.RangeLog.Core.UI;


namespace Opgenorth.RangeLog.Core.Commands
{
    public class ConfigureAutoMapperCommand : ICommand
    {
        public void Execute()
        {
            Mapper.CreateMap<IRangeTripView, SimpleRangeTrip>()
                .ForMember(x => x.ID, opt => opt.Ignore());
            Mapper.AssertConfigurationIsValid();
        }
    }
}