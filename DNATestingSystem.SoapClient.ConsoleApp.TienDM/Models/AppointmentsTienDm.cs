using System.Runtime.Serialization;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models
{
    [DataContract]
    public class AppointmentsTienDm
    {
        [DataMember]
        public int AppointmentsTienDmid { get; set; }

        [DataMember]
        public int UserAccountId { get; set; }

        [DataMember]
        public int ServicesNhanVtid { get; set; }

        [DataMember]
        public int AppointmentStatusesTienDmid { get; set; }

        [DataMember]
        public DateTime? AppointmentDate { get; set; }

        [DataMember]
        public TimeSpan? AppointmentTime { get; set; }

        [DataMember]
        public string SamplingMethod { get; set; } = null!;

        [DataMember]
        public string? Address { get; set; }

        [DataMember]
        public string ContactPhone { get; set; } = null!;

        [DataMember]
        public string? Notes { get; set; }

        [DataMember]
        public DateTime? CreatedDate { get; set; }

        [DataMember]
        public DateTime? ModifiedDate { get; set; }

        [DataMember]
        public decimal TotalAmount { get; set; }

        [DataMember]
        public bool? IsPaid { get; set; }

        [DataMember]
        public virtual AppointmentStatusesTienDm? AppointmentStatusesTienDm { get; set; }

        [DataMember]
        public virtual ServicesNhanVt? ServicesNhanVt { get; set; }

        [DataMember]
        public virtual SystemUserAccount? UserAccount { get; set; }

        public override string ToString()
        {
            return $"ID: {AppointmentsTienDmid}, User: {UserAccountId}, Service: {ServicesNhanVtid}, " +
                   $"Date: {AppointmentDate}, Time: {AppointmentTime}, Method: {SamplingMethod}, " +
                   $"Phone: {ContactPhone}, Amount: {TotalAmount:C}, Paid: {IsPaid}";
        }
    }
}
