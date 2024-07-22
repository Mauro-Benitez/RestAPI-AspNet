using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Business
{
    public interface IBookBusiness
    {
        Book Create(Book book);
        Book FindById(long Id);
        Book Update(Book book);
        List<Book> FindAll();
        void Delete(long id);

    }
}
