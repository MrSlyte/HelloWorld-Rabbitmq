// See https://aka.ms/new-console-template for more information
using HelloWorldRabbit;
using System.Text;

Console.WriteLine("Esse worker é qual número:");
var numberWorker = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Hello, World!");

Console.WriteLine(" [*] Waiting for messages.");

var handler = new ConsumerHandler();
handler.Handle("localhost", "hello", false, (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");

    int dots = message.Split('.').Length - 1;
    Thread.Sleep(dots * 1000);

    Console.WriteLine(" [x] Done");
    // here channel could also be accessed as ((EventingBasicConsumer)sender).Model
    if (numberWorker == 2)
    {
        handler.Channel?.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
});

Console.ReadLine();