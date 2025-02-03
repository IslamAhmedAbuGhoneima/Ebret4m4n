using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child Child { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("HealthCare")]
        public int HealthCaretId { get; set; }
        public HealthCareCenter HealthCare { get; set; }
    }
}
