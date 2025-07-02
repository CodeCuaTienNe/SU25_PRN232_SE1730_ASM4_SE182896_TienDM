using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DNATestingSystem.SoapAPIServices.TienDM.SoapModels;

[DataContract]
public partial class AppointmentStatusesTienDm
{
    [DataMember]
    public int AppointmentStatusesTienDmid { get; set; }
    [DataMember]
    public string StatusName { get; set; } = null!;
    [DataMember]
    public string? Description { get; set; }
    [DataMember]
    public DateTime? CreatedDate { get; set; }
    [DataMember]
    public bool? IsActive { get; set; }    // Keep JsonIgnore to avoid circular reference when serializing appointments collection
    [DataMember]
    [JsonIgnore]
    public virtual ICollection<AppointmentsTienDm> AppointmentsTienDms { get; set; } = new List<AppointmentsTienDm>();
}
