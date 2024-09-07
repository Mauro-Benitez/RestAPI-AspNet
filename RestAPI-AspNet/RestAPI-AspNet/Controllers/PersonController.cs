using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Model;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace RestAPI_AspNet.Controllers
{
    //To Version our API
    // https://github.com/microsoft/aspnet-api-versioning
    // https://github.com/microsoft/aspnet-api-versioning/wiki/New-Services-Quick-Start#aspnet-core
    // https://github.com/microsoft/aspnet-api-versioning/tree/master/samples/aspnetcore

    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class PersonController : ControllerBase
    {


        private readonly ILogger<PersonController> _logger;

        // Declaration of the service used
        private IPersonBusiness _personBusiness;


        // Injection of an instance of IPersonService
        // when creating an instance of PersonController
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }



        // Maps GET requests to https://localhost:{port}/api/person
        // Get no parameters for FindAll -> Search All
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(
            [FromQuery] string? name,            
            string sortDirection,
            int pageSize,
            int page)
        {
            return Ok(_personBusiness.FindWithPagedSearch(name, sortDirection, pageSize,page));
        }



        // Maps GET requests to https://localhost:{port}/api/person/{id}
        // receiving an ID as in the Request Path
        // Get with parameters for FindById -> Search by ID
        [HttpGet("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var person = _personBusiness.FindById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);

        }



  
        [HttpGet("findPersonByName")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            var person = _personBusiness.FindByName(firstName, lastName);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);

        }



        // Maps POST requests to https://localhost:{port}/api/person/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {

            if (person == null)
            {
                return BadRequest();
            }

            return Ok(_personBusiness.Create(person));

        }



        // Maps PUT requests to https://localhost:{port}/api/person/
        // [FromBody] consumes the JSON object sent in the request body
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {

            if (person == null)
            {
                return BadRequest();
            }

            return Ok(_personBusiness.Update(person));

        }
    
        [HttpPatch("{id}")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(long id)
        {
            var person = _personBusiness.Disable(id);
            return Ok(person);
        }


        // Maps DELETE requests to https://localhost:{port}/api/person/{id}
        // receiving an ID as in the Request Path
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

            _personBusiness.Delete(id);
            return NoContent();


        }





    }
}
