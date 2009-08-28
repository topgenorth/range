using System.Collections.Generic;


namespace Opgenorth.RangeLog.Core
{
    public interface IEnumerableActions<T>
    {
        IEnumerable<T> AllMatching(ISpecification<T> specification);
        Result GetResultOfVisitingAllItemsWith<Result>(IValueReturningVisitor<Result, T> visitor);
        //        IEnumerable<Output> TransmorgifyAllUsing<Output>(ITransmorgifier<T, Output> mapper);
        void VisitAllItemsUsing(IVisitor<T> visitor);
    }

    public class EnumerableActions<T> : IEnumerableActions<T>
    {
        private readonly IEnumerable<T> _itemsToActOn;


        public EnumerableActions(IEnumerable<T> itemsToActOn)
        {
            _itemsToActOn = itemsToActOn;
        }

        #region IEnumerableActions<T> Members

        public IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            foreach (var t in _itemsToActOn)
            {
                if (specification.IsSatisfiedBy(t))
                {
                    yield return t;
                }
            }
        }

        public Result GetResultOfVisitingAllItemsWith<Result>(IValueReturningVisitor<Result, T> visitor)
        {
            VisitAllItemsUsing(visitor);
            return visitor.GetResult();
        }

//        public IEnumerable<Output> TransmorgifyAllUsing<Output>(ITransmorgifier<T, Output> transmorgifier)
//        {
//            foreach (var t in _itemsToActOn)
//            {
//                yield return transmorgifier.Transmorgify(t);
//            }
//        }
        public void VisitAllItemsUsing(IVisitor<T> visitor)
        {
            foreach (var t in _itemsToActOn)
            {
                visitor.Visit(t);
            }
        }

        #endregion
    }

    public static class EnumerablActionExtensions
    {
//        public static IEnumerable<Output> TransmorgifyAllUsing<T, Output>(this IEnumerable<T> itemsToActOn, ITransmorgifier<T, Output> transmorgifier)
//        {
//            IEnumerableActions<T> actions = new EnumerableActions<T>(itemsToActOn);
//            return actions.TransmorgifyAllUsing(transmorgifier);
//        }
    }
}