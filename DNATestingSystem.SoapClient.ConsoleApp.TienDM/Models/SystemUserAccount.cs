using System.Runtime.Serialization;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models
{
    [DataContract]
    public class SystemUserAccount
    {
        [DataMember]
        public int UserAccountId { get; set; }

        [DataMember]
        public string UserName { get; set; } = null!;

        [DataMember]
        public string FullName { get; set; } = null!;

        [DataMember]
        public string Email { get; set; } = null!;

        [DataMember]
        public string Phone { get; set; } = null!;

        [DataMember]
        public string EmployeeCode { get; set; } = null!;

        [DataMember]
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"ID: {UserAccountId}, Username: {UserName}, FullName: {FullName}, Email: {Email}";
        }
    }
}
