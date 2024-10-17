using HelloWorldRabbit;
using System.Text;


var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

PublisherHandler.Handle("localhost", "hello", "hello", body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}