using System;
using System.Collections.Generic;

using Db4objects.Db4o;

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

using Opgenorth.RangeLog.Core.Data.db4o;
using Opgenorth.RangeLog.Core.Domain;
using Opgenorth.RangeLog.Core.Domain.Impl;

using Rhino.Mocks;


namespace Opgenorth.RangeLog.Tests.Data.db4o
{
    [TestFixture]
    public class Db4oObjectFinder_Tests
    {
        private readonly Guid _guidToFind = new Guid("2CC06A93-D032-46FE-B501-B4C5DD195578");


        [Test]
        public void Find_object_matching_a_Guid()
        {
            var db = MockRepository.GenerateMock<IObjectContainer>();
            var finder = new Db4oObjectFinder(db);

            var toReturn = new List<ITrip>
                               {
                                   new SimpleRangeTrip
                                       {
                                           ID = _guidToFind
                                       }
                               };
            db.Stub(x => x.Query(Arg<Predicate<ITrip>>.Is.NotNull))
                .Return(toReturn);

            var trip = finder.FetchObjectMatchingId<ITrip>(_guidToFind);

            Assert.That(trip, Is.Not.Null);
            Assert.That(trip.ID, Is.EqualTo(_guidToFind));
        }
    }
}