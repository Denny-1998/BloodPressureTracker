using System.Text;
using MeasurementService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MeasurementService.Logic
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
                    retries --;
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
            _channel.ExchangeDeclare(exchange: "measurementExchange", type: "direct");

            _channel.QueueDeclare(queue: "measurementPostQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "measurementUpdateQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueDeclare(queue: "measurementDeleteQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: "measurementPostQueue", exchange: "measurementExchange", routingKey: "POST");
            _channel.QueueBind(queue: "measurementUpdateQueue", exchange: "measurementExchange", routingKey: "UPDATE");
            _channel.QueueBind(queue: "measurementDeleteQueue", exchange: "measurementExchange", routingKey: "DELETE");
        }

        public async Task ListenForMessages()
        {
            ListenToQueue("measurementPostQueue", "POST");
            ListenToQueue("measurementUpdateQueue", "UPDATE");
            ListenToQueue("measurementDeleteQueue", "DELETE");
        }


        private async Task ListenToQueue(string queueName, string action)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                if (action == "POST")
                {
                    HandlePost(message);
                }
                else if (action == "UPDATE")
                {
                    HandleUpdate(message);
                }
                else if (action == "DELETE")
                {
                    HandleDelete(message);
                }
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }



        private async Task HandlePost(string message)
        {
            var jsonHandler = new JsonHandler();
            Measurement measurement = jsonHandler.convertMeasurement(message);
            using (var scope = _serviceProvider.CreateScope())
            {
                var measurementHandler = scope.ServiceProvider.GetRequiredService<MeasurementHandler>();
                await measurementHandler.StoreMeasurement(measurement);
            }
            Console.WriteLine("Post Received message: " + message);
        }

        private async Task HandleUpdate(string message)
        {
            var jsonHandler = new JsonHandler();
            Measurement measurement = jsonHandler.convertMeasurement(message);
            using (var scope = _serviceProvider.CreateScope())
            {
                var measurementHandler = scope.ServiceProvider.GetRequiredService<MeasurementHandler>();
                await measurementHandler.UpdateMeasurement(measurement);
            }
            Console.WriteLine("Update Received message: " + message);
        }
        private async Task HandleDelete(string message)
        {
            var jsonHandler = new JsonHandler();
            Measurement measurement = jsonHandler.convertMeasurement(message);
            using (var scope = _serviceProvider.CreateScope())
            {
                var measurementHandler = scope.ServiceProvider.GetRequiredService<MeasurementHandler>();
                await measurementHandler.DeleteMeasurement(measurement.Id);
            }
            Console.WriteLine("Delete Received message: " + message);
        }



    }
}
