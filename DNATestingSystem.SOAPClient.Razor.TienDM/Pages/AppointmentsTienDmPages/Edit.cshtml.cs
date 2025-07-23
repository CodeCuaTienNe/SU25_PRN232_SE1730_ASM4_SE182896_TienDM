using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DNATestingSystem.SOAPClient.Razor.TienDM.Services;

namespace DNATestingSystem.SOAPClient.Razor.TienDM
{
    public class EditModel : PageModel
    {
        private readonly AppointmentsSoapClient _soapClient;

        public EditModel(AppointmentsSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        [BindProperty]
        public AppointmentsSoapModel AppointmentsSoapModel { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Convert DateOnly/TimeOnly to string for SOAP
            AppointmentsSoapModel.AppointmentDate = AppointmentsSoapModel.AppointmentDateOnly.ToString("yyyy-MM-dd");
            AppointmentsSoapModel.AppointmentTime = AppointmentsSoapModel.AppointmentTimeOnly.ToString("HH:mm:ss");

            var success = await _soapClient.UpdateAppointmentAsync(AppointmentsSoapModel.AppointmentsTienDmid, AppointmentsSoapModel);
            if (success)
            {
                TempData["SuccessMessage"] = "Appointment updated successfully!";
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError("", "Failed to update appointment. Please try again.");
                return Page();
            }
        }
    }
}
