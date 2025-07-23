using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models;
using System.ServiceModel;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services
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
    }
}
