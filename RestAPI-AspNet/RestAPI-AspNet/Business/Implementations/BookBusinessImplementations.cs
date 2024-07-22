using RestAPI_AspNet.Model;
using RestAPI_AspNet.Repository;
using RestAPI_AspNet.Repository.Generic;

namespace RestAPI_AspNet.Business.Implementations
{
    public class BookBusinessImplementations : IBookBusiness
    {

        private readonly IRepository<Book> _bookRepository;

        public BookBusinessImplementations(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> FindAll()
        {
            return _bookRepository.FindAll();
        }

        public Book FindById(long Id)
        {
           return _bookRepository.FindById(Id);
        }


        public Book Create(Book book)
        {
            return _bookRepository.Create(book);
        }

        public void Delete(long id)
        {
            _bookRepository.Delete(id);
        }      
       

        public Book Update(Book book)
        {
            return _bookRepository.Update(book);
        }
    }
}
