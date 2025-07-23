using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestingSystem.SOAPClient.Razor.TienDM.Services;

namespace DNATestingSystem.SOAPClient.Razor.TienDM
{
    public class IndexModel : PageModel
    {
        private readonly AppointmentsSoapClient _soapClient;

        public IndexModel(AppointmentsSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        public IList<AppointmentsSoapModel> AppointmentsTienDm { get; set; } = default!;

        public string ErrorMessage { get; set; } = "";

        public async Task OnGetAsync()
        {
            try
            {
                var appointments = await _soapClient.GetAllAppointmentsAsync();
                AppointmentsTienDm = appointments ?? new List<AppointmentsSoapModel>();

                if (AppointmentsTienDm.Count == 0)
                {
                    ErrorMessage = "No appointments found or service is not available.";
                }
            }
            catch (Exception ex)
            {
                // Log error and show empty list
                ErrorMessage = $"Error loading appointments: {ex.Message}";
                AppointmentsTienDm = new List<AppointmentsSoapModel>();
            }
        }
    }
}
