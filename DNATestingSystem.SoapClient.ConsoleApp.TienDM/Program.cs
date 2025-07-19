using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Models;
using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Services;
using DNATestingSystem.SoapClient.ConsoleApp.TienDM.Helpers;

namespace DNATestingSystem.SoapClient.ConsoleApp.TienDM
{
    class Program
    {
        private static readonly string ServiceUrlHttps = "https://localhost:7077/AppointmentsTienDmSoapService.asmx";
        private static readonly string ServiceUrlHttp = "http://localhost:5077/AppointmentsTienDmSoapService.asmx"; 
       static async Task Main(string[] args)
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("    DNA Testing System SOAP Client");
            Console.WriteLine("===========================================");

            // Simple test mode
            if (args.Length > 0 && args[0] == "--test")
            {
                await RunSimpleTest();
                return;
            }

            // Let user choose protocol and method
            Console.WriteLine("Choose protocol:");
            Console.WriteLine("1. HTTPS SOAP (Default)");
            Console.WriteLine("2. HTTP SOAP");
            Console.WriteLine("3. HTTPS Simple HTTP");
            Console.WriteLine("4. HTTP Simple HTTP");
            Console.Write("Enter choice (1-4): ");

            string protocolChoice = Console.ReadLine() ?? "1";

            bool useSimpleHttp = protocolChoice == "3" || protocolChoice == "4";
            bool useHttps = protocolChoice == "1" || protocolChoice == "3";

            string serviceUrl = useHttps ? ServiceUrlHttps : ServiceUrlHttp;
            serviceUrl = serviceUrl.Replace("/AppointmentsTienDmSoapService.asmx", ""); // Remove .asmx for simple HTTP

            Console.WriteLine($"Service URL: {serviceUrl}");
            Console.WriteLine($"Method: {(useSimpleHttp ? "Simple HTTP" : "SOAP")}");
            Console.WriteLine();

            if (useSimpleHttp)
            {
                await RunSimpleHttpMode(serviceUrl);
            }
            else
            {
                await RunSoapMode(serviceUrl + "/AppointmentsTienDmSoapService.asmx");
            }
        }

