using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RestAPI_AspNet.Hypermedia.Abstract;
using System.Collections.Concurrent;
using System.Xml.Linq;

namespace RestAPI_AspNet.Hypermedia
{
    public abstract class ContentResponseEnricher<T> : IResponseEnricher where T : ISupportsHyperMedia
    {


        public ContentResponseEnricher()
        {

        }

        //Verifica se o tipo do conteúdo é T ou uma lista de T
        public virtual bool CanEnricher(Type contentType)
        {
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }


        //Este método recebe o conteúdo a ser enriquecido e um helper de URL para gerar links.
        protected abstract Task EnrichModel(T content, IUrlHelper urlHelper);



        //Verifica se pode enriquecer
        bool IResponseEnricher.CanEnricher(ResultExecutingContext response)
        {
            if (response.Result is OkObjectResult okObjectResult)
            {
                return CanEnricher(okObjectResult.Value.GetType());
            }

            return false;
        }



        //Executa o enriquecimento 
        public async Task Enrich(ResultExecutingContext response)
        {
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);

            if (response.Result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is T model)
                {
                    await EnrichModel(model, urlHelper);
                }

                else if (okObjectResult.Value is List<T> collection)
                {
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);

                    Parallel.ForEach(bag, (element) =>
                    {
                        EnrichModel(element, urlHelper);
                    });

                }

            }

            await Task.FromResult<object>(null);

        }
    }
}
