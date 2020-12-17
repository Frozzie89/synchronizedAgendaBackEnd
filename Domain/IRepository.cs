using System.Collections.Generic;

namespace TI_BackEnd.Domain
{

    public interface IRepositoryGet<T>
    {
        IEnumerable<T> Query();
        T Get(int id);
    }
    public interface IRepositoryCreate<T>
    {
        T Create(T className);
    }
    public interface IRepositoryDelete<T>
    {
        bool Delete(int id);
    }
    public interface IRepositoryUpdate<T>
    {
        bool Update(int id, T className);
    }
}