using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DNATestingSystem.Repository.TienDM.Models;
using DNATestingSystem.Repository.TienDM.Validation;

namespace DNATestingSystem.SoapAPIServices.TienDM.SoapModels;

[DataContract(Namespace = "http://tempuri.org/")]
public partial class AppointmentsTienDm
{
    [DataMember(Order = 0)]
    public int AppointmentsTienDmid { get; set; }

    [DataMember(Order = 1)]
    [Required(ErrorMessage = "User Account is required")]
    [Display(Name = "User Account")]
    public int UserAccountId { get; set; }

    [DataMember(Order = 2)]
    [Required(ErrorMessage = "Service selection is required")]
    [Display(Name = "Service")]
    public int ServicesNhanVtid { get; set; }

    [DataMember(Order = 3)]
    [Required(ErrorMessage = "Appointment status is required")]
    [Display(Name = "Appointment Status")]
    public int AppointmentStatusesTienDmid { get; set; }

    [DataMember(Order = 4)]
    [Required(ErrorMessage = "Appointment date is required")]
    [Display(Name = "Appointment Date")]
    public string AppointmentDate { get; set; } = "";

    [DataMember(Order = 5)]
    [Required(ErrorMessage = "Appointment time is required")]
    [Display(Name = "Appointment Time")]
    public string AppointmentTime { get; set; } = "";

    [DataMember(Order = 6)]
    [Required(ErrorMessage = "Sampling method is required")]
    public string SamplingMethod { get; set; } = "";

    [DataMember(Order = 7)]
    [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
    [Display(Name = "Address")]
    public string? Address { get; set; }

    [DataMember(Order = 8)]
    [Required(ErrorMessage = "Contact phone is required")]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    [StringLength(15, MinimumLength = 10, ErrorMessage = "Contact phone must be between 10 and 15 characters")]
    [Display(Name = "Contact Phone")]
    public string ContactPhone { get; set; } = "";

    [DataMember(Order = 9)]
    [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
    [Display(Name = "Notes")]
    public string? Notes { get; set; }

    [DataMember(Order = 10)]
    [Display(Name = "Created Date")]
    public DateTime? CreatedDate { get; set; }

    [DataMember(Order = 11)]
    [Display(Name = "Modified Date")]
    public DateTime? ModifiedDate { get; set; }

    [DataMember(Order = 12)]
    [Required(ErrorMessage = "Total amount is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Total amount must be between 0.01 and 999,999.99")]
    [DataType(DataType.Currency)]
    [Display(Name = "Total Amount")]
    public decimal TotalAmount { get; set; }

    [DataMember(Order = 13)]
    [Display(Name = "Is Paid")]
    public bool? IsPaid { get; set; }

    //[DataMember]
    //[JsonIgnore]
    //public virtual AppointmentStatusesTienDm? AppointmentStatusesTienDm { get; set; }

    //[DataMember]
    //[JsonIgnore]
    //public virtual ICollection<SampleThinhLc> SampleThinhLcs { get; set; } = new List<SampleThinhLc>();

    //[DataMember]
    //[JsonIgnore]
    //public virtual ServicesNhanVt? ServicesNhanVt { get; set; }

    //[DataMember]
    //[JsonIgnore]
    //public virtual SystemUserAccount? UserAccount { get; set; }

}
