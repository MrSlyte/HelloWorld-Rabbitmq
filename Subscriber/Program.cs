// See https://aka.ms/new-console-template for more information
using HelloWorldRabbit;
using System.Text;

Console.WriteLine("Hello, World!");

var handler = new ConsumerHandler();
handler.Handle("localhost", "hello", true, (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
});

Console.ReadLine();