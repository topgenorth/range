using Microsoft.Practices.ServiceLocation;

using Opgenorth.RangeLog.Core.Container.StructureMap;

using StructureMap.ServiceLocatorAdapter;


namespace Opgenorth.RangeLog.Core.Container
{
    public class InitializeCommonServiceLocatorCommand : ICommand
    {
        public void Execute()
        {
            var container = GetStructuremapContainer();
            var smServiceLocator = new StructureMapServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => smServiceLocator);
        }

        private static global::StructureMap.Container GetStructuremapContainer()
        {
            var registry = new DefaultStructureMapRegistry();
            var container = new global::StructureMap.Container(registry);
            container.AssertConfigurationIsValid();
            return container;
        }

        public static void WithDefaultStructureMapRegistry()
        {
            new InitializeCommonServiceLocatorCommand().Execute();
        }
    }
}