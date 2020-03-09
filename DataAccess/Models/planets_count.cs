using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactStartApi.DataAccess.Models
{
    public class planets_count
    {
        [Key, Column(Order = 0)]
        public int id { get; set; }
        public int planets_counts { get; set; }

        public string name { get; set; }


    }
}
