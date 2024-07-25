using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.Converter.Implementations;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Context;
using RestAPI_AspNet.Repository;
using RestAPI_AspNet.Repository.Generic;
using System;

namespace RestAPI_AspNet.Business.Implementations
{
    public class PersonBusinessImplementations : IPersonBusiness
    {

        //DB
        private  IRepository<Person> _personRepository;

        //Value Object Converter
        private PersonConverter _person;


        public PersonBusinessImplementations(IRepository<Person> repository)
        {
            _personRepository = repository;
            _person = new PersonConverter();
        }

        // Method responsible for returning all people,
       
        public List<PersonVO> FindAll()
        {
            var personEntity = _personRepository.FindAll();

            return _person.Parse(personEntity);
        }


        // Method responsible for returning a person
        
        public PersonVO FindById(long Id)
        {
            var personEntity = _personRepository.FindById(Id);

            return _person.Parse(personEntity);


        }


        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public PersonVO Create(PersonVO person)
        {
            var personEntity = _person.Parse(person);

            personEntity = _personRepository.Create(personEntity);

            return _person.Parse(personEntity);
        }

            


        // Method responsible for updating a person for
        // being mock we return the same information passed
        public PersonVO Update(PersonVO person)
        {
            var personEntity = _person.Parse(person);

            personEntity = _personRepository.Update(personEntity);

            return _person.Parse(personEntity);

        }


        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }

      
    }
}
