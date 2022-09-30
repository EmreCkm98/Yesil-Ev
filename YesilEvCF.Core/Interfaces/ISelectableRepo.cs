using System;
using System.Collections.Generic;

namespace YesilEvCF.Core.Interfaces
{
    public interface ISelectableRepo<T> : IRepo<T> where T : class
    {
        List<T> GetAll();
        T GetById(object id);
        List<T> GetByFilter(Func<T, bool> filter);
    }
}
