using RestAPI_AspNet_UsingDiferentVerbs.Model;

namespace RestAPI_AspNet_UsingDiferentVerbs.Services
{
    public interface IPersonService
    {

        Person Create (Person person);
        Person FindById(long Id);
        Person Update (Person person);        
        List<Person> FindAll();
        void Delete(long id);







    }
}
