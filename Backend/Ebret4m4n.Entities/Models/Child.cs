using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebret4m4n.Entities.Models
{
    public class Child
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public char Gender { get; set; }

        //DiseasesHistory

        //public List<IFormFile> Images { get; set; }

        public ICollection<Vaccine>? Vaccines { get; set; } = [];
        public ICollection<Diseas>? Diseas { get; set; } = []; 

        [ForeignKey("Certificate")]
        public int CertificateId { get; set; }
        public Certificate Certificate { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }



    }
}
