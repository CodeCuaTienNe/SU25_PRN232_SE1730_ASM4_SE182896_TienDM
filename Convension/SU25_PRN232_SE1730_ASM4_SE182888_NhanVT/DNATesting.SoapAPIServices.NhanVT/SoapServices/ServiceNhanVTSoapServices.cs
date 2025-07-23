using DNATesting.SoapAPIServices.NhanVT.SoapModels;
using DNATestingSystem.Services.NhanVT;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DNATesting.SoapAPIServices.NhanVT.SoapServices
{
    [ServiceContract]
    public interface IServiceNhanVTSoapServices
    {
        [OperationContract]
        Task<List<ServicesNhanVt>> GetServiceNhanVTAsync();

        [OperationContract]
        Task<ServicesNhanVt> GetServiceNhanVTByIDAsync(int id);

        [OperationContract]
        Task<int> CreateServiceNhanVTAsync(ServicesNhanVt dnaService);

        [OperationContract]
        Task<int> UpdateServiceNhanVTAsync(ServicesNhanVt dnaService);

        [OperationContract]
        Task<bool> DeleteServiceNhanVTAsync(int id);



        [OperationContract]
        Task<List<ServiceCategoriesNhanVt>> GetServiceCategoriesAsync();


        [OperationContract]
        Task<ServiceCategoriesNhanVt> GetServiceCatgegoryNhanVTByIDAsync(int id);

        [OperationContract]
        Task<int> CreateServiceCatgegoryNhanVTAsync(ServiceCategoriesNhanVt dnaServiceCategory);

        [OperationContract]
        Task<int> UpdateServiceCatgegoryNhanVTAsync(ServiceCategoriesNhanVt dnaServiceCategory);

        [OperationContract]
        Task<bool> DeleteServiceCatgegoryNhanVTAsync(int id);


    }


    public class ServiceNhanVTSoapServices : IServiceNhanVTSoapServices
    {

        private readonly IServiceProviders _serviceProviders;


        public ServiceNhanVTSoapServices(IServiceProviders serviceProviders) =>
            _serviceProviders = serviceProviders;
        



        public async Task<List<ServicesNhanVt>> GetServiceNhanVTAsync()
        {
            try
            {
                var dnaService = await _serviceProviders.ServicesNhanVTService.GetAllServiceAsync();

var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var json = JsonSerializer.Serialize(dnaService, opt);

                var result = JsonSerializer.Deserialize<List<ServicesNhanVt>>(json, opt);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while retrieving services: {ex.Message}");
            }

            return new List<ServicesNhanVt>();
        }

        public async Task<ServicesNhanVt> GetServiceNhanVTByIDAsync(int id)
        {
            try
            {
                var dnaService = new ServicesNhanVt();
                var result = await _serviceProviders.ServicesNhanVTService.GetServicesByIdAsync(id);
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var json = JsonSerializer.Serialize(result, opt);
                var item = JsonSerializer.Deserialize<ServicesNhanVt>(json, opt);

                return item ?? dnaService;
            }

            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while retrieving the service by ID: {ex.Message}");
            }   
        }


        public async Task<int> CreateServiceNhanVTAsync(ServicesNhanVt dnaService)
        {
            try
            {


                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };


                var json = JsonSerializer.Serialize(dnaService, opt);

                var serviceModel = JsonSerializer.Deserialize<DNATestingSystem.Repository.NhanVT.Models.ServicesNhanVt>(json, opt);

                if (serviceModel != null)
                {

                    serviceModel.ServicesNhanVtid = null;
                    serviceModel.CreatedDate = DateTime.Now;
                    serviceModel.ModifiedDate = DateTime.Now;
                    var result = await _serviceProviders.ServicesNhanVTService.CreateServiceAsync(serviceModel);
                    return result;
                }
                else
                {
                    throw new FaultException("Failed to deserialize service data");
                }

            }
            catch (Exception e)
            {
                // Log detailed exception information
                Console.WriteLine($"ERROR in CreateServiceNhanVTAsync: {e.GetType().Name}");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {e.InnerException.Message}");
                }

                // Return more specific error message
                throw new FaultException($"An error occurred while creating the service: {e.Message}");
            }
        }



        public async Task<bool> DeleteServiceNhanVTAsync(int id)
        {
            try
            {
                var result = await _serviceProviders.ServicesNhanVTService.DeleteServiceAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while deleting the service: {ex.Message}");
            }

            return false;
        }

        public async Task<int> UpdateServiceNhanVTAsync(ServicesNhanVt dnaService)
        {
            try
            {
var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

                };
                var json = JsonSerializer.Serialize(dnaService, opt);

                var item = JsonSerializer.Deserialize<DNATestingSystem.Repository.NhanVT.Models.ServicesNhanVt>(json, opt);

                var result = await _serviceProviders.ServicesNhanVTService.UpdateServiceAsync(item);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while updating the service: {ex.Message}");
            }
            return -1;

        }

        public async Task<List<ServiceCategoriesNhanVt>> GetServiceCategoriesAsync()
        {
            var dnaServiceCategory = await _serviceProviders.ServicesCategoryNhanVTService.GetAllServicesCategoryAsync(); 

            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(dnaServiceCategory, opt);
            
            var result = JsonSerializer.Deserialize<List<ServiceCategoriesNhanVt>>(json, opt);

            return result;
        }



        public async Task<ServiceCategoriesNhanVt> GetServiceCatgegoryNhanVTByIDAsync(int id)
        {
            try
            {
                var dnaServiceCategory = new ServiceCategoriesNhanVt();
                var result = await _serviceProviders.ServicesCategoryNhanVTService.GetServiceCategoryByIdAsync(id);
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };
                var json = JsonSerializer.Serialize(result, opt);
                var item = JsonSerializer.Deserialize<ServiceCategoriesNhanVt>(json, opt);

                return item ?? dnaServiceCategory;
            }

            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while retrieving the service by ID: {ex.Message}");
            }
        }


        public async Task<int> CreateServiceCatgegoryNhanVTAsync(ServiceCategoriesNhanVt dnaServiceCategory)
        {
            try
            {


                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };


                var json = JsonSerializer.Serialize(dnaServiceCategory, opt);

                var serviceModel = JsonSerializer.Deserialize<DNATestingSystem.Repository.NhanVT.Models.ServiceCategoriesNhanVt>(json, opt);

                if (serviceModel != null)
                {

                    serviceModel.ServiceCategoryNhanVtid = null;
                    serviceModel.CreatedDate = DateTime.Now;
                    
                    var result = await _serviceProviders.ServicesCategoryNhanVTService.CreateServiceCategoryAsync(serviceModel);
                    return result;
                }
                else
                {
                    throw new FaultException("Failed to deserialize service data");
                }

            }
            catch (Exception e)
            {
                // Log detailed exception information
                Console.WriteLine($"ERROR in CreateServiceNhanVTAsync: {e.GetType().Name}");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"StackTrace: {e.StackTrace}");

                if (e.InnerException != null)
                {
                    Console.WriteLine($"InnerException: {e.InnerException.Message}");
                }

                // Return more specific error message
                throw new FaultException($"An error occurred while creating the service: {e.Message}");
            }
        }



        public async Task<bool> DeleteServiceCatgegoryNhanVTAsync(int id)
        {
            try
            {
                var result = await _serviceProviders.ServicesCategoryNhanVTService.DeleteServiceCategoryAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while deleting the service: {ex.Message}");
            }

            return false;
        }

        public async Task<int> UpdateServiceCatgegoryNhanVTAsync(ServiceCategoriesNhanVt dnaServiceCategory)
        {
            try
            {
                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

                };
                var json = JsonSerializer.Serialize(dnaServiceCategory, opt);

                var item = JsonSerializer.Deserialize<DNATestingSystem.Repository.NhanVT.Models.ServiceCategoriesNhanVt>(json, opt);

                var result = await _serviceProviders.ServicesCategoryNhanVTService.UpdateServiceCategoryAsync(item);

                return result;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new FaultException($"An error occurred while updating the service: {ex.Message}");
            }
            return -1;

        }

 

    }
}
