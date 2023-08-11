using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
namespace RabbitMqConsumer.Services
{
    public class EmailQueueService : IEmailQueue
    {
      
        public void ConfirmMailQueue()
        {
            ConnectionFactory factory = new ConnectionFactory();
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
                        var tuple = JsonSerializer.Deserialize<Tuple<string, string, string>>(Encoding.UTF8.GetString(e.Body.Span));
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential("mr.nihadsoltanov@gmail.com", "webbb222444-444222"),
                            EnableSsl = true,
                        };
                        MailMessage mailMessage = new MailMessage("mr.nihadsoltanov@gmail.com", tuple.Item2,
                           tuple.Item3, body: tuple.Item1);
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);
                        Console.WriteLine("Email send");
                    };
                }
            }
        }
    }
}
