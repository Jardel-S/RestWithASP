using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP.Model;
using RestWithASP.Model.Context;
using RestWithASP.Repository;
using System;

namespace RestWithASP.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _respository;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _respository = repository;
        }

        public List<Person> FindAll()
        {
            return _respository.FindAll();
        }

        public Person FindById(long id)
        {
            return _respository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _respository.Create(person);
        }

        public Person Update(Person person)
        {
           return Update(person);
        }

        public void Delete(long id)
        {
            _respository.Delete(id);
        }

        private bool Exists(long id)
        {
            return _respository.Exists(id);
        }
    }
}
