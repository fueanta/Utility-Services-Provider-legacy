using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class Labour : ParentUser
    {
        // Id
        // Name
        // Address
        // JoiningDate
        // Gender
        // ImagePath from ParentUser

        // FakeId

        public double Wallet { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        [Display(Name = "City")]
        public int? CityId { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }
        [Display(Name = "Area")]
        public int? AreaId { get; set; }
    }
}
