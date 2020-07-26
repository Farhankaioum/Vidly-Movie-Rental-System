using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Component.CustomAttribute;

namespace Vidly.Dto
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }


        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }


        public bool IsSubscribedToNewsLetter { get; set; }

        [Required(ErrorMessage = "Please select a membership type")]
        public byte MembershipTypeId { get; set; }
    }
}
