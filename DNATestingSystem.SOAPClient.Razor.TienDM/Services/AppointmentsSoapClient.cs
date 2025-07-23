using System.ServiceModel;
using System.Xml;

namespace DNATestingSystem.SOAPClient.Razor.TienDM.Services
{
    public class AppointmentsSoapClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _serviceUrl;

        public AppointmentsSoapClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _serviceUrl = configuration.GetValue<string>("SoapServiceUrl") ?? "https://localhost:7077/AppointmentsTienDmSoapService.asmx";
        }

        public async Task<List<AppointmentsSoapModel>> GetAllAppointmentsAsync()
        {
            var soapEnvelope = @"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <GetAppointmentsTienDms xmlns=""http://tempuri.org/"">
    </GetAppointmentsTienDms>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IAppointmentsTienDmSoapService/GetAppointmentsTienDmsAsync");

            try
            {
                var response = await _httpClient.PostAsync(_serviceUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                // Debug: Log the raw response
                Console.WriteLine($"SOAP Response Status: {response.StatusCode}");
                Console.WriteLine($"SOAP Response Content: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"SOAP service returned error: {response.StatusCode} - {responseContent}");
                }

                return ParseAppointmentsFromSoapResponse(responseContent);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to connect to SOAP service at {_serviceUrl}: {ex.Message}");
            }
        }

        public async Task<AppointmentsSoapModel?> GetAppointmentByIdAsync(int id)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <GetAppointmentsTienDmById xmlns=""http://tempuri.org/"">
      <id>{id}</id>
    </GetAppointmentsTienDmById>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IAppointmentsTienDmSoapService/GetAppointmentsTienDmByIdAsync");

            var response = await _httpClient.PostAsync(_serviceUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var appointments = ParseAppointmentsFromSoapResponse(responseContent);
            return appointments.FirstOrDefault();
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentsSoapModel appointment)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
               xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <CreateAppointmentsTienDm xmlns=""http://tempuri.org/"">
      <appointment>
        <UserAccountId>{appointment.UserAccountId}</UserAccountId>
        <ServicesNhanVtid>{appointment.ServicesNhanVtid}</ServicesNhanVtid>
        <AppointmentStatusesTienDmid>{appointment.AppointmentStatusesTienDmid}</AppointmentStatusesTienDmid>
        <AppointmentDate>{appointment.AppointmentDate}</AppointmentDate>
        <AppointmentTime>{appointment.AppointmentTime}</AppointmentTime>
        <SamplingMethod>{System.Security.SecurityElement.Escape(appointment.SamplingMethod)}</SamplingMethod>
        <Address>{System.Security.SecurityElement.Escape(appointment.Address ?? "")}</Address>
        <ContactPhone>{appointment.ContactPhone}</ContactPhone>
        <Notes>{System.Security.SecurityElement.Escape(appointment.Notes ?? "")}</Notes>
        <TotalAmount>{appointment.TotalAmount}</TotalAmount>
        <IsPaid>{appointment.IsPaid.ToString().ToLower()}</IsPaid>
      </appointment>
    </CreateAppointmentsTienDm>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IAppointmentsTienDmSoapService/CreateAppointmentsTienDmAsync");

            var response = await _httpClient.PostAsync(_serviceUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return ParseBooleanFromSoapResponse(responseContent);
        }

        public async Task<bool> UpdateAppointmentAsync(int id, AppointmentsSoapModel appointment)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
               xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <UpdateAppointmentsTienDm xmlns=""http://tempuri.org/"">
      <id>{id}</id>
      <appointment>
        <UserAccountId>{appointment.UserAccountId}</UserAccountId>
        <ServicesNhanVtid>{appointment.ServicesNhanVtid}</ServicesNhanVtid>
        <AppointmentStatusesTienDmid>{appointment.AppointmentStatusesTienDmid}</AppointmentStatusesTienDmid>
        <AppointmentDate>{appointment.AppointmentDate}</AppointmentDate>
        <AppointmentTime>{appointment.AppointmentTime}</AppointmentTime>
        <SamplingMethod>{System.Security.SecurityElement.Escape(appointment.SamplingMethod)}</SamplingMethod>
        <Address>{System.Security.SecurityElement.Escape(appointment.Address ?? "")}</Address>
        <ContactPhone>{appointment.ContactPhone}</ContactPhone>
        <Notes>{System.Security.SecurityElement.Escape(appointment.Notes ?? "")}</Notes>
        <TotalAmount>{appointment.TotalAmount}</TotalAmount>
        <IsPaid>{appointment.IsPaid.ToString().ToLower()}</IsPaid>
      </appointment>
    </UpdateAppointmentsTienDm>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IAppointmentsTienDmSoapService/UpdateAppointmentsTienDmAsync");

            var response = await _httpClient.PostAsync(_serviceUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return ParseBooleanFromSoapResponse(responseContent);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <DeleteAppointmentsTienDm xmlns=""http://tempuri.org/"">
      <id>{id}</id>
    </DeleteAppointmentsTienDm>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, System.Text.Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://tempuri.org/IAppointmentsTienDmSoapService/DeleteAppointmentsTienDmAsync");

            var response = await _httpClient.PostAsync(_serviceUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return ParseBooleanFromSoapResponse(responseContent);
        }

        private List<AppointmentsSoapModel> ParseAppointmentsFromSoapResponse(string soapResponse)
        {
            var appointments = new List<AppointmentsSoapModel>();

            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(soapResponse);

                // Try to get all AppointmentsTienDm nodes (GetAll)
                var appointmentNodes = doc.GetElementsByTagName("AppointmentsTienDm");
                if (appointmentNodes != null && appointmentNodes.Count > 0)
                {
                    foreach (XmlNode node in appointmentNodes)
                    {
                        var appointment = new AppointmentsSoapModel();
                        appointment.AppointmentsTienDmid = GetIntValue(node, "AppointmentsTienDmid");
                        appointment.UserAccountId = GetIntValue(node, "UserAccountId");
                        appointment.ServicesNhanVtid = GetIntValue(node, "ServicesNhanVtid");
                        appointment.AppointmentStatusesTienDmid = GetIntValue(node, "AppointmentStatusesTienDmid");
                        appointment.AppointmentDate = GetStringValue(node, "AppointmentDate");
                        appointment.AppointmentTime = GetStringValue(node, "AppointmentTime");
                        appointment.SamplingMethod = GetStringValue(node, "SamplingMethod");
                        appointment.Address = GetStringValue(node, "Address");
                        appointment.ContactPhone = GetStringValue(node, "ContactPhone");
                        appointment.Notes = GetStringValue(node, "Notes");
                        appointment.TotalAmount = GetDecimalValue(node, "TotalAmount");
                        appointment.IsPaid = GetBoolValue(node, "IsPaid");
                        appointment.CreatedDate = GetDateTimeValue(node, "CreatedDate");
                        appointment.ModifiedDate = GetDateTimeValue(node, "ModifiedDate");
                        appointments.Add(appointment);
                    }
                }
                else
                {
                    // Try to get single result node (GetById)
                    var singleNode = doc.GetElementsByTagName("GetAppointmentsTienDmByIdResult");
                    if (singleNode != null && singleNode.Count > 0)
                    {
                        var node = singleNode[0];
                        var appointment = new AppointmentsSoapModel();
                        appointment.AppointmentsTienDmid = GetIntValue(node, "AppointmentsTienDmid");
                        appointment.UserAccountId = GetIntValue(node, "UserAccountId");
                        appointment.ServicesNhanVtid = GetIntValue(node, "ServicesNhanVtid");
                        appointment.AppointmentStatusesTienDmid = GetIntValue(node, "AppointmentStatusesTienDmid");
                        appointment.AppointmentDate = GetStringValue(node, "AppointmentDate");
                        appointment.AppointmentTime = GetStringValue(node, "AppointmentTime");
                        appointment.SamplingMethod = GetStringValue(node, "SamplingMethod");
                        appointment.Address = GetStringValue(node, "Address");
                        appointment.ContactPhone = GetStringValue(node, "ContactPhone");
                        appointment.Notes = GetStringValue(node, "Notes");
                        appointment.TotalAmount = GetDecimalValue(node, "TotalAmount");
                        appointment.IsPaid = GetBoolValue(node, "IsPaid");
                        appointment.CreatedDate = GetDateTimeValue(node, "CreatedDate");
                        appointment.ModifiedDate = GetDateTimeValue(node, "ModifiedDate");
                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing SOAP response: {ex.Message}");
            }
            return appointments;
        }

        private bool ParseBooleanFromSoapResponse(string soapResponse)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(soapResponse);

                var resultNode = doc.SelectSingleNode("//*[contains(local-name(), 'Result')]");
                if (resultNode != null && bool.TryParse(resultNode.InnerText, out bool result))
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing boolean SOAP response: {ex.Message}");
            }

            return false;
        }

        private int GetIntValue(XmlNode node, string elementName)
        {
            var element = node[elementName];
            return element != null && int.TryParse(element.InnerText, out int value) ? value : 0;
        }

        private string GetStringValue(XmlNode node, string elementName)
        {
            var element = node[elementName];
            return element?.InnerText ?? "";
        }

        private decimal GetDecimalValue(XmlNode node, string elementName)
        {
            var element = node[elementName];
            return element != null && decimal.TryParse(element.InnerText, out decimal value) ? value : 0m;
        }

        private bool GetBoolValue(XmlNode node, string elementName)
        {
            var element = node[elementName];
            return element != null && bool.TryParse(element.InnerText, out bool value) && value;
        }

        private DateTime? GetDateTimeValue(XmlNode node, string elementName)
        {
            var element = node[elementName];
            return element != null && DateTime.TryParse(element.InnerText, out DateTime value) ? value : null;
        }

        public void Dispose()
        {
            // HttpClient will be disposed by DI container
        }
    }
}
