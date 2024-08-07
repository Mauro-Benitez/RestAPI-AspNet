namespace RestAPI_AspNet.Hypermedia.Abstract
{
    
    public interface ISupportsHyperMedia 
    {

        List<HyperMediaLink> Links { get; set; }
    }
}
