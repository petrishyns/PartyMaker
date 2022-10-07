using PartyMaker.Domain.Entities;
using PartyMaker.Domain.Enumerations;
using PartyMaker.Domain.Interfaces.Dao;
using PartyMaker.Domain.Interfaces.Services;
using PartyMaker.Domain.Models;

namespace PartyMaker.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerDao _customerDao;

    public CustomerService(ICustomerDao customerDao)
    {
        _customerDao = customerDao;
    }

    public Guid GetCustomerIdByUserId(Guid userId)
    {
        return _customerDao.GetCustomerIdByUserId(userId);
    }

    public CustomerDto GetById(Guid id)
    {
        var customer = _customerDao.GetCustomerById(id);

        if (customer == null)
        {
            return null;
        }

        var shortInfo = new List<OrderPreviewDto>();

        foreach (var order in customer.Orders)
        {
            shortInfo.Add(new OrderPreviewDto()
            {
                Id = order.Id,
                OrderShortInfo = MapShortInfo(order.Items.ToList()),
            });
        }

        return new CustomerDto()
        {
            Id = customer.Id,
            UserName = customer.UserName,
            Age = customer.Age,
            Email = customer.User.Email,
            Orders = shortInfo,
            Image = customer.User.Image?.Url,
        };
    }



    private static string MapShortInfo(List<Item> items)
    {
        var result = "";

        foreach (var item in items)
        {
            foreach (var req in item.ItemRequests)
            {
                result += req.SupplierService.Service.Name + ", ";
            }
        }

        return result;
    }

    public void ChangeCustomerInfo(Guid customerId, string email, string userName, int age, string firstName, string lastName)
    {
        _customerDao.ChangeInfo(customerId, email, userName, age, firstName, lastName);
    }

    public List<OrderPreviewDto> GetFilterOrders(Guid id, int state)
    {
        OrderStatus orderStatus = (OrderStatus)state;
        var shortInfo = new List<OrderPreviewDto>();

        var result = _customerDao.GetFilteredOrders(id, orderStatus);

        foreach (var order in result)
        {
            shortInfo.Add(new OrderPreviewDto()
            {
                Id = order.Id,
                OrderShortInfo = MapShortInfo(order.Items.ToList()),
            });
        }
        return shortInfo;
    }
}