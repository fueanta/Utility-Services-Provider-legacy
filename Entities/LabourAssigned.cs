using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LabourAssigned: Entity
    {
        //ID
        [ForeignKey("LabourId")]
        public virtual Labour Labour { get; set; }
        [Display(Name = "Labour")]
        public int? LabourId { get; set; }

        [ForeignKey("ServiceRequestId")]
        public virtual ServiceRequest ServiceRequest { get; set; }
        [Display(Name = "ServiceRequest")]
        public int? ServiceRequestId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        [Display(Name = "Employee")]
        public int? EmployeeId { get; set; }

        public DateTime TimeAssigned { get; set; }
    }
}
