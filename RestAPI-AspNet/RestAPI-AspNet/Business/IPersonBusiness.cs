using RestAPI_AspNet.Model;
using RestAPI_AspNet.Data.VO;

namespace RestAPI_AspNet.Business
{
    public interface IPersonBusiness
    {

        PersonVO Create(PersonVO person);
        PersonVO FindById(long Id);
        PersonVO Update(PersonVO person);
        List<PersonVO> FindAll();
        void Delete(long id);
        PersonVO Disable (long id);

    }
}
