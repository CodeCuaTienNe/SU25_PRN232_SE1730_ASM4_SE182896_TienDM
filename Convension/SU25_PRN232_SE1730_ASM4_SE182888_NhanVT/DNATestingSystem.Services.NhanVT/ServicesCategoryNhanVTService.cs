using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Models;
using DNATestingSystem.Repository.NhanVT;

namespace DNATestingSystem.Services.NhanVT
{
    public class ServicesCategoryNhanVTService : IServicesCategoryNhanVTService
    {
        private readonly ServiceCategoriesNhanVTRepository _repository;

        public ServicesCategoryNhanVTService() => _repository = new ServiceCategoriesNhanVTRepository();
        public async Task<List<ServiceCategoriesNhanVt>> GetAllServicesCategoryAsync()
        {
            return await _repository.GetAllServiceCategory();
        }

        public async Task<ServiceCategoriesNhanVt> GetServiceCategoryByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateServiceCategoryAsync(ServiceCategoriesNhanVt serviceCategory)
        {
            return await _repository.CreateAsync(serviceCategory);
        }

        public async Task<int> UpdateServiceCategoryAsync(ServiceCategoriesNhanVt serviceCategory)
        {
            return await _repository.UpdateAsync(serviceCategory);
        }

        public async Task<bool> DeleteServiceCategoryAsync(int id)
        {
            var serviceCategoryItem = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(serviceCategoryItem);
        }



    }
}
