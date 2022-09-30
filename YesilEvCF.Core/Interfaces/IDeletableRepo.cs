namespace YesilEvCF.Core.Interfaces
{
    public interface IDeletableRepo<T> : IRepo<T> where T : class
    {
        T Delete(T item);
    }
}
