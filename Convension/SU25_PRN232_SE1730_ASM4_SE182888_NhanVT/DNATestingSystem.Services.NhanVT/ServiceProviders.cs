using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATestingSystem.Services.NhanVT
{

    public interface IServiceProviders
    {
        SystemUserAccountService SystemUserAccountService { get; }
        IServicesNhanVTService ServicesNhanVTService { get; }

        IServicesCategoryNhanVTService ServicesCategoryNhanVTService { get; }
    }
    public class ServiceProviders : IServiceProviders
    {
        private SystemUserAccountService _systemUserAccountService;
        private IServicesCategoryNhanVTService _servicesCategoryNhanVTService;
        private IServicesNhanVTService _servicesNhanVTService;
   
        public ServiceProviders() { }

        public SystemUserAccountService SystemUserAccountService
        {
            get { return _systemUserAccountService ??= new SystemUserAccountService(); }
        }

        public IServicesNhanVTService ServicesNhanVTService
        {
            get { return _servicesNhanVTService ??= new ServicesNhanVTService(); }
        }

        public IServicesCategoryNhanVTService ServicesCategoryNhanVTService
        {
            get { return _servicesCategoryNhanVTService ??= new ServicesCategoryNhanVTService(); }
        }
    }
}
