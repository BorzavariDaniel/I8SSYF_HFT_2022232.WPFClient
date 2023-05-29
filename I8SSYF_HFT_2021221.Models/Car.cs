using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace I8SSYF_HFT_2021221.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Engine Engine { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Model Model { get; set; }

        [ForeignKey(nameof(Model))]
        public int ModelId { get; set; }

        [ForeignKey(nameof(Engine))]
        public int EngineId { get; set; }
    }
}
