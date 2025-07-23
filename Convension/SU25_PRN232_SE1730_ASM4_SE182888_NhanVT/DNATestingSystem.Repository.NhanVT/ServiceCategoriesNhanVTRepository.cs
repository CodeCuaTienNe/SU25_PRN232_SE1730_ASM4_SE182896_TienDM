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
    public class ServiceCategoriesNhanVTRepository : GenericRepository<ServiceCategoriesNhanVt>
    {
        public ServiceCategoriesNhanVTRepository() { }

        public ServiceCategoriesNhanVTRepository(SE18_PRN232_SE1730_G3_DNATestingSystemContext context) => _context = context;

        public async Task<List<ServiceCategoriesNhanVt>> GetAllServiceCategory()
        {
            var allServicesCategoryNhanVT = await _context.ServiceCategoriesNhanVts.ToListAsync();

            return allServicesCategoryNhanVT ?? new List<ServiceCategoriesNhanVt>();
        }


    }
}
