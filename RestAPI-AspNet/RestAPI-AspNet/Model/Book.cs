using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPI_AspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace RestAPI_AspNet.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {
       
        [Column("author")]
        public string Autor { get; set; }

        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("title")]
        public string Title { get; set; }



    }
}
