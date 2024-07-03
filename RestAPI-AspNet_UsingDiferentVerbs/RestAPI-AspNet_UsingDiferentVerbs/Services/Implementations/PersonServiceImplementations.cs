using RestAPI_AspNet_UsingDiferentVerbs.Model;

namespace RestAPI_AspNet_UsingDiferentVerbs.Services.Implementations
{
    public class PersonServiceImplementations : IPersonService
    {

        private volatile int count;


        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {
           
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }


            return persons;

        }

        private Person MockPerson(int i)
        {

            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name" + i,
                LastName = "Person LastName" + i,
                Address = "Some Address" + i,
                Gender = "Male"

            };
        }


        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);


        }



        public Person FindById(long Id)
        {
            return new Person
            {
                Id = 1,
                FirstName = "Mauro",
                LastName = "Elias",
                Address = "São paulo - Cambuci - SP",
                Gender = "Masculino"

            };
        }

        public Person Update(Person person)
        {
            return person;
        }
    }
}
