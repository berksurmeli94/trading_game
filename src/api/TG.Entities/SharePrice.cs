using TG.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Entities
{
    [Table("share_prices")]
    public class SharePrice: EntityBase
    {
        [ForeignKey("companies")]
        [Required]
        public string ShareId { get; set; }
        public override string Title { get; set; }
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }
        public Share Share { get; set; }
    }
}
