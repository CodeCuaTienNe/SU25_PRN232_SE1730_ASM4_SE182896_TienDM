using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM
{
    class SimpleTest
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("    Simple SOAP Client Test");
            Console.WriteLine("===========================================");

            string serviceUrl = "https://localhost:7077/AppointmentsTienDmSoapService.asmx";
            Console.WriteLine($"Testing connection to: {serviceUrl}");

            try
            {
                using var client = new AppointmentsSoapClient(serviceUrl);
                Console.WriteLine("Connected successfully!");

                Console.WriteLine("Calling GetAllAppointments...");
                var appointments = await client.GetAllAppointmentsAsync();

                Console.WriteLine($"Retrieved {appointments.Count} appointments:");
                foreach (var appointment in appointments.Take(3)) // Show first 3
                {
                    Console.WriteLine($"- ID: {appointment.AppointmentsTienDmid}");
                    Console.WriteLine($"  Address: {appointment.Address}");
                    Console.WriteLine($"  Phone: {appointment.ContactPhone}");
                    Console.WriteLine($"  Amount: {appointment.TotalAmount:C}");
                    Console.WriteLine($"  Paid: {appointment.IsPaid}");
                    Console.WriteLine();
                }

                Console.WriteLine("Test completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
