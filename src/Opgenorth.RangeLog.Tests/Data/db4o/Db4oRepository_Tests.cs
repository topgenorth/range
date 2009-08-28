using System;
using System.Collections.Generic;

using Db4objects.Db4o;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using Opgenorth.RangeLog.Core.Data;
using Opgenorth.RangeLog.Core.Data.db4o;
using Opgenorth.RangeLog.Core.Domain;
using Opgenorth.RangeLog.Core.Domain.Impl;

using Rhino.Mocks;


namespace Opgenorth.RangeLog.Tests.Data.db4o
{
    [TestFixture]
    public class Db4oRepository_Tests
    {
        private IDb4oConnectionFactory _mockConnectionFactory;


        private IObjectContainer _mockObjectContainer;


        [SetUp]
        public void SetUp()
        {
            _mockConnectionFactory = MockRepository.GenerateStub<IDb4oConnectionFactory>();
            _mockObjectContainer = MockRepository.GenerateMock<IObjectContainer>();
            _mockConnectionFactory.Stub(x => x.CreateObjectContainer()).Return(_mockObjectContainer);
        }

        private IRepository<ITrip> CreateSUT()
        {
            var repository = new Db4oRepository(_mockConnectionFactory);
            return repository;
        }

        [Test]
        public void Fetch_a_trip_by_Guid_returns_null()
        {
            var guidToFetch = new Guid("63600BFD-465B-4402-85A1-48B6943A453F");
            var repository = CreateSUT();

            var mockFinder = MockRepository.GenerateMock<IDb4oObjectFinder>();
            _mockConnectionFactory.Stub(x => x.CreateFinder()).Return(mockFinder);
            mockFinder.Stub(x => x.FetchObjectMatchingId<ITrip>(guidToFetch))
                .Return(null);

            var trip = repository.FetchById(guidToFetch);
            Assert.That(trip, Is.Null);
            mockFinder.VerifyAllExpectations();
        }

        [Test]
        public void Fetch_a_trip_by_Guid()
        {
            var guidToFetch = new Guid("63600BFD-465B-4402-85A1-48B6943A453F");
            var repository = CreateSUT();

            var mockFinder = MockRepository.GenerateMock<IDb4oObjectFinder>();
            _mockConnectionFactory.Stub(x => x.CreateFinder()).Return(mockFinder);
            mockFinder.Stub(x => x.FetchObjectMatchingId<ITrip>(guidToFetch))
                .Return(new SimpleRangeTrip
                            {
                                ID = guidToFetch
                            });

            var trip = repository.FetchById(guidToFetch);
            Assert.That(trip, Is.Not.Null);
            mockFinder.VerifyAllExpectations();
        }

        [Test]
        public void Delete_an_existing_trip()
        {
            var repository = CreateSUT();
            var tripToDelete = new SimpleRangeTrip();

            var mockDeleter = MockRepository.GenerateStub<IDb4oObjectDeleter>();
            _mockConnectionFactory.Stub(x => x.CreateDeleter()).Return(mockDeleter);

            repository.Remove(tripToDelete);

            mockDeleter.AssertWasCalled(x => x.DeleteObject(Arg<ITrip>.Is.Anything));
        }

        [Test]
        public void Update_an_existing_trip()
        {
            var repository = CreateSUT();
            var trip = new SimpleRangeTrip();

            repository.Update(trip);

            _mockObjectContainer.AssertWasCalled(x => x.Store(trip));
        }

        [Test]
        public void Saving_a_trip()
        {
            var repository = CreateSUT();
            var trip = MockRepository.GenerateStub<ITrip>();

            repository.Update(trip);

            _mockObjectContainer.AssertWasCalled(x => x.Store(trip));
        }

        [Test]
        public void Should_fetch_all_trips()
        {
            var repository = CreateSUT();
            _mockObjectContainer.Stub(x => x.Query<ITrip>()).Return(new List<ITrip>(0));

            repository.FetchAll();

            _mockObjectContainer.AssertWasCalled(x => x.Query<ITrip>());
        }
    }
}