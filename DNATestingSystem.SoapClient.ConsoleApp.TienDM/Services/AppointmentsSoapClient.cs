using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models;
using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services
{
    public class AppointmentsSoapClient : IDisposable
    {
        private readonly ChannelFactory<IAppointmentsTienDmSoapService> _channelFactory;
        private readonly IAppointmentsTienDmSoapService _client;

        public AppointmentsSoapClient(string serviceUrl)
        {
            Binding binding;

            // Check if URL uses HTTPS and create appropriate binding
            if (serviceUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                binding = new BasicHttpsBinding()
                {
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    SendTimeout = TimeSpan.FromMinutes(5),
                    ReceiveTimeout = TimeSpan.FromMinutes(5)
                };
            }
            else
            {
                binding = new BasicHttpBinding()
                {
                    MaxReceivedMessageSize = 2147483647,
                    MaxBufferSize = 2147483647,
                    SendTimeout = TimeSpan.FromMinutes(5),
                    ReceiveTimeout = TimeSpan.FromMinutes(5)
                };
            }

            var endpoint = new EndpointAddress(serviceUrl);
            _channelFactory = new ChannelFactory<IAppointmentsTienDmSoapService>(binding, endpoint);
            _client = _channelFactory.CreateChannel();
        }

        public async Task<List<AppointmentsTienDm>> GetAllAppointmentsAsync()
        {
            try
            {
                return await _client.GetAppointmentsTienDmsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all appointments: {ex.Message}");
                return new List<AppointmentsTienDm>();
            }
        }

        public async Task<AppointmentsTienDm?> GetAppointmentByIdAsync(int id)
        {
            try
            {
                return await _client.GetAppointmentsTienDmByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointment by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentsTienDm appointment)
        {
            try
            {
                var result = await _client.CreateAppointmentsTienDmAsync(appointment);
                Console.WriteLine($"[Create] SOAP returned: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating appointment: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> UpdateAppointmentAsync(int id, AppointmentsTienDm appointment)
        {
            try
            {
                var result = await _client.UpdateAppointmentsTienDmAsync(id, appointment);
                Console.WriteLine($"[Update] SOAP returned: {result}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating appointment ID {id}: {ex.Message}\n{ex.StackTrace}");
                return false;
            }
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            try
            {
                return await _client.DeleteAppointmentsTienDmAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting appointment ID {id}: {ex.Message}");
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                if (_client is ICommunicationObject communicationObject)
                {
                    if (communicationObject.State == CommunicationState.Faulted)
                    {
                        communicationObject.Abort();
                    }
                    else
                    {
                        communicationObject.Close();
                    }
                }

                _channelFactory?.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disposing client: {ex.Message}");
                _channelFactory?.Abort();
            }
        }
    }
}
