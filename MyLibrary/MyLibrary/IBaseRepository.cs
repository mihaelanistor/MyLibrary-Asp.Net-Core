using System.Collections.Generic;

namespace MyLibrary
{
    public interface IBaseRepository<T>
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        List<T> GetAll();
        T FindById(int id);
    }
}