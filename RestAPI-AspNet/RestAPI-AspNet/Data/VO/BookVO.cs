using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPI_AspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace RestAPI_AspNet.Data.VO
{
   
    public class BookVO
    {

        public long Id { get; set; }
        public string Autor { get; set; }
        public DateTime LaunchDate { get; set; }        
        public decimal Price { get; set; }
        public string Title { get; set; }



    }
}
