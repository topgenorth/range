using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMapAdapter.Tests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StructureMapAdapter.Tests {
    public abstract class ServiceLocatorTests {
        protected readonly IServiceLocator locator;

        protected ServiceLocatorTests() {
            locator = CreateServiceLocator();
        }

        protected abstract IServiceLocator CreateServiceLocator();

        [TestMethod]
        public void GetInstance() {
            ILogger instance = locator.GetInstance<ILogger>();
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void AskingForInvalidComponentShouldRaiseActivationException() {
            locator.GetInstance<IDictionary>();
        }

        [TestMethod]
        public void GetNamedInstance() {
            ILogger instance = locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName);
            Assert.AreSame(instance.GetType(), typeof(AdvancedLogger));
        }

        [TestMethod]
        public void GetNamedInstance2() {
            ILogger instance = locator.GetInstance<ILogger>(typeof(SimpleLogger).FullName);
            Assert.AreSame(instance.GetType(), typeof(SimpleLogger));
        }

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void GetUnknownInstance2() {
            locator.GetInstance<ILogger>("test");
        }

        [TestMethod]
        public void GetAllInstances() {
            IEnumerable<ILogger> instances = locator.GetAllInstances<ILogger>();
            IList<ILogger> list = new List<ILogger>(instances);
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void GetlAllInstance_ForUnknownType_ReturnEmptyEnumerable() {
            IEnumerable<IDictionary> instances = locator.GetAllInstances<IDictionary>();
            IList<IDictionary> list = new List<IDictionary>(instances);
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void GenericOverload_GetInstance() {
            Assert.AreEqual(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance(typeof(ILogger), null).GetType());
        }

        [TestMethod]
        public void GenericOverload_GetInstance_WithName() {
            Assert.AreEqual(
                locator.GetInstance<ILogger>(typeof(AdvancedLogger).FullName).GetType(),
                locator.GetInstance(typeof(ILogger), typeof(AdvancedLogger).FullName).GetType()
                );
        }

        [TestMethod]
        public void Overload_GetInstance_NoName_And_NullName() {
            Assert.AreEqual(
                locator.GetInstance<ILogger>().GetType(),
                locator.GetInstance<ILogger>(null).GetType());
        }

        [TestMethod]
        public void GenericOverload_GetAllInstances() {
            List<ILogger> genericLoggers = new List<ILogger>(locator.GetAllInstances<ILogger>());
            List<object> plainLoggers = new List<object>(locator.GetAllInstances(typeof(ILogger)));
            Assert.AreEqual(genericLoggers.Count, plainLoggers.Count);
            for (int i = 0; i < genericLoggers.Count; i++) {
                Assert.AreEqual(
                    genericLoggers[i].GetType(),
                    plainLoggers[i].GetType());
            }
        }
    }
}