using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Request
{
    public class BaseFilter
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 15;
        public string SearchTerm { get; set; }

    }
}
