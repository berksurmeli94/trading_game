using TG.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TG.Core.Enums.Enums;

namespace TG.Entities
{
    [Table("transactions")]
    public class Transaction: EntityBase
    {
        public override string Title { get; set; }
        [ForeignKey("users")]
        [Required]
        public string UserId { get; set; }
        [ForeignKey("shares")]
        [Required]
        public string ShareId { get; set; }
        public int Amount { get; set; }
        [Range(0, Double.MaxValue)]
        public decimal Price { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
