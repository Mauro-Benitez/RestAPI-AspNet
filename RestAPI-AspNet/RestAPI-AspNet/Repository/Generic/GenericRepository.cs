using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Base;
using RestAPI_AspNet.Model.Context;
using RestAPI_AspNet.Repository.Generic;
using System;
using System.Data;

namespace RestAPI_AspNet.Repository.Implementations
{
    //Repository Implementations
    public class GenericRepository<T>: IRepository<T> where T : BaseEntity
    {

       //db
        private MySQLContext _context;

        private DbSet<T> _dbSet;



        public GenericRepository(MySQLContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Method responsible for returning all people,
        // again this information is mocks
        public List<T> FindAll()
        {
            return _dbSet.ToList();
        }


        // Method responsible for returning a person
        // as we have not accessed any database we are returning a mock
        public T FindById(long Id)
        {
            return _dbSet.SingleOrDefault(t => t.Id.Equals(Id));
            
        }


        // Method responsible for creating a new person.
        // If we had a database this would be the time to persist the data
        public T Create(T item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {

                throw ;
            }
           
        }


        // Method responsible for updating a person for
        // being mock we return the same information passed
        public T Update(T item)
        {

            
            var result = _dbSet.SingleOrDefault(t => t.Id.Equals(item.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
           
        }


        // Method responsible for deleting a person from an ID
        public void Delete(long id)
        {
            var result = _dbSet.SingleOrDefault(t => t.Id.Equals(id));

            if (result != null)
            {
                try
                {
                    _dbSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
           
        }


        public bool Exists(long id)
        {
            return _dbSet.Any(t => t.Id.Equals(id));
        }



    }
}
