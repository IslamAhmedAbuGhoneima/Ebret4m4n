using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class HealthCareCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User {  get; set; }
        [ForeignKey("AdminOfHC")]
        public string AdminOfHCId { get; set; }
        public AdminOfHC AdminOfHC { get; set; }
    }
}
