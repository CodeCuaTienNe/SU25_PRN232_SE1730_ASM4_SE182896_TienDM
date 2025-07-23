using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Repository.NhanVT
{
    public interface IUnitOfWork : IDisposable
    {
        ServicesNhanVtRepository ServicesNhanVtRepository { get; }

        ServiceCategoriesNhanVTRepository ServiceCategoriesNhanVTRepository { get; }

        UserAccountRepository UserAccountRepository { get; }
        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SE18_PRN232_SE1730_G3_DNATestingSystemContext _context;
        public ServicesNhanVtRepository _servicesNhanVtRepository;
        public ServiceCategoriesNhanVTRepository _serviceCategoriesNhanVTRepository;
        public UserAccountRepository _userAccountRepository;

        public UnitOfWork() => _context ??= new SE18_PRN232_SE1730_G3_DNATestingSystemContext();

        public ServicesNhanVtRepository ServicesNhanVtRepository
        {
            get { return _servicesNhanVtRepository ??= new ServicesNhanVtRepository(_context); }
        }


        public ServiceCategoriesNhanVTRepository ServiceCategoriesNhanVTRepository
        {
            get { return _serviceCategoriesNhanVTRepository ??= new ServiceCategoriesNhanVTRepository(_context); }
        }


        public UserAccountRepository UserAccountRepository
        {
            get { return _userAccountRepository ??= new UserAccountRepository(_context); }
        }


        public void Dispose() => _context.Dispose();

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}


