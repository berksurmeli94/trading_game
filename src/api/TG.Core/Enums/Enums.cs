using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Enums
{
    public static class Enums
    {
        public enum RecordStatus
        {
            [Display(Name = "Active")] Active = 1,

            [Display(Name = "Deleted")] Deleted = 10
        }

        public enum Authority
        {
            [Display(Name = "User")] User = 1,

            [Display(Name = "Company User")] Company = 2,

            [Display(Name = "Admin")] Admin = 10,

            [Display(Name = "Moderator")] Moderator = 11
        }

        public enum TransactionType
        {
            [Display(Name = "Buy")] BUY = 1,

            [Display(Name = "Sell")] SELL = 2,
        }
    }
}
