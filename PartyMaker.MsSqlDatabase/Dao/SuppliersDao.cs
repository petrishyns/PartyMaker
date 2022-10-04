﻿using Microsoft.EntityFrameworkCore;
using PartyMaker.Domain.Entities;
using PartyMaker.Domain.Interfaces.Dao;

namespace PartyMaker.MsSqlDatabase.Dao;

public class SuppliersDao : ISuppliersDao
{
    private readonly PartyMakerContext _context;

    public SuppliersDao(PartyMakerContext context)
    {
        _context = context;
    }

    public List<Supplier> GetSupliers()
    {
        return _context.Suppliers.Include(sup => sup.User).ToList();
    }
    public Supplier GetById(Guid id)
    {
        return _context.Suppliers.FirstOrDefault(x => x.Id == id);
    }

    public Supplier GetSuppliersInfoById(Guid id)
    {
        var supplier = _context.Suppliers
            .Include(x => x.SupplierServices)
                .ThenInclude(x=>x.Image)
            .Include(x=>x.SupplierServices)
                .ThenInclude(x=>x.Service)
            .Include(x=>x.User)
                .ThenInclude(x=>x.Image)
            .FirstOrDefault(x => x.Id == id);
        return supplier;
    }

    public List<Supplier> GetByServiceId(Guid id)
    {
        return _context.SupplierServices
            .Include(x => x.Service)
            .Include(x => x.Supplier)
            .Where(x=>x.Service.Id == id)
            .Select(x=>x.Supplier)
            .ToList();
    }

    public void ChangeMainInfo(Guid supplierId, string companyName, string phone, string city, string description)
    {
        var supplier = _context.Suppliers
            .Include(x => x.User)
            .FirstOrDefault(x=>x.Id == supplierId);
        if (supplier == null)
        {
            return;
        }

        supplier.City = city;
        supplier.CompanyName = companyName;
        supplier.Description = description;
        supplier.User.Phone = phone;

        _context.Suppliers.Update(supplier);
        _context.SaveChanges();
    }


    public void Deactivate(Guid id)
    {
        var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
        if (supplier == null)
        {
            return;
        }
        supplier.IsDeleted = true;
        _context.SaveChanges();
    }

    public void Activate(Guid id)
    {
        var supplier = _context.Suppliers.FirstOrDefault(x => x.Id == id);
        if (supplier == null)
        {
            return;
        }
        supplier.IsDeleted = false;
        _context.SaveChanges();
    }
}