using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestingSystem.SOAPClient.Razor.TienDM.Services;

namespace DNATestingSystem.SOAPClient.Razor.TienDM
{
    public class DeleteModel : PageModel
    {
        private readonly AppointmentsSoapClient _soapClient;

        public DeleteModel(AppointmentsSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        [BindProperty]
        public AppointmentsSoapModel? AppointmentsSoapModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var appointment = await _soapClient.GetAppointmentByIdAsync(id.Value);
            if (appointment == null)
                return NotFound();

            AppointmentsSoapModel = appointment;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var success = await _soapClient.DeleteAppointmentAsync(id.Value);
            if (success)
            {
                TempData["SuccessMessage"] = "Appointment deleted successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete appointment.";
            }
            return RedirectToPage("./Index");
        }
    }
}
