using System;
using System.Collections.Generic;

namespace TG.Common.Models.Response.Portfolio
{
    public class PortfolioListResponseModel
    {
        public List<PortfolioInfo> Portfolios { get; set; } = new List<PortfolioInfo>();
    }


    public class PortfolioInfo
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public List<ShareInfo> ShareInfos { get; set; } = new List<ShareInfo>();
    }

    public class ShareInfo
    {
        public string ShareId { get; set; }
        public string ShareCode { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalAmount { get; set; }
        public DateTimeOffset CreatedOn { get; set; }

    }
}
