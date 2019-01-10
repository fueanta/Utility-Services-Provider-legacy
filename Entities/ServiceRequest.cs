using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum Status { Pending, Completed }
    public class ServiceRequest : Entity
    {
        //ID
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        [Display(Name = "Client")]
        public int? ClientId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [Display(Name = "Service")]
        public int? ServiceId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Service Time")]
        public DateTime ServiceTime { get; set; }
        public Status Status { get; set; }
        public string Helper { get; set; }
    }
}
