using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Hypermedia.Filters;

namespace RestAPI_AspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class BookController : ControllerBase
    {
       
        private IBookBusiness _bookBusiness;

        public readonly ILogger<BookController> _logger;


        public BookController(IBookBusiness personBusiness , ILogger<BookController>logger)
        {
            _bookBusiness = personBusiness;
            _logger = logger;
        }



        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);

        }


        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            return Ok(_bookBusiness.Create(book));

        }


        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
   
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);

            return NoContent();
        }







    }
}
