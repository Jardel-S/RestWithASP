﻿using RestWithASP.Data.VO;
using RestWithASP.Hypermedia.Utils;
using RestWithASP.Model;

namespace RestWithASP.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);

        PersonVO FindById(long id);

        List<PersonVO> FindByName(string firstName, string lastName);

        List<PersonVO> FindAll();

        PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page);

        PersonVO Update(PersonVO person);

        PersonVO Disable(long id);

        void Delete(long id);
    }
}
