using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.Converter.Implementations;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Hypermedia.Utils;
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
        private  IPersonRepository _personRepository;

        //Value Object Converter
        private PersonConverter _person;


        public PersonBusinessImplementations(IPersonRepository repository)
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


        public PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
        {
            
            var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";


            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_Name like '%{name}%'";
            query += $"order by p.first_Name {sort} limit {size} offset {offset}";      
         
            string countQuery = @"select count(*) from person p where 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_Name like '%{name}%'";

            var persons = _personRepository.FindWitchPagedSearch(query);

            int totalResult = _personRepository.GetCount(countQuery);


            return new PagedSearchVO<PersonVO> {
            CurrentPage = page,
            List = _person.Parse(persons),
            PageSize = size,
            SortDirections = sort,
            TotalResult = totalResult,

            };
        }



        // Method responsible for returning a person

        public PersonVO FindById(long Id)
        {
            var personEntity = _personRepository.FindById(Id);

            return _person.Parse(personEntity);
        }

        // Method responsible for returning a person find by name
        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            var personEntity = _personRepository.FindByName(firstName, lastName); 

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
         public PersonVO Disable(long id)
         {
            var personEntity = _personRepository.Disable(id);
            return _person.Parse(personEntity);
         }

        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            _personRepository.Delete(id);
        }

        
    }
}
