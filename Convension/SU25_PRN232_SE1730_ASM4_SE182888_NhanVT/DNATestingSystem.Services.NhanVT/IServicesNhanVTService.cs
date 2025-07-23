using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;

namespace DNATestingSystem.Services.NhanVT
{
    public interface IServicesNhanVTService
    {
        Task<List<ServicesNhanVt>> GetAllServiceAsync();

        Task<ServicesNhanVt> GetServicesByIdAsync(int id);

        Task<List<ServicesNhanVt>> SearchServiceAsync(string serviceName, decimal? servicePrice, string categoryName);

        Task<int> CreateServiceAsync(ServicesNhanVt service);


        Task<int> UpdateServiceAsync(ServicesNhanVt service);


        Task<bool> DeleteServiceAsync(int id);

        Task<PaginationResult<List<ServicesNhanVt>>> SearchWithPagingAsync(string serviceName, decimal? servicePrice, string categoryName, int page, int pageSize);

        Task<PaginationResult<List<ServicesNhanVt>>> SearchServiceWithPagingAsync(SearchServices searchRequest);

        Task<PaginationResult<List<ServicesNhanVt>>> SearchAllWithPagingAsync(int page, int pageSize);

    }
}
