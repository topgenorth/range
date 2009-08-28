using Microsoft.Practices.ServiceLocation;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using Opgenorth.RangeLog.Core.Container;
using Opgenorth.RangeLog.Core.Data.db4o;

using StructureMap.ServiceLocatorAdapter;


namespace Opgenorth.RangeLog.Tests
{
    [TestFixture]
    public class InitializeCommonServiceLocatorCommand_Tests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var cmd = new InitializeCommonServiceLocatorCommand();
            cmd.Execute();
        }

        [Test]
        public void CommonServiceLocator_is_initialized()
        {
            Assert.That(ServiceLocator.Current, Is.Not.Null);
            Assert.That(ServiceLocator.Current, Is.TypeOf(typeof (StructureMapServiceLocator)));
        }

        [Test]
        public void Has_an_instance_of_IDb4oConfiguration()
        {
            var config = ServiceLocator.Current.GetInstance<IDb4oConfiguration>();
            Assert.That(config, Is.Not.Null);
        }

        [Test]
        public void Has_an_instance_of_IDb4oConnectionFactory()
        {
            var factory = ServiceLocator.Current.GetInstance<IDb4oConnectionFactory>();
            Assert.That(factory, Is.Not.Null);
        }

        [Test]
        public void Has_an_instance_of_IDb4oTripRepository()
        {
            var repository = ServiceLocator.Current.GetInstance<IDb4oTripRepository>();
            Assert.That(repository, Is.Not.Null);
        }
    }
}