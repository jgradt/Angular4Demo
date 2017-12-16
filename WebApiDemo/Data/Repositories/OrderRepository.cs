using WebApiDemo.Data.Entities;

namespace WebApiDemo.Data.Repositories
{
    public interface IOrderRepository : IBaseDataRepository<Order>
    {
    }

    public class OrderRepository : BaseDataRepository<Order>, IOrderRepository
    {

        public OrderRepository(DemoDbContext demoDbContext) : base(demoDbContext)
        {
        }

        public override void SetDataForUpdate(Order sourceEntity, Order destinationEntity)
        {
            destinationEntity.OrderDate = sourceEntity.OrderDate;
            destinationEntity.DeliveredDate = sourceEntity.DeliveredDate;
            destinationEntity.Status = sourceEntity.Status;
            destinationEntity.TotalDue = sourceEntity.TotalDue;
            destinationEntity.Comment = sourceEntity.Comment;
        }

    }
}
