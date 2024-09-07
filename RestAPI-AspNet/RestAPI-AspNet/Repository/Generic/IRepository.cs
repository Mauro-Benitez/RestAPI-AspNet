using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Base;

namespace RestAPI_AspNet.Repository.Generic
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T item);
        T FindById(long Id);
        T Update(T item);
        List<T> FindAll();
        void Delete(long id);
        bool Exists(long Id);
        List<T> FindWitchPagedSearch(string query);
        int GetCount(string query);



    }
}
