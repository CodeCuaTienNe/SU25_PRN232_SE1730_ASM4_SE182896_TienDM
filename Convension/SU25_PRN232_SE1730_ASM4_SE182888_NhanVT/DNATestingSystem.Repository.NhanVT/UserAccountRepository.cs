using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Repository.NhanVT
{
    public class UserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public UserAccountRepository() { }

        public UserAccountRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;
        
        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            //return await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Phone == username && u.Password == password);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == username && u.Password == password);


        }


    }
}
