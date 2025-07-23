
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestingSystem.SOAPClient.Razor.TienDM.Services;

namespace DNATestingSystem.SOAPClient.Razor.TienDM
{
    public class DetailsModel : PageModel
    {
        private readonly AppointmentsSoapClient _soapClient;

        public DetailsModel(AppointmentsSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        public AppointmentsSoapModel? AppointmentsTienDm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _soapClient.GetAppointmentByIdAsync(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }
            AppointmentsTienDm = appointment;
            return Page();
        }
    }
}
