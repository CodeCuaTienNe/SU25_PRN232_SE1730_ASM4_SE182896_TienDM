using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public class SystemUserAccountService
    {
        private readonly UserAccountRepository _repository;

        public SystemUserAccountService() => _repository = new UserAccountRepository();

        public async Task<SystemUserAccount> GetUserAccount(string UserName, string Password)
        {
            return await _repository.GetUserAccount(UserName, Password);
        }
    }
}
