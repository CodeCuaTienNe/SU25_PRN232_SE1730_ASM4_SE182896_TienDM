using System;
using System.Threading.Tasks;
using ServiceNhanVTWCFServiceReference;

namespace DNATesting.SoapClient.ConsoleApp.NhanVT
{
    class Program
    {
        static ServiceNhanVTSoapServicesClient client = new ServiceNhanVTSoapServicesClient();

        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== DNA Testing SOAP Console ===");
                Console.WriteLine("1. List all services");
                Console.WriteLine("2. Get service by ID");
                Console.WriteLine("3. Create new service");
                Console.WriteLine("4. Update service");
                Console.WriteLine("5. Delete service");
                Console.WriteLine("6. List all categories");
                Console.WriteLine("7. Get category by ID");
                Console.WriteLine("8. Create new category");
                Console.WriteLine("9. Update category");
                Console.WriteLine("10. Delete category");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await ListAllServices();
                            break;
                        case "2":
                            await GetServiceById();
                            break;
                        case "3":
                            await CreateService();
                            break;
                        case "4":
                            await UpdateService();
                            break;
                        case "5":
                            await DeleteService();
                            break;
                        case "6":
                            await ListAllCategories();
                            break;
                        case "7":
                            await GetCategoryById();
                            break;
                        case "8":
                            await CreateCategory();
                            break;
                        case "9":
                            await UpdateCategory();
                            break;
                        case "10":
                            await DeleteCategory();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid choice!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static async Task ListAllServices()
        {
            var services = await client.GetServiceNhanVTAsync();
            Console.WriteLine("--- All Services ---");
            foreach (var s in services)
            {
                PrintService(s);
            }
        }

        static async Task GetServiceById()
        {
            Console.Write("Enter Service ID: ");
            int id = int.Parse(Console.ReadLine());
            var s = await client.GetServiceNhanVTByIDAsync(id);
            PrintService(s);
        }

        static async Task CreateService()
        {
            var s = new ServicesNhanVt();
            Console.Write("Service Name: ");
            s.ServiceName = Console.ReadLine();
            Console.Write("Category ID: ");
            s.ServiceCategoryNhanVtid = int.Parse(Console.ReadLine());
            Console.Write("Description: ");
            s.Description = Console.ReadLine();
            Console.Write("Price: ");
            s.Price = decimal.Parse(Console.ReadLine());
            Console.Write("Duration: ");
            s.Duration = int.Parse(Console.ReadLine());
            Console.Write("Is Self Sample Allowed (true/false): ");
            s.IsSelfSampleAllowed = bool.Parse(Console.ReadLine());
            Console.Write("Is Home Visit Allowed (true/false): ");
            s.IsHomeVisitAllowed = bool.Parse(Console.ReadLine());
            Console.Write("Is Clinic Visit Allowed (true/false): ");
            s.IsClinicVisitAllowed = bool.Parse(Console.ReadLine());
            Console.Write("Processing Time: ");
            s.ProcessingTime = int.Parse(Console.ReadLine());
            Console.Write("Is Active (true/false): ");
            s.IsActive = bool.Parse(Console.ReadLine());

            int newId = await client.CreateServiceNhanVTAsync(s);
            Console.WriteLine($"Created service successfully");
        }

