using System;

namespace TG.Common.Models.Response.Share
{
    public class ListShareResponseModel
    {
        public string ID { get; set; }
        public string CompanyName { get; set; }
        public string ShareCode { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}
