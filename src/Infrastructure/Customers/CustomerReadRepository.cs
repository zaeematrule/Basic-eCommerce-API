using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApiTemplate.Core.Customers;
using WebApiTemplate.Infrastructure.Persistence;

namespace WebApiTemplate.Infrastructure.Customers;

public class CustomerReadRepository : ReadRepositoryBase<Product>, ICustomerReadRepository
{
    public CustomerReadRepository(AppDbContext db)
        : base(db) { }
}
