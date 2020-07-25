using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Component.CustomAttribute;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

       
        [Display(Name="Date Of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        
        public bool IsSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }
        
        [Display(Name = "Membership Type")]
        [Required(ErrorMessage ="Please select a membership type")]
        public byte MembershipTypeId { get; set; }
    }
}
