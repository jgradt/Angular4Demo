using WebApiDemo.Data.Entities;

namespace WebApiDemo.Data.Repositories
{
    public interface ICustomerRepository : IBaseDataRepository<Customer>
    {
    }

    public class CustomerRepository : BaseDataRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(DemoDbContext demoDbContext) : base(demoDbContext)
        {
        }

        public override void SetDataForUpdate(Customer sourceEntity, Customer destinationEntity)
        {
            destinationEntity.FirstName = sourceEntity.FirstName;
            destinationEntity.LastName = sourceEntity.LastName;
            destinationEntity.Email = sourceEntity.Email;
        }

    }
}
