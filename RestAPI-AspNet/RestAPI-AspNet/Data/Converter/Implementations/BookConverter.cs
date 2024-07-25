using RestAPI_AspNet.Data.Converter.Contract;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;

namespace RestAPI_AspNet.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;

            return new BookVO
            {
                Autor = origin.Autor,
                Id = origin.Id,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title
            };
        }

        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;

            return new Book
            {
                Autor = origin.Autor,
                Id = origin.Id,
                LaunchDate = origin.LaunchDate,
                Price = origin.Price,
                Title = origin.Title

            };
        }

        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }

       

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;

            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
