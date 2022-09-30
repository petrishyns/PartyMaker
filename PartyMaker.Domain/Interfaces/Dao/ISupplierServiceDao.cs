using PartyMaker.Domain.Entities;

namespace PartyMaker.Domain.Interfaces.Dao;

public interface ISupplierServiceDao
{
    void Create(string description, string imageUrl, Guid supplierId, Guid serviceId);

    void Update(string description, string imageUrl, Guid supplierId, Guid serviceId);

    void Delete(Guid id);

    SupplierService GetById(Guid id);

}