using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Business
{
    public interface IPersonBusiness
    {

        Person Create(Person person);
        Person FindById(long Id);
        Person Update(Person person);
        List<Person> FindAll();
        void Delete(long id);

    }
}
