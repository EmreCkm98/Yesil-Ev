namespace YesilEvCF.Core.Interfaces
{
    public interface IRepo<T> where T : class
    {
        void SaveChanges();
    }
}
