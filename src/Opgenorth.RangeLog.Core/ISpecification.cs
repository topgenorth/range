namespace Opgenorth.RangeLog.Core
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T item);
    }
}