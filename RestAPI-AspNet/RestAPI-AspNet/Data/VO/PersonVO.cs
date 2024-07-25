using RestAPI_AspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI_AspNet.Data.VO
{

   
    public class PersonVO 
    {

        public long Id { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }
    }
}
