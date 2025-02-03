using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class HealthCareAppointment
    {
        public int Id { get; set; }
        public string FirstDay { get; set; }
        public string SecondDay { get; set; }


        [ForeignKey("HealthCareCenter")]
        public int HealthCareId { get; set; }
        public HealthCareCenter HealthCareCenter { get; set; }
    }
}
