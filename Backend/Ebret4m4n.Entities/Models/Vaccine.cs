using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DocesRequired { get; set; }
        public int DocesTaken { get; set; }
        public bool IsTaken { get; set; }
        public int ChildAge { get; set; }
        public bool IsDefult { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public Child Child {  get; set; }

        public ICollection<SideEffect> SideEffects { get; set; } = [];
        
    }
}
