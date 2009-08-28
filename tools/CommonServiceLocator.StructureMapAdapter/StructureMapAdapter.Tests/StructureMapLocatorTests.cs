using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMapAdapter.Tests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.ServiceLocatorAdapter;

namespace StructureMapAdapter.Tests
{
    [TestClass]
    public class StructureMapLocatorTests : ServiceLocatorTests
    {
        protected override IServiceLocator CreateServiceLocator()
        {
            Registry registry = new Registry();
            registry.ForRequestedType<ILogger>().TheDefaultIsConcreteType<AdvancedLogger>();
            registry.AddInstanceOf<ILogger>(new SimpleLogger()).WithName(typeof(SimpleLogger).FullName);
            registry.AddInstanceOf<ILogger>(new AdvancedLogger()).WithName(typeof(AdvancedLogger).FullName);
            IContainer container = new Container(registry);

            return new StructureMapServiceLocator(container);
        }

        [TestMethod]
        public void StructureMapAdapter_Get_WithZeroLenName_ReturnsDefaultInstance()
        {
            Assert.AreSame(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance<ILogger>("").GetType()
                );
        }

    }
}
