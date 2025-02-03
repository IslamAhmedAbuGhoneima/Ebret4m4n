using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class SideEffect
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [ForeignKey("Vaccine")]
        public int VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
