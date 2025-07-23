using System.ComponentModel.DataAnnotations;

namespace DNATestingSystem.SOAPClient.Razor.TienDM.Services
{
    public class AppointmentsSoapModel
    {
        public int AppointmentsTienDmid { get; set; }

        [Required(ErrorMessage = "User Account is required")]
        [Display(Name = "User Account")]
        public int UserAccountId { get; set; }

        [Required(ErrorMessage = "Service selection is required")]
        [Display(Name = "Service")]
        public int ServicesNhanVtid { get; set; }

        [Required(ErrorMessage = "Appointment status is required")]
        [Display(Name = "Appointment Status")]
        public int AppointmentStatusesTienDmid { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public string AppointmentDate { get; set; } = "";

        [Required(ErrorMessage = "Appointment time is required")]
        [Display(Name = "Appointment Time")]
        [DataType(DataType.Time)]
        public string AppointmentTime { get; set; } = "";

        [Required(ErrorMessage = "Sampling method is required")]
        [Display(Name = "Sampling Method")]
        public string SamplingMethod { get; set; } = "";

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Contact phone is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Contact Phone")]
        public string ContactPhone { get; set; } = "";

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Total amount must be between 0.01 and 999,999.99")]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Is Paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties for display
        public string? StatusName { get; set; }
        public string? ServiceName { get; set; }
        public string? UserEmail { get; set; }

        // Helper properties for form input
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateOnly AppointmentDateOnly
        {
            get => DateTime.TryParse(AppointmentDate, out var date) ? DateOnly.FromDateTime(date) : DateOnly.FromDateTime(DateTime.Now);
            set => AppointmentDate = value.ToString("yyyy-MM-dd");
        }

        [DataType(DataType.Time)]
        [Display(Name = "Appointment Time")]
        public TimeOnly AppointmentTimeOnly
        {
            get => TimeSpan.TryParse(AppointmentTime, out var time) ? TimeOnly.FromTimeSpan(time) : TimeOnly.FromTimeSpan(new TimeSpan(9, 0, 0));
            set => AppointmentTime = value.ToString("HH:mm:ss");
        }
    }
}
