namespace Opgenorth.RangeLog.Core
{
    public interface IVisitor<T>
    {
        void Visit(T item);
    }
}