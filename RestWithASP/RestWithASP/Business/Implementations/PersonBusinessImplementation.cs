using RestWithASP.Data.Converter.Implementation;
using RestWithASP.Data.VO;
using RestWithASP.Model;
using RestWithASP.Repository;

namespace RestWithASP.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _respository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _respository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            return _converter.Parse(_respository.FindAll());
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_respository.FindById(id));
        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _respository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _respository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Disable(long id)
        {
            var personEntity = _respository.Disable(id);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _respository.Delete(id);
        }
    }
}
