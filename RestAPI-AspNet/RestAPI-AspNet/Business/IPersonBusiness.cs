using RestAPI_AspNet.Model;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Hypermedia.Utils;

namespace RestAPI_AspNet.Business
{
    public interface IPersonBusiness
    {

        PersonVO Create(PersonVO person);
        PersonVO FindById(long Id);
        List<PersonVO> FindByName(string firstName, string lastName);
        PersonVO Update(PersonVO person);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
        void Delete(long id);
        PersonVO Disable (long id);

    }
}
