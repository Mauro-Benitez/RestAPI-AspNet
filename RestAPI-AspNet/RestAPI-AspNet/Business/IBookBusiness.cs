using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;


namespace RestAPI_AspNet.Business
{
    public interface IBookBusiness
    {


        BookVO Create(BookVO book);
        BookVO FindById(long Id);
        BookVO Update(BookVO book);
        List<BookVO> FindAll();
        void Delete(long id);

    }
}
