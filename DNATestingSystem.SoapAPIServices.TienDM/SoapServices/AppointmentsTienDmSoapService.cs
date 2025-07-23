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
        Task<bool> CreateAppointmentsTienDmAsync(AppointmentsTienDm appointment);

        [OperationContract]
        Task<bool> UpdateAppointmentsTienDmAsync(int id, AppointmentsTienDm appointment);

        [OperationContract]
        Task<bool> DeleteAppointmentsTienDmAsync(int id);

        [OperationContract]
        Task<string> TestAppointmentData(AppointmentsTienDm appointment);
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

                // Manual mapping from Repository model to SOAP model
                var result = appointments.Select(repo => new AppointmentsTienDm
                {
                    AppointmentsTienDmid = repo.AppointmentsTienDmid,
                    UserAccountId = repo.UserAccountId,
                    ServicesNhanVtid = repo.ServicesNhanVtid,
                    AppointmentStatusesTienDmid = repo.AppointmentStatusesTienDmid,
                    AppointmentDate = repo.AppointmentDate.ToDateTime(TimeOnly.MinValue).ToString("yyyy-MM-dd"),
                    AppointmentTime = repo.AppointmentTime.ToTimeSpan().ToString(@"hh\:mm\:ss"),
                    SamplingMethod = repo.SamplingMethod,
                    Address = repo.Address,
                    ContactPhone = repo.ContactPhone,
                    Notes = repo.Notes,
                    TotalAmount = repo.TotalAmount,
                    IsPaid = repo.IsPaid,
                    CreatedDate = repo.CreatedDate,
                    ModifiedDate = repo.ModifiedDate
                }).ToList();

                return result;
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

                // Manual mapping from Repository model to SOAP model
                var result = new AppointmentsTienDm
                {
                    AppointmentsTienDmid = appointment.AppointmentsTienDmid,
                    UserAccountId = appointment.UserAccountId,
                    ServicesNhanVtid = appointment.ServicesNhanVtid,
                    AppointmentStatusesTienDmid = appointment.AppointmentStatusesTienDmid,
                    AppointmentDate = appointment.AppointmentDate.ToDateTime(TimeOnly.MinValue).ToString("yyyy-MM-dd"),
                    AppointmentTime = appointment.AppointmentTime.ToTimeSpan().ToString(@"hh\:mm\:ss"),
                    SamplingMethod = appointment.SamplingMethod,
                    Address = appointment.Address,
                    ContactPhone = appointment.ContactPhone,
                    Notes = appointment.Notes,
                    TotalAmount = appointment.TotalAmount,
                    IsPaid = appointment.IsPaid,
                    CreatedDate = appointment.CreatedDate,
                    ModifiedDate = appointment.ModifiedDate
                };

                return result;
            }
            catch (Exception)
            {
                // Log exception if needed
            }

            return null;
        }

        public async Task<bool> CreateAppointmentsTienDmAsync(AppointmentsTienDm appointment)
        {
            try
            {
                Console.WriteLine($"[SOAP Create] Received appointment data:");
                Console.WriteLine($"  appointment is null: {appointment == null}");

                if (appointment != null)
                {
                    Console.WriteLine($"  AppointmentsTienDmid: {appointment.AppointmentsTienDmid}");
                    Console.WriteLine($"  UserAccountId: {appointment.UserAccountId}");
                    Console.WriteLine($"  ServicesNhanVtid: {appointment.ServicesNhanVtid}");
                    Console.WriteLine($"  AppointmentStatusesTienDmid: {appointment.AppointmentStatusesTienDmid}");
                    Console.WriteLine($"  AppointmentDate: {appointment.AppointmentDate}");
                    Console.WriteLine($"  AppointmentTime: {appointment.AppointmentTime}");
                    Console.WriteLine($"  SamplingMethod: '{appointment.SamplingMethod}' (is null: {appointment.SamplingMethod == null})");
                    Console.WriteLine($"  Address: '{appointment.Address}' (is null: {appointment.Address == null})");
                    Console.WriteLine($"  ContactPhone: '{appointment.ContactPhone}' (is null: {appointment.ContactPhone == null})");
                    Console.WriteLine($"  Notes: '{appointment.Notes}' (is null: {appointment.Notes == null})");
                    Console.WriteLine($"  TotalAmount: {appointment.TotalAmount}");
                    Console.WriteLine($"  IsPaid: {appointment.IsPaid}");
                    Console.WriteLine($"  CreatedDate: {appointment.CreatedDate}");
                    Console.WriteLine($"  ModifiedDate: {appointment.ModifiedDate}");
                }

                if (appointment == null)
                {
                    Console.WriteLine("[SOAP Create] Appointment is null");
                    return false;
                }

                // Validate required fields before proceeding
                if (appointment.UserAccountId <= 0)
                {
                    Console.WriteLine($"[SOAP Create] Invalid UserAccountId: {appointment.UserAccountId}");
                    return false;
                }

                if (appointment.ServicesNhanVtid <= 0)
                {
                    Console.WriteLine($"[SOAP Create] Invalid ServicesNhanVtid: {appointment.ServicesNhanVtid}");
                    return false;
                }

                if (appointment.AppointmentStatusesTienDmid <= 0)
                {
                    Console.WriteLine($"[SOAP Create] Invalid AppointmentStatusesTienDmid: {appointment.AppointmentStatusesTienDmid}");
                    return false;
                }

                if (string.IsNullOrEmpty(appointment.ContactPhone))
                {
                    Console.WriteLine($"[SOAP Create] ContactPhone is empty or null");
                    return false;
                }

                if (string.IsNullOrEmpty(appointment.SamplingMethod))
                {
                    Console.WriteLine($"[SOAP Create] SamplingMethod is empty or null");
                    return false;
                }

                // Manual mapping instead of JSON serialization to avoid issues
                var appointmentRMO = new DNATestingSystem.Repository.TienDM.Models.AppointmentsTienDm
                {
                    UserAccountId = appointment.UserAccountId,
                    ServicesNhanVtid = appointment.ServicesNhanVtid,
                    AppointmentStatusesTienDmid = appointment.AppointmentStatusesTienDmid,
                    AppointmentDate = DateTime.TryParse(appointment.AppointmentDate, out var date) ? DateOnly.FromDateTime(date) : DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    AppointmentTime = TimeSpan.TryParse(appointment.AppointmentTime, out var time) ? TimeOnly.FromTimeSpan(time) : TimeOnly.FromTimeSpan(new TimeSpan(9, 0, 0)),
                    SamplingMethod = appointment.SamplingMethod ?? "",
                    Address = appointment.Address,
                    ContactPhone = appointment.ContactPhone ?? "",
                    Notes = appointment.Notes,
                    TotalAmount = appointment.TotalAmount,
                    IsPaid = appointment.IsPaid,
                    CreatedDate = DateTime.Now
                };

                Console.WriteLine($"[SOAP Create] After manual mapping:");
                Console.WriteLine($"  ContactPhone: '{appointmentRMO.ContactPhone}' (is null: {appointmentRMO.ContactPhone == null})");
                Console.WriteLine($"  UserAccountId: {appointmentRMO.UserAccountId}");
                Console.WriteLine($"  SamplingMethod: '{appointmentRMO.SamplingMethod}'");
                Console.WriteLine($"  TotalAmount: {appointmentRMO.TotalAmount}");

                var result = await _serviceProviders.AppointmentsTienDmService.CreateAsync(appointmentRMO);
                Console.WriteLine($"[SOAP Create] Service returned: {result}");
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SOAP Create] Exception: {ex.Message}\n{ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[SOAP Create] InnerException: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}");
                    if (ex.InnerException.InnerException != null)
                    {
                        Console.WriteLine($"[SOAP Create] InnerInnerException: {ex.InnerException.InnerException.Message}\n{ex.InnerException.InnerException.StackTrace}");
                    }
                }
                return false;
            }
        }
        public async Task<bool> UpdateAppointmentsTienDmAsync(int id, AppointmentsTienDm appointment)
        {
            try
            {
                Console.WriteLine($"[SOAP Update] Received update for ID {id}:");
                Console.WriteLine($"  ContactPhone: '{appointment?.ContactPhone}' (is null: {appointment?.ContactPhone == null})");
                Console.WriteLine($"  AppointmentsTienDmid in request: {appointment?.AppointmentsTienDmid}");
                Console.WriteLine($"  UserAccountId: {appointment?.UserAccountId}");

                if (appointment == null)
                {
                    Console.WriteLine("[SOAP Update] Appointment is null");
                    return false;
                }

                // Manual mapping instead of JSON serialization to avoid issues
                var appointmentRMO = new DNATestingSystem.Repository.TienDM.Models.AppointmentsTienDm
                {
                    AppointmentsTienDmid = id, // Set the ID from parameter
                    UserAccountId = appointment.UserAccountId,
                    ServicesNhanVtid = appointment.ServicesNhanVtid,
                    AppointmentStatusesTienDmid = appointment.AppointmentStatusesTienDmid,
                    AppointmentDate = DateTime.TryParse(appointment.AppointmentDate, out var date) ? DateOnly.FromDateTime(date) : DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    AppointmentTime = TimeSpan.TryParse(appointment.AppointmentTime, out var time) ? TimeOnly.FromTimeSpan(time) : TimeOnly.FromTimeSpan(new TimeSpan(9, 0, 0)),
                    SamplingMethod = appointment.SamplingMethod ?? "",
                    Address = appointment.Address,
                    ContactPhone = appointment.ContactPhone ?? "",
                    Notes = appointment.Notes,
                    TotalAmount = appointment.TotalAmount,
                    IsPaid = appointment.IsPaid,
                    ModifiedDate = DateTime.Now
                };

                Console.WriteLine($"[SOAP Update] After manual mapping:");
                Console.WriteLine($"  ContactPhone: '{appointmentRMO.ContactPhone}' (is null: {appointmentRMO.ContactPhone == null})");
                Console.WriteLine($"  Final AppointmentsTienDmid: {appointmentRMO.AppointmentsTienDmid}");

                var result = await _serviceProviders.AppointmentsTienDmService.UpdateAsync(appointmentRMO);
                Console.WriteLine($"[SOAP Update] Service returned: {result}");
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SOAP Update] Exception: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
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

        public Task<string> TestAppointmentData(AppointmentsTienDm appointment)
        {
            if (appointment == null)
                return Task.FromResult("Appointment is null");

            var result = $"UserAccountId: {appointment.UserAccountId}, " +
                   $"ServicesNhanVtid: {appointment.ServicesNhanVtid}, " +
                   $"AppointmentStatusesTienDmid: {appointment.AppointmentStatusesTienDmid}, " +
                   $"ContactPhone: '{appointment.ContactPhone}', " +
                   $"SamplingMethod: '{appointment.SamplingMethod}', " +
                   $"TotalAmount: {appointment.TotalAmount}, " +
                   $"AppointmentDate: {appointment.AppointmentDate}, " +
                   $"AppointmentTime: {appointment.AppointmentTime}";

            return Task.FromResult(result);
        }




    }
}
