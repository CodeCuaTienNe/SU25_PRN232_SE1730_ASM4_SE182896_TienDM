using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DNATestingSystem.SOAPClient.Razor.TienDM.Services;

namespace DNATestingSystem.SOAPClient.Razor.TienDM
{
    public class CreateModel : PageModel
    {
        private readonly AppointmentsSoapClient _soapClient;

        public CreateModel(AppointmentsSoapClient soapClient)
        {
            _soapClient = soapClient;
        }

        public IActionResult OnGet()
        {
            // Initialize default values
            AppointmentsSoapModel = new AppointmentsSoapModel
            {
                AppointmentDateOnly = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                AppointmentTimeOnly = TimeOnly.FromTimeSpan(new TimeSpan(9, 0, 0)),
                TotalAmount = 100.00m,
                IsPaid = false,
                UserAccountId = 1,
                ServicesNhanVtid = 1,
                AppointmentStatusesTienDmid = 1
            };

            return Page();
        }

        [BindProperty]
        public AppointmentsSoapModel AppointmentsSoapModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Convert DateOnly/TimeOnly to string format for SOAP
                AppointmentsSoapModel.AppointmentDate = AppointmentsSoapModel.AppointmentDateOnly.ToString("yyyy-MM-dd");
                AppointmentsSoapModel.AppointmentTime = AppointmentsSoapModel.AppointmentTimeOnly.ToString("HH:mm:ss");

                var success = await _soapClient.CreateAppointmentAsync(AppointmentsSoapModel);

                if (success)
                {
                    TempData["SuccessMessage"] = "Appointment created successfully!";
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create appointment. Please try again.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating appointment: {ex.Message}");
                return Page();
            }
        }
    }
}
