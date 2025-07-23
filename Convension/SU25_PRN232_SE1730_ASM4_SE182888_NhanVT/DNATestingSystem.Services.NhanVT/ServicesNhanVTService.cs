using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Services.NhanVT
{

    public class ServicesNhanVTService : IServicesNhanVTService
    {
        private readonly ServicesNhanVtRepository _repository;

        public ServicesNhanVTService() => _repository = new ServicesNhanVtRepository();
        public async Task<List<ServicesNhanVt>> GetAllServiceAsync()
        {
            return await _repository.GetAllServicesAsync();
        }

        public async Task<ServicesNhanVt> GetServicesByIdAsync(int id)
        {
            return await _repository.GetServiceByIdAsync(id);
        }

        public async Task<List<ServicesNhanVt>> SearchServiceAsync(string serviceName, decimal? servicePrice, string categoryName)
        {
            return await _repository.SearchServicesAsync(serviceName, servicePrice, categoryName);
        }

        public async Task<int> CreateServiceAsync(ServicesNhanVt service)
        {
            return await _repository.CreateAsync(service);
        }

        public async Task<int> UpdateServiceAsync(ServicesNhanVt service)
        {
           return await _repository.UpdateAsync(service);
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var serviceItem = await _repository.GetByIdAsync(id);
            return await _repository.RemoveAsync(serviceItem);
        }


        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchWithPagingAsync(string serviceName, decimal? servicePrice, string categoryName, int page, int pageSize)
        {
            var servicePaginationItems = await _repository.SearchWithPagingAsync(serviceName, servicePrice, categoryName, page, pageSize);
            return servicePaginationItems ?? new PaginationResult<List<ServicesNhanVt>>();
        }

        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchServiceWithPagingAsync(SearchServices searchRequest)
        {
            var servicePaginationItems = await _repository.SearchServicesWithPagingAsync(searchRequest);
            return servicePaginationItems ?? new PaginationResult<List<ServicesNhanVt>>();
        }

        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchAllWithPagingAsync(int page, int pageSize)
        {
            var servicePaginationItems = await _repository.SearchAllWithPagingAsync(page, pageSize);
            return servicePaginationItems ?? new PaginationResult<List<ServicesNhanVt>>();
        }

    }
} 
