using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using PatientService.Models;
using PatientService.Logic;
using System.Text;
using Microsoft.Extensions.Diagnostics.Metrics;

namespace PatientService.Logic
{
    public class RabbitMqHandler
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _hostname = "rabbitmq";
        private IConnection _connection;
        private IModel _channel;


        public RabbitMqHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            SetupRabbitMq();

        }


        private void SetupRabbitMq()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname
            };

            int retries = 5;
            while (retries > 0)
            {
                try
                {
                    _connection = factory.CreateConnection();
                    break;
                }
                catch (Exception ex)
                {
                    retries--;
                    if (retries == 0)
                        throw new Exception(ex.Message);

                    Console.WriteLine("RabbitMQ connection failed. Retrying in 5 seconds.");
                    Thread.Sleep(10000);
                }

            }
            _channel = _connection.CreateModel();

            DeclareQueues();
        }


        private void DeclareQueues()
        {
            _channel.ExchangeDeclare(exchange: "patientExchange", type: "direct");

            _channel.QueueDeclare(queue: "patientPostQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            _channel.QueueBind(queue: "patientPostQueue", exchange: "patientExchange", routingKey: "POST");
        }

        public async Task ListenForMessages()
        {
            ListenToQueue("patientPostQueue", "POST");
        }


        private async Task ListenToQueue(string queueName, string action)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                HandlePost(message);
                
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }



        private async Task HandlePost(string message)
        {
            var jsonHandler = new JsonHandler();
            Patient patient = jsonHandler.convertPatient(message);
            using(var scope = _serviceProvider.CreateScope())
            {
                var patientHandler = scope.ServiceProvider.GetRequiredService<PatientHandler>();
                await patientHandler.StorePatient(patient);

            }
            Console.WriteLine("Post Received message: " + message);
        }


    }
}
