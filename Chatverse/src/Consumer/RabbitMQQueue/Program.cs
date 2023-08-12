
using Chatverse.Infrastructure.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

ConnectionFactory factory = new ConnectionFactory();
EmailConfirmService _emailSend = new EmailConfirmService();
factory.Uri = new Uri("amqps://vcakrzgr:TaL4IMmbZjkyrvRLeVkFMEtiCNCkILrQ@rat.rmq2.cloudamqp.com/vcakrzgr");

using (var connection = factory.CreateConnection())
{
    using (IModel channel = connection.CreateModel())
    {
        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        channel.QueueDeclare("email-queue", false, false, true);
        channel.BasicConsume("email-queue", true, consumer);
        consumer.Received += (sender, e) =>
        {
            Tuple<string,string> _items = JsonSerializer.Deserialize<Tuple<string,string>>(Encoding.UTF8.GetString(e.Body.Span));
            _emailSend.SendMail(email:_items.Item1, "Confirm Your Email", $"<h1>Welcome ChatVerse</h1>" + $"<p>Please confirm your email  by <a href='{_items.Item2}'>Clicking here</a> </p>");
            Console.WriteLine("Email send");
        };
    }
}
Console.Read();