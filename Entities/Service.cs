using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Service : Entity
    {
        // Id
        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Service Cost")]
        public double ServiceCost { get; set; }

        [Display(Name = "Labour Charge")]
        public double LabourCharge { get; set; }
    }
}
