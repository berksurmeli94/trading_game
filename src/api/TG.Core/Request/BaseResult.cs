using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Core.Request
{
    public class BaseResult<T>
    {
        public T Items { get; set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