        static async Task UpdateService()
        {
            Console.Write("Enter Service ID to update: ");
            int id = int.Parse(Console.ReadLine());
            var s = await client.GetServiceNhanVTByIDAsync(id);
            if (s == null)
            {
                Console.WriteLine("Service not found!");
                return;
            }
            Console.WriteLine("Leave blank to keep current value.");
            Console.Write($"Service Name ({s.ServiceName}): ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.ServiceName = input;

            Console.Write($"Category ID ({s.ServiceCategoryNhanVtid}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.ServiceCategoryNhanVtid = int.Parse(input);

            Console.Write($"Description ({s.Description}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.Description = input;

            Console.Write($"Price ({s.Price}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.Price = decimal.Parse(input);

            Console.Write($"Duration ({s.Duration}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.Duration = int.Parse(input);

            Console.Write($"Is Self Sample Allowed ({s.IsSelfSampleAllowed}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.IsSelfSampleAllowed = bool.Parse(input);

            Console.Write($"Is Home Visit Allowed ({s.IsHomeVisitAllowed}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.IsHomeVisitAllowed = bool.Parse(input);

            Console.Write($"Is Clinic Visit Allowed ({s.IsClinicVisitAllowed}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.IsClinicVisitAllowed = bool.Parse(input);

            Console.Write($"Processing Time ({s.ProcessingTime}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.ProcessingTime = int.Parse(input);

            Console.Write($"Is Active ({s.IsActive}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) s.IsActive = bool.Parse(input);

            int result = await client.UpdateServiceNhanVTAsync(s);
            Console.WriteLine(result > 0 ? "Update successful!" : "Update failed!");
        }

        static async Task DeleteService()
        {
            Console.Write("Enter Service ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            bool result = await client.DeleteServiceNhanVTAsync(id);
            Console.WriteLine(result ? "Deleted successfully!" : "Delete failed!");
        }

        static async Task ListAllCategories()
        {
            var cats = await client.GetServiceCategoriesAsync();
            Console.WriteLine("--- All Categories ---");
            foreach (var c in cats)
            {
                PrintCategory(c);
            }
        }

        static async Task GetCategoryById()
        {
            Console.Write("Enter Category ID: ");
            int id = int.Parse(Console.ReadLine());
            var cats = await client.GetServiceCategoriesAsync();
            var c = Array.Find(cats, x => x.ServiceCategoryNhanVtid == id);
            if (c != null)
                PrintCategory(c);
            else
                Console.WriteLine("Category not found!");
        }

        static async Task CreateCategory()
        {
            var c = new ServiceCategoriesNhanVt();
            Console.Write("Category Name: ");
            c.CategoryName = Console.ReadLine();
            Console.Write("Description: ");
            c.Description = Console.ReadLine();
            Console.Write("Is Active (true/false): ");
            c.IsActive = bool.Parse(Console.ReadLine());

            int newId = await client.CreateServiceCatgegoryNhanVTAsync(c);
            Console.WriteLine($"Created category successfully");
        }

        static async Task UpdateCategory()
        {
            Console.Write("Enter Category ID to update: ");
            int id = int.Parse(Console.ReadLine());
            var cats = await client.GetServiceCategoriesAsync();
            var c = Array.Find(cats, x => x.ServiceCategoryNhanVtid == id);
            if (c == null)
            {
                Console.WriteLine("Category not found!");
                return;
            }
            Console.WriteLine("Leave blank to keep current value.");
            Console.Write($"Category Name ({c.CategoryName}): ");
            var input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) c.CategoryName = input;

            Console.Write($"Description ({c.Description}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) c.Description = input;

            Console.Write($"Is Active ({c.IsActive}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) c.IsActive = bool.Parse(input);

            int result = await client.UpdateServiceCatgegoryNhanVTAsync(c);
            Console.WriteLine(result > 0 ? "Update successful!" : "Update failed!");
        }

        static async Task DeleteCategory()
        {
            Console.Write("Enter Category ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            bool result = await client.DeleteServiceCatgegoryNhanVTAsync(id);
            Console.WriteLine(result ? "Deleted successfully!" : "Delete failed!");
        }

        static void PrintService(ServicesNhanVt s)
        {
            // Header
            Console.WriteLine(
                "+----+----------------------+----------+--------+---------+-------+-------+-------+------------+--------+");
            Console.WriteLine(
                "| ID | Name                 | CatID    | Price  | Duration| Active| Self  | Home  | Clinic     | ProcTm |");
            Console.WriteLine(
                "+----+----------------------+----------+--------+---------+-------+-------+-------+------------+--------+");

            // Data row
            Console.WriteLine(
                $"| {s.ServicesNhanVtid,2} | {Trunc(s.ServiceName,20),-20} | {s.ServiceCategoryNhanVtid,8} | {s.Price,6:0.00} | {s.Duration,7} | {BoolToYesNo(s.IsActive),5} | {BoolToYesNo(s.IsSelfSampleAllowed),5} | {BoolToYesNo(s.IsHomeVisitAllowed),5} | {BoolToYesNo(s.IsClinicVisitAllowed),10} | {s.ProcessingTime,6} |");
            Console.WriteLine(
                "+----+----------------------+----------+--------+---------+-------+-------+-------+------------+--------+");
            Console.WriteLine($"| Description: {s.Description}");
            Console.WriteLine();
        }

        static void PrintCategory(ServiceCategoriesNhanVt c)
        {
            Console.WriteLine(
                "+----+----------------------+-------+");
            Console.WriteLine(
                "| ID | Name                 | Active|");
            Console.WriteLine(
                "+----+----------------------+-------+");
            Console.WriteLine(
                $"| {c.ServiceCategoryNhanVtid,2} | {Trunc(c.CategoryName,20),-20} | {BoolToYesNo(c.IsActive),5} |");
            Console.WriteLine(
                "+----+----------------------+-------+");
            Console.WriteLine($"| Description: {c.Description}");
            Console.WriteLine();
        }

        // Helper to format bool as Yes/No
        static string BoolToYesNo(bool? value)
        {
            return value.HasValue ? (value.Value ? "Yes" : "No") : "No";
        }

        // Helper to truncate long strings
        static string Trunc(string value, int max)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return value.Length > max ? value.Substring(0, max - 3) + "..." : value;
        }
    }
}