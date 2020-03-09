using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactStartApi.DataAccess.Models
{
    public class films_characters
    {
        [Key, Column(Order = 0)]
        public int film_id { get; set; }
        [Key, Column(Order = 1)]
        public int people_id { get; set; }

  
    }
}
