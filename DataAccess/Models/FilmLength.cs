using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactStartApi.DataAccess.Models
{
    public class FILMS
    {
        public int id { get; set; }
        public string opening_crawl { get; set; }

        public string title { get; set; }

  
    }
}
