﻿using Microsoft.EntityFrameworkCore;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Model.Context;
using RestAPI_AspNet.Repository.Generic;
using RestAPI_AspNet.Repository.Implementations;

namespace RestAPI_AspNet.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Disable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}
