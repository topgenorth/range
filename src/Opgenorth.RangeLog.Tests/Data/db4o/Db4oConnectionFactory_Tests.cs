using System;
using System.IO;

using NUnit.Framework;

using Opgenorth.RangeLog.Core.Data.db4o;


namespace Opgenorth.RangeLog.Tests.Data.db4o
{
    [TestFixture]
    public class Db4oConnectionFactory_Tests
    {
        private readonly IDb4oConfiguration config = new UnitTestingDb4oConfiguration();


        [SetUp]
        public void SetUp()
        {
            if (File.Exists(config.GetFilename()))
            {
                File.Delete(config.GetFilename());
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(config.GetFilename()))
            {
                File.Delete(config.GetFilename());
            }
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public void Throw_a_NullReferenceException_if_null_configuration_provided()
        {
            new Db4oConnectionFactory(null);
        }

        [Test]
        public void Create_a_Db4oConnection_object()
        {
            var factory = new Db4oConnectionFactory(config);
            var db = factory.CreateObjectContainer();

            Assert.That(db, Is.Not.Null);
            Assert.That(File.Exists(config.GetFilename()), Is.True);
            db.Dispose();
        }
    }
}