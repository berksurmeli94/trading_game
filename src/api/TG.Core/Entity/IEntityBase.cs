using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TG.Core.Enums.Enums;

namespace TG.Core.Entity
{
    public interface IEntityBase
    {
        public string ID { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
