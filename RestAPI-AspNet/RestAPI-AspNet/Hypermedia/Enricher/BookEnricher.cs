using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Hypermedia.Constants;
using RestAPI_AspNet.Hypermedia.Filters;
using System.IO;
using System.Text;

namespace RestAPI_AspNet.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {

        private readonly object _lock = new object();

            //Adiciona links de hipermídia a um objeto 
            protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
            {
                var patch = "api/book";

                string links = GetLink(content.Id, urlHelper, patch);

                content.Links.Add(new HyperMediaLink()
                {
                    Action = HttpActionVerb.GET,
                    Href = links,
                    Rel = RelationType.self,
                    Type = ResponseTypeFormat.DefaultGet
                });

                content.Links.Add(new HyperMediaLink()
                {
                    Action = HttpActionVerb.POST,
                    Href = links,
                    Rel = RelationType.post,
                    Type = ResponseTypeFormat.DefaultPost
                });

                content.Links.Add(new HyperMediaLink()
                {
                    Action = HttpActionVerb.PUT,
                    Href = links,
                    Rel = RelationType.put,
                    Type = ResponseTypeFormat.DefaultPut
                });

                content.Links.Add(new HyperMediaLink()
                {
                    Action = HttpActionVerb.DELETE,
                    Href = links,
                    Rel = RelationType.delete,
                    Type = "int"
                });
                return Task.CompletedTask;
            }

        //gera uma URL para um determinado controlador (patch) 
        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {

            lock (_lock) 
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();

            };
            
        }
    }
}
