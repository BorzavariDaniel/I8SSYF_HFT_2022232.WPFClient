using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I8SSYF_HFT_2021221.Models
{
    public class Engine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EngineId { get; set; }
        public string Fuel { get; set; }
        public int NumOfCylinders { get; set; }

        [NotMapped]
        public virtual ICollection<Car> Cars { get; set; }

        public Engine()
        {
            Cars = new HashSet<Car>();
        }
    }
}
