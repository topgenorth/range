namespace Opgenorth.RangeLog.Core
{
    public interface IValueReturningVisitor<TValueToReturn, T> : IVisitor<T>
    {
        TValueToReturn GetResult();
    }
}