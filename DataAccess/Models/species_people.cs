using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactStartApi.DataAccess.Models
{
    public class species_people
    {
        [Key, Column(Order = 0)]
        public int species_id { get; set; }
        [Key, Column(Order = 1)]
        public string people_id { get; set; }

  
    }
}
