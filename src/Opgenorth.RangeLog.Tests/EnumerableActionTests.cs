using NUnit.Framework;

using Opgenorth.RangeLog.Core;

using Rhino.Mocks;


namespace Opgenorth.RangeLog.Tests
{
    [TestFixture]
    public class When_EnumerableActions_told_to_visit_all_items
    {
        [Test]
        public void Should_tell_visitor_to_visit_all_items_in_the_enumerable()
        {
            var mockVisitor = MockRepository.GenerateStub<IVisitor<int>>();
            mockVisitor.Stub(x => x.Visit(Arg<int>.Is.Anything)).Repeat.Times(1);

            var itemsToActOn = new EnumerableActions<int>(new[]
                                                              {
                                                                  1, 2, 3
                                                              });

            itemsToActOn.VisitAllItemsUsing(mockVisitor);

            mockVisitor.VerifyAllExpectations();
        }
    }
}