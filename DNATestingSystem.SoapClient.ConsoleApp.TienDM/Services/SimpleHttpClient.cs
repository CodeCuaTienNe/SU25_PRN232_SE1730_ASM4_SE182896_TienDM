using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models;
using System.Text.Json;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services
{
    public class SimpleHttpClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public SimpleHttpClient(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _httpClient = new HttpClient();
        }

        public async Task<List<AppointmentsTienDm>> GetAllAppointmentsAsync()
        {
            try
            {
                string url = $"{_baseUrl}/AppointmentsTienDmSoapService.asmx/GetAppointmentsTienDmsAsync";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = await response.Content.ReadAsStringAsync();
                    return ParseAppointmentsFromXml(xmlContent);
                }

                Console.WriteLine($"Error: {response.StatusCode}");
                return new List<AppointmentsTienDm>();
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
                string url = $"{_baseUrl}/AppointmentsTienDmSoapService.asmx/GetAppointmentsTienDmByIdAsync?id={id}";
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = await response.Content.ReadAsStringAsync();
                    var appointments = ParseAppointmentsFromXml(xmlContent);
                    return appointments.FirstOrDefault();
                }

                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointment by ID {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            try
            {
                string url = $"{_baseUrl}/AppointmentsTienDmSoapService.asmx/DeleteAppointmentsTienDmAsync";

                var formData = new List<KeyValuePair<string, string>>
                {
                    new("id", id.ToString())
                };

                var response = await _httpClient.PostAsync(url, new FormUrlEncodedContent(formData));
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting appointment {id}: {ex.Message}");
                return false;
            }
        }

        private List<AppointmentsTienDm> ParseAppointmentsFromXml(string xmlContent)
        {
            var appointments = new List<AppointmentsTienDm>();

            try
            {
                // Simple XML parsing - this is a basic implementation
                // In production, you would use XDocument or XmlDocument

                if (xmlContent.Contains("<d2p1:AppointmentsTienDm>"))
                {
                    var appointmentBlocks = xmlContent.Split(new string[] { "<d2p1:AppointmentsTienDm>" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var block in appointmentBlocks.Skip(1))
                    {
                        if (block.Contains("</d2p1:AppointmentsTienDm>"))
                        {
                            var appointment = ParseSingleAppointment(block);
                            if (appointment != null)
                                appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing XML: {ex.Message}");
            }

            return appointments;
        }

        private AppointmentsTienDm? ParseSingleAppointment(string xmlBlock)
        {
            try
            {
                var appointment = new AppointmentsTienDm();

                appointment.AppointmentsTienDmid = ExtractIntValue(xmlBlock, "d2p1:AppointmentsTienDmid");
                appointment.UserAccountId = ExtractIntValue(xmlBlock, "d2p1:UserAccountId");
                appointment.ServicesNhanVtid = ExtractIntValue(xmlBlock, "d2p1:ServicesNhanVtid");
                appointment.AppointmentStatusesTienDmid = ExtractIntValue(xmlBlock, "d2p1:AppointmentStatusesTienDmid");
                appointment.Address = ExtractStringValue(xmlBlock, "d2p1:Address");
                appointment.ContactPhone = ExtractStringValue(xmlBlock, "d2p1:ContactPhone");
                appointment.SamplingMethod = ExtractStringValue(xmlBlock, "d2p1:SamplingMethod");
                appointment.Notes = ExtractStringValue(xmlBlock, "d2p1:Notes");
                appointment.TotalAmount = ExtractDecimalValue(xmlBlock, "d2p1:TotalAmount");
                appointment.IsPaid = ExtractBoolValue(xmlBlock, "d2p1:IsPaid");
                appointment.CreatedDate = ExtractDateTimeValue(xmlBlock, "d2p1:CreatedDate");
                appointment.ModifiedDate = ExtractDateTimeValue(xmlBlock, "d2p1:ModifiedDate");

                return appointment;
            }
            catch
            {
                return null;
            }
        }

        private int ExtractIntValue(string xml, string tagName)
        {
            var startTag = $"<{tagName}>";
            var endTag = $"</{tagName}>";

            var startIndex = xml.IndexOf(startTag);
            if (startIndex == -1) return 0;

            startIndex += startTag.Length;
            var endIndex = xml.IndexOf(endTag, startIndex);
            if (endIndex == -1) return 0;

            var value = xml.Substring(startIndex, endIndex - startIndex);
            return int.TryParse(value, out int result) ? result : 0;
        }

        private string ExtractStringValue(string xml, string tagName)
        {
            var startTag = $"<{tagName}>";
            var endTag = $"</{tagName}>";

            var startIndex = xml.IndexOf(startTag);
            if (startIndex == -1) return string.Empty;

            startIndex += startTag.Length;
            var endIndex = xml.IndexOf(endTag, startIndex);
            if (endIndex == -1) return string.Empty;

            return xml.Substring(startIndex, endIndex - startIndex);
        }

        private decimal ExtractDecimalValue(string xml, string tagName)
        {
            var value = ExtractStringValue(xml, tagName);
            return decimal.TryParse(value, out decimal result) ? result : 0;
        }

        private bool ExtractBoolValue(string xml, string tagName)
        {
            var value = ExtractStringValue(xml, tagName);
            return bool.TryParse(value, out bool result) && result;
        }

        private DateTime? ExtractDateTimeValue(string xml, string tagName)
        {
            var value = ExtractStringValue(xml, tagName);
            if (string.IsNullOrEmpty(value) || value.Contains("i:nil=\"true\""))
                return null;

            return DateTime.TryParse(value, out DateTime result) ? result : null;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
