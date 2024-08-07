using Microsoft.AspNetCore.Mvc.Filters;

namespace RestAPI_AspNet.Hypermedia.Abstract
{

  
    public interface IResponseEnricher
    {
        bool CanEnricher(ResultExecutingContext  context);

        Task Enrich (ResultExecutingContext context);  


        
    }
}
