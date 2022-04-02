using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TG.Core.Enums.Enums;

namespace TG.Core.Entity
{
    public class EntityBase: IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID { get; set; }
        public virtual string Title { get; set; }
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedOn { get; set; }
        public RecordStatus RecordStatus { get; set; } = RecordStatus.Active;
    }
}
