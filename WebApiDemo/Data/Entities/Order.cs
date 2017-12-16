using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Data.Entities
{
    public class Order : IDataEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Customer Customer { get; set; }
    }

    public enum OrderStatus
    {
        Received,
        InProgress,
        Shipped,
        Delivered
    }
}
