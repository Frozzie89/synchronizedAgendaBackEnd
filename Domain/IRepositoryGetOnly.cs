using System.Collections.Generic;

namespace TI_BackEnd.Domain
{
    public interface IRepositoryGetOnly<T>
    {
        IEnumerable<T> Query();
        T Get(int id);
    }
}