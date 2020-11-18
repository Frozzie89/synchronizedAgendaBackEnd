using System;
using System.Collections.Generic;

namespace TI_BackEnd.Domain
{
    public interface IRepository<T>
    {
        IEnumerable<T> Query();
        T Get(int id);
        T Create(T className);
        bool Delete(int id);
        bool Update(int id, T className);
    }
}