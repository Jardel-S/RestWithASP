using RestWithASP.Model;
using RestWithASP.Model.Context;
using RestWithASP.Repository.Generic;

namespace RestWithASP.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(SQLServerContext context) : base (context)
        {

        }

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

        public List<Person> FindByName(string? firstName, string? lastName)
        {
            var query = _context.Persons.AsQueryable();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(p => p.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(p => p.LastName.Contains(lastName));
            }

            return query.ToList();
        }

    }
}
