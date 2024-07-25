using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Model;

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
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            return Ok(_bookBusiness.FindById(id));
        }


        [HttpPost]
        public IActionResult Post([FromBody] BookVO book)
        {
            if(book == null)
            {
                return BadRequest();
            }

            return Ok(_bookBusiness.Create(book));

        }


        [HttpPut]
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
