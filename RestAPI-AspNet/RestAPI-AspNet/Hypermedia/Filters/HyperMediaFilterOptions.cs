using RestAPI_AspNet.Hypermedia.Abstract;

namespace RestAPI_AspNet.Hypermedia.Filters
{
    public class HyperMediaFilterOptions
    {

        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();




    }
}
