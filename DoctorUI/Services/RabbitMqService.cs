using System;
using System.Text;
using RabbitMQ.Client; 
using Newtonsoft.Json; 
using DoctorUI.Models; 

namespace DoctorUI.Services
{
    public class RabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PostNewPatient(Patient patient)
        {
            var message = JsonConvert.SerializeObject(patient);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "patientExchange", routingKey: "POST", basicProperties: null, body: body);
        }
    }

}
