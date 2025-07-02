using DNATestingSystem.Repository.TienDM.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DNATestingSystem.SoapAPIServices.TienDM.SoapModels;

[DataContract]
public partial class SystemUserAccount
{
    [DataMember]
    public int UserAccountId { get; set; }

    [DataMember]
    public string UserName { get; set; } = null!;

    [DataMember]
    public string Password { get; set; } = null!;

    [DataMember]
    public string FullName { get; set; } = null!;

    [DataMember]
    public string Email { get; set; } = null!;

    [DataMember]
    public string Phone { get; set; } = null!;

    [DataMember]
    public string EmployeeCode { get; set; } = null!;

    [DataMember]
    public int RoleId { get; set; }

    [DataMember]
    public string? RequestCode { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public string? ApplicationCode { get; set; }

    [DataMember]
    public string? CreatedBy { get; set; }

    [DataMember]
    public DateTime? ModifiedDate { get; set; }

    [DataMember]
    public string? ModifiedBy { get; set; }

    [DataMember]
    public bool IsActive { get; set; }

    // Keep JsonIgnore to avoid circular reference when serializing collections
    [DataMember]
    [JsonIgnore]
    public virtual ICollection<AppointmentsTienDm> AppointmentsTienDms { get; set; } = new List<AppointmentsTienDm>();

    [DataMember]
    [JsonIgnore]
    public virtual ICollection<BlogsHuyLhg> BlogsHuyLhgs { get; set; } = new List<BlogsHuyLhg>();

    [DataMember]
    [JsonIgnore]
    public virtual ICollection<OrderGiapHd> OrderGiapHds { get; set; } = new List<OrderGiapHd>();

    [DataMember]
    [JsonIgnore]
    public virtual ICollection<ProfileThinhLc> ProfileThinhLcs { get; set; } = new List<ProfileThinhLc>();

    [DataMember]
    [JsonIgnore]
    public virtual ICollection<ServicesNhanVt> ServicesNhanVts { get; set; } = new List<ServicesNhanVt>();
}
