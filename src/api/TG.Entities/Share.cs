using TG.Core.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TG.Entities
{
    [Table("shares")]
    public class Share: EntityBase
    {
        public override string Title { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        // is false when total amount is zero
        public bool IsAvailable { get; set; } = true;
        public string ShareCode { get; set; }
        [Range(0, Double.MaxValue)]
        public decimal TotalWorthOfShare { get; set; }

        [Range(0, long.MaxValue)]
        public int TotalAmountOfShare { get; set; }
        public ICollection<SharePrice> SharePrices { get; set; }
    }
}
