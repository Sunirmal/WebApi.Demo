using TesdtApplication.Caching;
using TesdtApplication.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace TesdtApplication.Services
{
    public class CustomerService
    {
        private readonly LRUCache<int, Models.Customer> _customerCache;
        public CustomerService(LRUCache<int, Models.Customer> customerCache)
        {
            _customerCache = customerCache;
        }
        public async Task<Customer?> GetCustomerAsync(int id)
        {
            var customer = _customerCache.Get(id);
            if(customer != null)
            {
                return customer;
            }
            await Task.Delay(100); // Simulate async operation
            var Customer = new Customer
            {
                Id = id,
                Name = $"Customer {id}",
                Email = $"customer{id}@example.com"
            };
            _customerCache.Put(id, Customer);
            return Customer;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await Task.Delay(100); // Simulate async operation
            _customerCache.Put(customer.Id, customer);
            return customer;
        }
    }
}
