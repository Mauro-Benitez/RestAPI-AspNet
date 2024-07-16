using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Context;
using RestAPI_AspNet.Repository;
using System;

namespace RestAPI_AspNet.Business.Implementations
{
    public class PersonBusinessImplementations: IPersonBusiness
    {

       
       private readonly IPersonRepository _personRepository;



        public PersonBusinessImplementations(IPersonRepository repository)
        {
            _personRepository = repository;
        }

        // Method responsible for returning all people,
        // again this information is mocks
        public List<Person> FindAll()
        {
            return _personRepository.FindAll();
        }


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        public Person FindById(long Id)
        {
            return _personRepository.FindById(Id);
        }


        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public Person Create(Person person)
        {

            return _personRepository.Create(person); 
        }


        // Method responsible for updating a person for
        // being mock we return the same information passed
        public Person Update(Person person)
        {

           return _personRepository.Update(person); 

        }


        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }  
                

        
       
       
    }
}
