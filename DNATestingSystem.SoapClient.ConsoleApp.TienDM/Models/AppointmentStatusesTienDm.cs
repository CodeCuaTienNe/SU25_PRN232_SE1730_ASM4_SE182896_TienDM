using System.Runtime.Serialization;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models
{
    [DataContract]
    public class AppointmentStatusesTienDm
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
        public bool? IsActive { get; set; }

        public override string ToString()
        {
            return $"ID: {AppointmentStatusesTienDmid}, Name: {StatusName}, Active: {IsActive}";
        }
    }
}
