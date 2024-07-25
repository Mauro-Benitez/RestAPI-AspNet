using RestAPI_AspNet.Data.Converter.Contract;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;

            return new PersonVO
            {

                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender

            };

        }              

        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;

            return new Person
            {
                Id = origin.Id,
                Address = origin.Address,
                FirstName = origin.FirstName,
                LastName = origin.LastName,
                Gender = origin.Gender
            };
        }

        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();


        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

        
    }
}
