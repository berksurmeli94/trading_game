using System;
using System.Collections.Generic;

namespace TG.Common.Models.Response.Share
{
    public class GetShareResponseModel
    {
        public string CompanyName { get; set; }
        public string ShareCode { get; set; }
        public List<SharePriceMM> Prices { get; set; }
    }

    public class SharePriceMM
    {
        public decimal Price { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
