﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestAPI_AspNet.Hypermedia;
using RestAPI_AspNet.Hypermedia.Abstract;
using RestAPI_AspNet.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace RestAPI_AspNet.Data.VO
{

    public class BookVO : ISupportsHyperMedia
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; } 

        [JsonPropertyName("Autor")]
        public string Autor { get; set; }

        [JsonPropertyName("Data de lançamento")]
        public DateTime LaunchDate { get; set; }

        [JsonPropertyName("Preço")]
        public decimal Price { get; set; }

        [JsonPropertyName("Titulo")]
        public string Title { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
