using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Area : Entity
    {
        // Id from Entity
        public string Name { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }
    }
}
