using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Business;
using RestAPI_AspNet.Data.VO;

namespace RestAPI_AspNet.Controllers
{
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]    
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class FileController : Controller
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }


        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] List<IFormFile> file)
        {
            FileDetailVO detail = await _fileBusiness.SaveFileToDisk(file.First());

            return new OkObjectResult(detail);
        }


        [HttpPost("uploadMultipleFiles")]
        [ProducesResponseType((200), Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFile([FromForm] List<IFormFile> files)
        {                     
            
           List<FileDetailVO> detail = await _fileBusiness.SaveFilesToDisk(files);
            return new OkObjectResult(detail);
        }


        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer =  _fileBusiness.GetFile(fileName);

            if(buffer != null)
            {
                HttpContext.Response.ContentType =
                   $"application/{Path.GetExtension(fileName).Replace(".", "")}";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
           
            return new ContentResult();
        }






    }
}
