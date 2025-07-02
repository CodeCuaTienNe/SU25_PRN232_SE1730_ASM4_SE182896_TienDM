using DNATestingSystem.Services.TienDM;
using DNATestingSystem.SoapAPIServices.TienDM.SoapModels;
using System.ServiceModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using IServiceProvider = DNATestingSystem.Services.TienDM.IServiceProvider;

namespace DNATestingSystem.SoapAPIServices.TienDM.SoapServices
{
    [ServiceContract]
    public interface IAppointmentsTienDmSoapService
    {
        [OperationContract]
        Task<List<AppointmentsTienDm>> GetAppointmentsTienDmsAsync();

        [OperationContract]
        Task<AppointmentsTienDm?> GetAppointmentsTienDmByIdAsync(int id);

        [OperationContract]
        Task<AppointmentsTienDm> CreateAppointmentsTienDmAsync(AppointmentsTienDm appointment);

        [OperationContract]
        Task<AppointmentsTienDm?> UpdateAppointmentsTienDmAsync(int id, AppointmentsTienDm appointment);

        [OperationContract]
        Task<bool> DeleteAppointmentsTienDmAsync(int id);
    }

    public class AppointmentsTienDmSoapService : IAppointmentsTienDmSoapService
    {
        private readonly IServiceProviders _serviceProviders;

        public AppointmentsTienDmSoapService(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }
        public async Task<List<AppointmentsTienDm>> GetAppointmentsTienDmsAsync()
        {
            try
            {
                var appointments = await _serviceProviders.AppointmentsTienDmService.GetAllAsync();

                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var appointmentsJsonString = JsonSerializer.Serialize(appointments, opt);

                var result = JsonSerializer.Deserialize<List<AppointmentsTienDm>>(appointmentsJsonString, opt);

                return result ?? new List<AppointmentsTienDm>();
            }
            catch (Exception)
            {
                // Log exception if needed
            }

            return new List<AppointmentsTienDm>();
        }
        public async Task<AppointmentsTienDm?> GetAppointmentsTienDmByIdAsync(int id)
        {
            try
            {
                var appointment = await _serviceProviders.AppointmentsTienDmService.GetByIdAsync(id);

                if (appointment == null)
                    return null;

                var opt = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var appointmentJsonString = JsonSerializer.Serialize(appointment, opt);

                var result = JsonSerializer.Deserialize<AppointmentsTienDm>(appointmentJsonString, opt);

                return result;
            }
            catch (Exception)
            {
                // Log exception if needed
            }

            return null;
        }

        public async Task<AppointmentsTienDm> CreateAppointmentsTienDmAsync(AppointmentsTienDm appointment)
        {
            try
            {
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var appointmentsJsonString = JsonSerializer.Serialize(appointment, opt);
                //repo - model - object  
                var appointmentRMO = JsonSerializer.Deserialize<DNATestingSystem.Repository.TienDM.Models.AppointmentsTienDm>(appointmentsJsonString, opt);

                if (appointmentRMO != null)
                {
                    var result = await _serviceProviders.AppointmentsTienDmService.CreateAsync(appointmentRMO);
                    return appointment;
                }

                return new AppointmentsTienDm();
            }
            catch (Exception) { }
            return new AppointmentsTienDm();
        }
        public async Task<AppointmentsTienDm?> UpdateAppointmentsTienDmAsync(int id, AppointmentsTienDm appointment)
        {
            try
            {
                var opt = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.IgnoreCycles, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

                var appointmentsJsonString = JsonSerializer.Serialize(appointment, opt);
                //repo - model - object
                var appointmentRMO = JsonSerializer.Deserialize<DNATestingSystem.Repository.TienDM.Models.AppointmentsTienDm>(appointmentsJsonString, opt);

                if (appointmentRMO != null)
                {
                    appointmentRMO.AppointmentsTienDmid = id; // Set the ID
                    var result = await _serviceProviders.AppointmentsTienDmService.UpdateAsync(appointmentRMO);
                    if (result > 0)
                    {
                        return appointment;
                    }
                }

                return null;
            }
            catch (Exception) { }
            return null;
        }

        public async Task<bool> DeleteAppointmentsTienDmAsync(int id)
        {
            try
            {
                var result = await _serviceProviders.AppointmentsTienDmService.DeleteAsync(id);
                return result; // Return true for success, false for failure
            }
            catch (Exception) { }
            return false;
        }




    }
}
