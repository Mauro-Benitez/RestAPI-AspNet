using RestAPI_AspNet.Hypermedia;
using RestAPI_AspNet.Hypermedia.Abstract;
using RestAPI_AspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI_AspNet.Data.VO
{


    public class PersonVO : ISupportsHyperMedia
    {

        public long Id { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
