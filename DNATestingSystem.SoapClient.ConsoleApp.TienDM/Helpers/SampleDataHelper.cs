using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM.Helpers
{
    public static class SampleDataHelper
    {
        public static AppointmentsTienDm CreateSampleAppointment()
        {
            return new AppointmentsTienDm
            {
                UserAccountId = 1,
                ServicesNhanVtid = 1,
                AppointmentStatusesTienDmid = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                AppointmentTime = DateTime.Now.AddHours(2).TimeOfDay,
                SamplingMethod = "Blood Sample",
                Address = "123 Main Street, Hanoi",
                ContactPhone = "0123456789",
                Notes = "Sample appointment created by SOAP client",
                TotalAmount = 500000,
                IsPaid = false,
                CreatedDate = DateTime.Now
            };
        }

        public static List<AppointmentsTienDm> CreateMultipleSampleAppointments(int count = 3)
        {
            var appointments = new List<AppointmentsTienDm>();
            var random = new Random();
            var samplingMethods = new[] { "Blood Sample", "Saliva Sample", "Hair Sample", "Tissue Sample" };
            var addresses = new[]
            {
                "123 Main Street, Hanoi",
                "456 Second Avenue, Ho Chi Minh City",
                "789 Third Boulevard, Da Nang",
                "321 Fourth Street, Hue"
            };

            for (int i = 0; i < count; i++)
            {
                appointments.Add(new AppointmentsTienDm
                {
                    UserAccountId = random.Next(1, 5),
                    ServicesNhanVtid = random.Next(1, 4),
                    AppointmentStatusesTienDmid = random.Next(1, 3),
                    AppointmentDate = DateTime.Now.AddDays(random.Next(1, 30)),
                    AppointmentTime = DateTime.Now.AddHours(random.Next(1, 12)).TimeOfDay,
                    SamplingMethod = samplingMethods[random.Next(samplingMethods.Length)],
                    Address = addresses[random.Next(addresses.Length)],
                    ContactPhone = $"0{random.Next(100000000, 999999999)}",
                    Notes = $"Test appointment #{i + 1} created by SOAP client",
                    TotalAmount = random.Next(100000, 1000000),
                    IsPaid = random.Next(0, 2) == 1,
                    CreatedDate = DateTime.Now.AddMinutes(-random.Next(0, 1440))
                });
            }

            return appointments;
        }
    }
}
