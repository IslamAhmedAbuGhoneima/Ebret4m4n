using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Shared.DTOs
{
    public class ChildVaccineDto
    {
        public string Name { get; set; }
        public int ChildAge { get; set; }
        public bool IsTake { get; set; } = true;
    }
}
