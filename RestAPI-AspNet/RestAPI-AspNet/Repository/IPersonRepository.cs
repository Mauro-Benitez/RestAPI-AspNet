using RestAPI_AspNet.Model;
using RestAPI_AspNet.Repository.Generic;
using RestAPI_AspNet.Repository.Implementations;

namespace RestAPI_AspNet.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {

        Person Disable(long id);


        List<Person>FindByName(string firstName, string lastName);


    }
}
