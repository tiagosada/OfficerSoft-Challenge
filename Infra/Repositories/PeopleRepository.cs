using System;
using System.Collections.Generic;
using System.Linq;
using Domain.People;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class PeopleRepository : DatabaseRepository<Person>, IPeopleRepository
    {
        public override Person Get(Func<Person, bool> predicate)
        {
            using (var db = new Context())
            {
                return db.People
                    .FirstOrDefault(predicate);
            }
        }
        
        public override IEnumerable<Person> GetAll()
        {
            using (var db = new Context())
            {
                return db.People
                    .ToList();
            }
        }

        public override IEnumerable<Person> GetAll(Func<Person, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}