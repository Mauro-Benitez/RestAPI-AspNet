using Microsoft.AspNetCore.Mvc;
using RestAPI_AspNet.Data.VO;
using RestAPI_AspNet.Hypermedia.Constants;
using System.Text;

namespace RestAPI_AspNet.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();

        //Adiciona links de hipermídia a um objeto 
        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var patch = "api/person";

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
                Action = HttpActionVerb.PATCH,
                Href = links,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPatch
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
        private string GetLink(long id, IUrlHelper urlHelper, string patch)
        {

            lock (_lock)
            {
                var url = new
                {
                    controller = patch,
                    id = id,
                };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();

            };

        }
    }
}
