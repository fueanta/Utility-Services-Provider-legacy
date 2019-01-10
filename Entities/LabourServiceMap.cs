using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LabourServiceMap: Entity
    {
        // ID
        [ForeignKey("LabourId")]
        public virtual Labour Labour { get; set; }
        [Display(Name = "Labour")]
        public int? LabourId{ get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [Display(Name = "Service Type")]
        public int? ServiceId { get; set; }
    }
}
