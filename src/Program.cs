using var client = new HttpClient();

Console.WriteLine("Enter username:");
var username = Console.ReadLine() ?? string.Empty;

Console.WriteLine("Enter password:");
var password = Console.ReadLine() ?? string.Empty;