        static async Task RunSoapMode(string serviceUrl)
        {
            using var client = new AppointmentsSoapClient(serviceUrl);

            bool running = true;
            while (running)
            {
                DisplayMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await GetAllAppointments(client);
                            break;
                        case "2":
                            await GetAppointmentById(client);
                            break;
                        case "3":
                            await CreateAppointment(client);
                            break;
                        case "4":
                            await CreateMultipleAppointments(client);
                            break;
                        case "5":
                            await UpdateAppointment(client);
                            break;
                        case "6":
                            await DeleteAppointment(client);
                            break;
                        case "7":
                            await TestAllOperations(client);
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (running)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Thank you for using DNA Testing System SOAP Client!");
        }

        static void DisplayMenu()
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get All Appointments");
            Console.WriteLine("2. Get Appointment by ID");
            Console.WriteLine("3. Create New Appointment");
            Console.WriteLine("4. Create Multiple Sample Appointments");
            Console.WriteLine("5. Update Appointment");
            Console.WriteLine("6. Delete Appointment");
            Console.WriteLine("7. Test All Operations (Demo)");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        static async Task GetAllAppointments(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Getting All Appointments ---");
            var appointments = await client.GetAllAppointmentsAsync();

            if (appointments.Any())
            {
                Console.WriteLine($"Found {appointments.Count} appointments:");
                foreach (var appointment in appointments)
                {
                    Console.WriteLine($"  {appointment}");
                }
            }
            else
            {
                Console.WriteLine("No appointments found.");
            }
        }

        static async Task GetAppointmentById(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Get Appointment by ID ---");
            Console.Write("Enter Appointment ID: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var appointment = await client.GetAppointmentByIdAsync(id);
                if (appointment != null)
                {
                    Console.WriteLine($"Found appointment: {appointment}");
                }
                else
                {
                    Console.WriteLine($"Appointment with ID {id} not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static async Task CreateAppointment(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Create New Appointment ---");

            var appointment = new AppointmentsTienDm();

            Console.Write("Enter User Account ID: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
                appointment.UserAccountId = userId;
            else
            {
                Console.WriteLine("Invalid User Account ID. Using default (1).");
                appointment.UserAccountId = 1;
            }

            Console.Write("Enter Service ID: ");
            if (int.TryParse(Console.ReadLine(), out int serviceId))
                appointment.ServicesNhanVtid = serviceId;
            else
            {
                Console.WriteLine("Invalid Service ID. Using default (1).");
                appointment.ServicesNhanVtid = 1;
            }

            Console.Write("Enter Appointment Status ID: ");
            if (int.TryParse(Console.ReadLine(), out int statusId))
                appointment.AppointmentStatusesTienDmid = statusId;
            else
            {
                Console.WriteLine("Invalid Status ID. Using default (1).");
                appointment.AppointmentStatusesTienDmid = 1;
            }

            Console.Write("Enter Sampling Method: ");
            appointment.SamplingMethod = Console.ReadLine() ?? "Blood Sample";

            Console.Write("Enter Address: ");
            appointment.Address = Console.ReadLine();

            Console.Write("Enter Contact Phone: ");
            appointment.ContactPhone = Console.ReadLine() ?? "0123456789";

            Console.Write("Enter Notes: ");
            appointment.Notes = Console.ReadLine();

            Console.Write("Enter Total Amount: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                appointment.TotalAmount = amount;
            else
            {
                Console.WriteLine("Invalid amount. Using default (500000).");
                appointment.TotalAmount = 500000;
            }

            // Set default values
            appointment.AppointmentDate = DateTime.Now.AddDays(1);
            appointment.AppointmentTime = DateTime.Now.AddHours(2).TimeOfDay;
            appointment.IsPaid = false;
            appointment.CreatedDate = DateTime.Now;

            var result = await client.CreateAppointmentAsync(appointment);
            if (result != null)
            {
                Console.WriteLine($"Appointment created successfully: {result}");
            }
            else
            {
                Console.WriteLine("Failed to create appointment.");
            }
        }

        static async Task CreateMultipleAppointments(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Create Multiple Sample Appointments ---");
            Console.Write("Enter number of appointments to create (default 3): ");

            int count = 3;
            if (int.TryParse(Console.ReadLine(), out int inputCount) && inputCount > 0)
                count = inputCount;

            var appointments = SampleDataHelper.CreateMultipleSampleAppointments(count);

            Console.WriteLine($"Creating {count} sample appointments...");

            int successCount = 0;
            foreach (var appointment in appointments)
            {
                var result = await client.CreateAppointmentAsync(appointment);
                if (result != null)
                {
                    Console.WriteLine($"✓ Created: {result}");
                    successCount++;
                }
                else
                {
                    Console.WriteLine($"✗ Failed to create appointment");
                }

                // Small delay to avoid overwhelming the server
                await Task.Delay(500);
            }

            Console.WriteLine($"\nCreated {successCount} out of {count} appointments successfully.");
        }

        static async Task UpdateAppointment(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Update Appointment ---");
            Console.Write("Enter Appointment ID to update: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // First, get the existing appointment
                var existingAppointment = await client.GetAppointmentByIdAsync(id);
                if (existingAppointment == null)
                {
                    Console.WriteLine($"Appointment with ID {id} not found.");
                    return;
                }

                Console.WriteLine($"Current appointment: {existingAppointment}");
                Console.WriteLine("Enter new values (press Enter to keep current value):");

                Console.Write($"Sampling Method ({existingAppointment.SamplingMethod}): ");
                var samplingMethod = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(samplingMethod))
                    existingAppointment.SamplingMethod = samplingMethod;

                Console.Write($"Address ({existingAppointment.Address}): ");
                var address = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(address))
                    existingAppointment.Address = address;

                Console.Write($"Contact Phone ({existingAppointment.ContactPhone}): ");
                var phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone))
                    existingAppointment.ContactPhone = phone;

                Console.Write($"Notes ({existingAppointment.Notes}): ");
                var notes = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(notes))
                    existingAppointment.Notes = notes;

                Console.Write($"Total Amount ({existingAppointment.TotalAmount}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                    existingAppointment.TotalAmount = amount;

                Console.Write($"Is Paid ({existingAppointment.IsPaid}) [y/n]: ");
                var paidInput = Console.ReadLine()?.ToLower();
                if (paidInput == "y" || paidInput == "yes")
                    existingAppointment.IsPaid = true;
                else if (paidInput == "n" || paidInput == "no")
                    existingAppointment.IsPaid = false;

                existingAppointment.ModifiedDate = DateTime.Now;

                var result = await client.UpdateAppointmentAsync(id, existingAppointment);
                if (result != null)
                {
                    Console.WriteLine($"Appointment updated successfully: {result}");
                }
                else
                {
                    Console.WriteLine("Failed to update appointment.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static async Task DeleteAppointment(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Delete Appointment ---");
            Console.Write("Enter Appointment ID to delete: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                // First, show the appointment to be deleted
                var appointment = await client.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    Console.WriteLine($"Appointment with ID {id} not found.");
                    return;
                }

                Console.WriteLine($"Appointment to delete: {appointment}");
                Console.Write("Are you sure you want to delete this appointment? (y/N): ");

                var confirmation = Console.ReadLine()?.ToLower();
                if (confirmation == "y" || confirmation == "yes")
                {
                    bool result = await client.DeleteAppointmentAsync(id);
                    if (result)
                    {
                        Console.WriteLine($"Appointment with ID {id} deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to delete appointment.");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        static async Task TestAllOperations(AppointmentsSoapClient client)
        {
            Console.WriteLine("\n--- Testing All Operations (Demo) ---");

            // 1. Create a sample appointment
            Console.WriteLine("1. Creating a sample appointment...");
            var sampleAppointment = SampleDataHelper.CreateSampleAppointment();
            var createdAppointment = await client.CreateAppointmentAsync(sampleAppointment);

            if (createdAppointment == null)
            {
                Console.WriteLine("Failed to create sample appointment. Cannot continue demo.");
                return;
            }

            Console.WriteLine($"✓ Created: {createdAppointment}");
            await Task.Delay(1000);

            // 2. Get all appointments
            Console.WriteLine("\n2. Getting all appointments...");
            var allAppointments = await client.GetAllAppointmentsAsync();
            Console.WriteLine($"✓ Found {allAppointments.Count} appointments");
            await Task.Delay(1000);

            // 3. Get the created appointment by ID
            if (allAppointments.Any())
            {
                var lastAppointment = allAppointments.Last();
                Console.WriteLine($"\n3. Getting appointment by ID {lastAppointment.AppointmentsTienDmid}...");
                var fetchedAppointment = await client.GetAppointmentByIdAsync(lastAppointment.AppointmentsTienDmid);
                if (fetchedAppointment != null)
                {
                    Console.WriteLine($"✓ Found: {fetchedAppointment}");
                }
                await Task.Delay(1000);

                // 4. Update the appointment
                Console.WriteLine($"\n4. Updating appointment ID {lastAppointment.AppointmentsTienDmid}...");
                lastAppointment.Notes = "Updated by demo test";
                lastAppointment.IsPaid = true;
                lastAppointment.ModifiedDate = DateTime.Now;

                var updatedAppointment = await client.UpdateAppointmentAsync(lastAppointment.AppointmentsTienDmid, lastAppointment);
                if (updatedAppointment != null)
                {
                    Console.WriteLine($"✓ Updated: {updatedAppointment}");
                }
                await Task.Delay(1000);

                // 5. Delete the appointment
                Console.WriteLine($"\n5. Deleting appointment ID {lastAppointment.AppointmentsTienDmid}...");
                bool deleted = await client.DeleteAppointmentAsync(lastAppointment.AppointmentsTienDmid);
                if (deleted)
                {
                    Console.WriteLine($"✓ Deleted appointment ID {lastAppointment.AppointmentsTienDmid}");
                }
                else
                {
                    Console.WriteLine($"✗ Failed to delete appointment ID {lastAppointment.AppointmentsTienDmid}");
                }
            }

            Console.WriteLine("\n✓ Demo completed!");
        }

        static async Task RunSimpleTest()
        {
            Console.WriteLine("Running simple SOAP test...");
            string serviceUrl = ServiceUrlHttps;
            Console.WriteLine($"Testing connection to: {serviceUrl}");

            try
            {
                using var client = new AppointmentsSoapClient(serviceUrl);
                Console.WriteLine("✓ Connected successfully!");

                Console.WriteLine("⏳ Calling GetAllAppointments...");
                var appointments = await client.GetAllAppointmentsAsync();

                Console.WriteLine($"✓ Retrieved {appointments.Count} appointments");

                if (appointments.Count > 0)
                {
                    Console.WriteLine("\nFirst 3 appointments:");
                    foreach (var appointment in appointments.Take(3))
                    {
                        Console.WriteLine($"  ID: {appointment.AppointmentsTienDmid}");
                        Console.WriteLine($"  Address: {appointment.Address}");
                        Console.WriteLine($"  Phone: {appointment.ContactPhone}");
                        Console.WriteLine($"  Amount: {appointment.TotalAmount:C}");
                        Console.WriteLine($"  Paid: {appointment.IsPaid}");
                        Console.WriteLine("  ---");
                    }
                }

                Console.WriteLine("\n✓ Test completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }

        static async Task RunSimpleHttpMode(string baseUrl)
        {
            var simpleClient = new SimpleHttpClient(baseUrl);

            bool running = true;
            while (running)
            {
                DisplaySimpleHttpMenu();
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await GetAllAppointmentsSimple(simpleClient);
                            break;
                        case "2":
                            await GetAppointmentByIdSimple(simpleClient);
                            break;
                        case "3":
                            await CreateAppointmentSimple(simpleClient);
                            break;
                        case "0":
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (running)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            Console.WriteLine("Thank you for using DNA Testing System Simple HTTP Client!");
        }

        static void DisplaySimpleHttpMenu()
        {
            Console.WriteLine("DNA Testing System - Simple HTTP Client");
            Console.WriteLine("=".PadRight(50, '='));
            Console.WriteLine("1. Get All Appointments");
            Console.WriteLine("2. Get Appointment by ID");
            Console.WriteLine("3. Create Appointment");
            Console.WriteLine("0. Exit");
            Console.WriteLine("=".PadRight(50, '='));
            Console.Write("Choose an option: ");
        }

        static async Task GetAllAppointmentsSimple(SimpleHttpClient client)
        {
            try
            {
                var appointments = await client.GetAllAppointmentsAsync();

                if (appointments?.Any() == true)
                {
                    Console.WriteLine($"\nFound {appointments.Count} appointment(s):");
                    foreach (var appointment in appointments)
                    {
                        Console.WriteLine($"ID: {appointment.AppointmentsTienDmid}");
                        Console.WriteLine($"Appointment Date: {appointment.AppointmentDate}");
                        Console.WriteLine($"Appointment Time: {appointment.AppointmentTime}");
                        Console.WriteLine($"User Account ID: {appointment.UserAccountId}");
                        Console.WriteLine($"Service ID: {appointment.ServicesNhanVtid}");
                        Console.WriteLine($"Contact Phone: {appointment.ContactPhone}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                else
                {
                    Console.WriteLine("No appointments found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointments: {ex.Message}");
            }
        }

        static async Task GetAppointmentByIdSimple(SimpleHttpClient client)
        {
            try
            {
                Console.Write("Enter Appointment ID: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var appointment = await client.GetAppointmentByIdAsync(id);

                    if (appointment != null)
                    {
                        Console.WriteLine("\nAppointment Details:");
                        Console.WriteLine($"ID: {appointment.AppointmentsTienDmid}");
                        Console.WriteLine($"Appointment Date: {appointment.AppointmentDate}");
                        Console.WriteLine($"Appointment Time: {appointment.AppointmentTime}");
                        Console.WriteLine($"User Account ID: {appointment.UserAccountId}");
                        Console.WriteLine($"Service ID: {appointment.ServicesNhanVtid}");
                        Console.WriteLine($"Contact Phone: {appointment.ContactPhone}");
                    }
                    else
                    {
                        Console.WriteLine($"Appointment with ID {id} not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting appointment: {ex.Message}");
            }
        }

        static Task CreateAppointmentSimple(SimpleHttpClient client)
        {
            try
            {
                Console.WriteLine("\nCreate New Appointment:");

                Console.Write("User Account ID: ");
                if (!int.TryParse(Console.ReadLine(), out int userAccountId))
                {
                    Console.WriteLine("Invalid User Account ID format.");
                    return Task.CompletedTask;
                }

                Console.Write("Service ID: ");
                if (!int.TryParse(Console.ReadLine(), out int serviceId))
                {
                    Console.WriteLine("Invalid Service ID format.");
                    return Task.CompletedTask;
                }

                Console.Write("Appointment Date (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
                {
                    Console.WriteLine("Invalid date format.");
                    return Task.CompletedTask;
                }

                Console.Write("Appointment Time (HH:mm): ");
                if (!TimeSpan.TryParse(Console.ReadLine(), out TimeSpan appointmentTime))
                {
                    Console.WriteLine("Invalid time format.");
                    return Task.CompletedTask;
                }

                Console.Write("Contact Phone: ");
                var contactPhone = Console.ReadLine();

                Console.Write("Sampling Method: ");
                var samplingMethod = Console.ReadLine();

                var appointment = new Models.AppointmentsTienDm
                {
                    UserAccountId = userAccountId,
                    ServicesNhanVtid = serviceId,
                    AppointmentStatusesTienDmid = 1, // Default status
                    AppointmentDate = appointmentDate,
                    AppointmentTime = appointmentTime,
                    ContactPhone = contactPhone ?? "",
                    SamplingMethod = samplingMethod ?? ""
                };

                // Since CreateAppointmentAsync doesn't exist yet, we'll just display the data
                Console.WriteLine($"\nAppointment would be created with:");
                Console.WriteLine($"User Account ID: {appointment.UserAccountId}");
                Console.WriteLine($"Service ID: {appointment.ServicesNhanVtid}");
                Console.WriteLine($"Appointment Date: {appointment.AppointmentDate}");
                Console.WriteLine($"Appointment Time: {appointment.AppointmentTime}");
                Console.WriteLine($"Contact Phone: {appointment.ContactPhone}");
                Console.WriteLine($"Sampling Method: {appointment.SamplingMethod}");
                Console.WriteLine("\nNote: Create functionality not yet implemented in Simple HTTP mode.");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating appointment: {ex.Message}");
                return Task.CompletedTask;
            }
        }
    }
}
