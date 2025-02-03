using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class Diseas
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child Child { get; set; }
    }
}
