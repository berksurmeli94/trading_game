using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Core.Entity;

namespace TG.Entities
{
    [Table("portfolios")]
    public class Portfolio : EntityBase
    {
        public override string Title { get; set; }
        [ForeignKey("users")]
        [Required]
        public string UserId { get; set; }
        public string ShareId { get; set; }
        public string PortfolioId { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShareCode { get; set; }
    }
}
