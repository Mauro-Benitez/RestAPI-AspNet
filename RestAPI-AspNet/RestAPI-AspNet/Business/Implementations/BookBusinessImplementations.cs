using RestAPI_AspNet.Data.Converter.Implementations;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Repository;
using RestAPI_AspNet.Repository.Generic;

namespace RestAPI_AspNet.Business.Implementations
{
    public class BookBusinessImplementations : IBookBusiness
    {
        //DB
        private readonly IRepository<Book> _repository;

        //Value Object Converter
        private readonly BookConverter _converter;

        public BookBusinessImplementations(IRepository<Book> bookRepository)
        {
            _repository = bookRepository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindById(long Id)
        {
            return _converter.Parse(_repository.FindById(Id));
        }


        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);


            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }      
       

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);

            return _converter.Parse(bookEntity);

        }

       
    }
}
