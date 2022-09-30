namespace YesilEvCF.Core.Interfaces
{
    public interface IUpdatableRepo<T> : IRepo<T> where T : class
    {
        void Update(T item);
    }
}
