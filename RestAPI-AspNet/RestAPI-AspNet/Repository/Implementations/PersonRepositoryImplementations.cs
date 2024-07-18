using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Context;
using System;

namespace RestAPI_AspNet.Repository.Implementations
{
    public class PersonRepositoryImplementations:IPersonRepository
    {

       
        private MySQLContext _context;


        public PersonRepositoryImplementations(MySQLContext context)
        {
            _context = context;
        }

        // Method responsible for returning all people,
        // again this information is mocks
        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        public Person FindById(long Id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(Id));
        }


        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ;
            }

            return person;
        }


        // Method responsible for updating a person for
        // being mock we return the same information passed
        public Person Update(Person person)
        {

            if (!Exists(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return person;
        }


        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            var result =  _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


        public bool Exists(long Id)
        {
            return _context.Persons.Any(P => P.Id.Equals(Id));
        }

       
       
    }
}
