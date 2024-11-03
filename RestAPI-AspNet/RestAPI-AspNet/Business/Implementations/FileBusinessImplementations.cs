using RestAPI_AspNet.Data.VO;

namespace RestAPI_AspNet.Business.Implementations
{
    public class FileBusinessImplementations : IFileBusiness
    {
        // caminho onde vai ser salvo o arquivo
        private readonly string _basePath;

        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementations(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }



        //valida salva o arquivos
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();

            var fileType = Path.GetExtension(file.FileName);

            var baseUrl = _context.HttpContext.Request.Host;

            if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".png" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(file.FileName);

                if(file != null && file.Length > 0)
                {
                    var destination = Path.Combine(_basePath, "", docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }



            }



            return fileDetail;
        }

        //salva uma lista de arquivos
        public async Task <List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            List<FileDetailVO> list = new();

            foreach(var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }


            return list;
        }


        //Obtem os dados do arquivo
        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;

            return File.ReadAllBytes(filePath);

        }



    }
}
