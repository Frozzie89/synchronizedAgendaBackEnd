namespace TI_BackEnd.Domain
{
    public interface IRepository<T> : IRepositoryGetOnly<T>
    {
        T Create(T className);
        bool Delete(int id);
        bool Update(int id, T className);
    }
}