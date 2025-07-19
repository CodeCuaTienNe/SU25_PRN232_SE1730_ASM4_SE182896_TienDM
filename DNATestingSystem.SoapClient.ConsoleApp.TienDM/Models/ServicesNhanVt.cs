using System.Runtime.Serialization;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models
{
    [DataContract]
    public class ServicesNhanVt
    {
        [DataMember]
        public int ServicesNhanVtid { get; set; }

        [DataMember]
        public string ServiceName { get; set; } = null!;

        [DataMember]
        public string? ServiceDescription { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"ID: {ServicesNhanVtid}, Name: {ServiceName}, Price: {Price:C}, Active: {IsActive}";
        }
    }
}
