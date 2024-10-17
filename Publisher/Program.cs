
using HelloWorldRabbit;
using System.Text;

const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

PublisherHandler.Handle("localhost", "hello", "hello", body);
Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();