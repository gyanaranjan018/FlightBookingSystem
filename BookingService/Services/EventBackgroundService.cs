using BookingService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookingService.Services
{
    public class EventBackgroundService : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;

        private readonly IServiceProvider _serviceProvider;

        public EventBackgroundService(IServiceProvider serviceProvider)
        {
            InitRabbitMQ();
            _serviceProvider = serviceProvider;
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("FlightBooking", false, false, false, null);
            _channel.BasicQos(0, 1, false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += ConsumerRecieved;

            _channel.BasicConsume("FlightBooking", false, consumer);
            return Task.CompletedTask;
        }

        private void ConsumerRecieved(object sender, BasicDeliverEventArgs eventArgs)
        {
            var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            var booking = JsonConvert.DeserializeObject<Booking>(content);

            using IServiceScope scope = _serviceProvider.CreateScope();
            IBookingManager manager =
                scope.ServiceProvider.GetRequiredService<IBookingManager>();

            manager.Add(booking);
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
