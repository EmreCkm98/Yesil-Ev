using System.Collections.Generic;

namespace YesilEvCF.Core.Interfaces
{
    public interface IInsertableRepo<T> : IRepo<T> where T : class
    {
        T Add(T item);
        List<T> AddRange(List<T> items);
    }
}
