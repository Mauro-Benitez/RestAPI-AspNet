using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace RestAPI_AspNet.Hypermedia.Filters
{
    public class HyperMediaFilter : ResultFilterAttribute
    {

        private readonly HyperMediaFilterOptions _hyperMediaFilterOptions;


        public HyperMediaFilter(HyperMediaFilterOptions hyperMediaFilterOptions)
        {
            _hyperMediaFilterOptions = hyperMediaFilterOptions;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            TryEnrichResult(context);
            base.OnResultExecuting(context);
        }

        //Verifica se o resultado da resposta (context.Result) é um OkObjectResult
        //Tenta eriquecer 
        private void TryEnrichResult(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult objectResult)
            {
                var enricher = _hyperMediaFilterOptions
                    .ContentResponseEnricherList
                    .FirstOrDefault(x => x.CanEnricher(context));
                if (enricher != null) Task.FromResult(enricher.Enrich(context));
            };
        }
    }
}
