using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Repository
{
    public interface IPersonRepository
    {

        Person Create(Person person);
        Person FindById(long Id);
        Person Update(Person person);
        List<Person> FindAll();
        void Delete(long id);
        bool Exists(long Id);

    }
}
