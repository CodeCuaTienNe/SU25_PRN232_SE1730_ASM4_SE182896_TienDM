using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATestingSystem.Repository.NhanVT.Basic;
using DNATestingSystem.Repository.NhanVT.ModelExtensions;
using DNATestingSystem.Repository.NhanVT.Models;
using Microsoft.EntityFrameworkCore;

namespace DNATestingSystem.Repository.NhanVT
{
    public class ServicesNhanVtRepository : GenericRepository<ServicesNhanVt>
    {
        public ServicesNhanVtRepository() { }

        public ServicesNhanVtRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<ServicesNhanVt>> GetAllServicesAsync()
        {
            var allServicesNhanVT = await _context.ServicesNhanVts.Include(s => s.ServiceCategoryNhanVt).ToListAsync();

            return allServicesNhanVT ?? new List<ServicesNhanVt>();
        }

        public async Task<ServicesNhanVt> GetServiceByIdAsync(int code)
        {
            var serviceItem = await _context.ServicesNhanVts
                .Include(s => s.ServiceCategoryNhanVt)
                .FirstOrDefaultAsync(s => s.ServicesNhanVtid == code);

            return serviceItem ?? new ServicesNhanVt();
        }

        public async Task<List<ServicesNhanVt>> SearchServicesAsync(string serviceName, decimal? servicePrice, string categoryName)
        {
            var servicesNhanVT = await _context.ServicesNhanVts
                .Include(s => s.ServiceCategoryNhanVt)
                .Where(s => (s.ServiceName.Contains(serviceName) || string.IsNullOrEmpty(serviceName))
                    && (s.Price == servicePrice || servicePrice == 0 || servicePrice == null)
                    && (s.ServiceCategoryNhanVt.CategoryName.Contains(categoryName) || string.IsNullOrEmpty(categoryName))
                ).ToListAsync();

            return servicesNhanVT ?? new List<ServicesNhanVt>();
        }

        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchWithPagingAsync(string serviceName, decimal? servicePrice, string categoryName, int page, int pageSize)
        {
            var servicesNhanVT = await _context.ServicesNhanVts
               .Include(s => s.ServiceCategoryNhanVt)
               .Where(s => (s.ServiceName.Contains(serviceName) || string.IsNullOrEmpty(serviceName))
                   && (s.Price == servicePrice || servicePrice == 0 || servicePrice == null)
                   && (s.ServiceCategoryNhanVt.CategoryName.Contains(categoryName) || string.IsNullOrEmpty(categoryName))
               ).ToListAsync();

            var totalItems = servicesNhanVT.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            servicesNhanVT = servicesNhanVT.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<ServicesNhanVt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = servicesNhanVT
            };
            return result;


        }

        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchServicesWithPagingAsync(SearchServices searchRequest)
        {
            var servicesNhanVT = await _context.ServicesNhanVts
               .Include(s => s.ServiceCategoryNhanVt)
               .Where(s => (s.ServiceName.Contains(searchRequest.serviceName) || string.IsNullOrEmpty(searchRequest.serviceName))
                   && (s.Price == searchRequest.servicePrice || searchRequest.servicePrice == 0 || searchRequest.servicePrice == null)
                   && (s.ServiceCategoryNhanVt.CategoryName.Contains(searchRequest.categoryName) || string.IsNullOrEmpty(searchRequest.categoryName))
               ).ToListAsync();

            var totalItems = servicesNhanVT.Count();
            var totalPages = (int)Math.Ceiling((double)searchRequest.CurrentPage / searchRequest.PageSize ?? 1);
            var currentPage = searchRequest.CurrentPage ?? 1;
            var pageSize = searchRequest.PageSize ?? 1;

            servicesNhanVT = servicesNhanVT.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<ServicesNhanVt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = servicesNhanVT
            };
            return result;


        }

        public async Task<PaginationResult<List<ServicesNhanVt>>> SearchAllWithPagingAsync(int page, int pageSize)
        {
            var servicesNhanVT = await _context.ServicesNhanVts.Include(s => s.ServiceCategoryNhanVt).ToListAsync();

            var totalItems = servicesNhanVT.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            servicesNhanVT = servicesNhanVT.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<ServicesNhanVt>>
            {
                TotalItems = totalItems,
                TotalPage = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = servicesNhanVT
            };
            return result;

        }


    }
}
