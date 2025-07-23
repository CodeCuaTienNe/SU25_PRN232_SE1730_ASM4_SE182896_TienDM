using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IServicesCategoryNhanVTService
    {
        Task<List<ServiceCategoriesNhanVt>> GetAllServicesCategoryAsync();
        Task<ServiceCategoriesNhanVt> GetServiceCategoryByIdAsync(int id);

        Task<int> CreateServiceCategoryAsync(ServiceCategoriesNhanVt serviceCategory);


        Task<int> UpdateServiceCategoryAsync(ServiceCategoriesNhanVt serviceCategory);


        Task<bool> DeleteServiceCategoryAsync(int id);
       



    }
}
