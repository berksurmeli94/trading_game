using TG.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TG.Core.Enums.Enums;

namespace TG.Entities
{
    [Table("users")]
    public class User: EntityBase
    {
        public override string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Authority Role { get; set; }
        public decimal TotalMoney { get; set; } = 5000000000; // 5 billion
        public List<Portfolio> Portfolios { get; set; }
    }
}
