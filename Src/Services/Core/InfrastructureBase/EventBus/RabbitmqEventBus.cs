using DomainBase;
using InfrastructureBase.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureBase.EventBus
{
    public class RabbitmqEventBus : IEventBus, IDisposable
    {
        private readonly IRabbitmqConnection rabbitmqConnection;
        private readonly IServiceProvider serviceProvider;
        private IModel _channel;

        public IModel channel
        {
            get
            {
                if (_channel == null)
                    _channel = rabbitmqConnection.CreateModel();
                return _channel;
            }
        }

        public RabbitmqEventBus(IRabbitmqConnection _rabbitmqConnection, IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
            rabbitmqConnection = _rabbitmqConnection ?? throw new ArgumentNullException(nameof(_rabbitmqConnection));
        }

        public void PublishAsync<TIntegrationEvent>(TIntegrationEvent @event, string exchangeName) where TIntegrationEvent : Event
        {
            if (!rabbitmqConnection.IsConnected)
            {
                rabbitmqConnection.TryConnect();
            }
            CreateExchangeIfExists(exchangeName, ExchangeType.Fanout);

            var eventName = @event.GetType().Name;
            var properties = channel.CreateBasicProperties();
            //发送消息的时候将消息的 deliveryMode 设置为 2 消息持久化
            properties.DeliveryMode = 2; // persistent
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: exchangeName,
                                routingKey: string.Empty,
                                mandatory: true,
                                basicProperties: properties,
                                body: body);

        }

        public void Subscribe<TH,T>(string exchangeName, string subscriberName)
            where T : Event
            where TH : IEventHandle<T>
        {
            if (!rabbitmqConnection.IsConnected)
            {
                rabbitmqConnection.TryConnect();
            }

            BindQueue(exchangeName, subscriberName);
            //定义消费者
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async(obj,args) =>
            {
                using (var scope=serviceProvider.CreateScope())
                {
                    var handle = scope.ServiceProvider.GetRequiredService<IEventHandle<T>>();
                    var jsonMessage = Encoding.UTF8.GetString(args.Body.ToArray());
                    var message = JsonConvert.DeserializeObject<T>(jsonMessage);

                    await handle.HandleAsync(message);

                    //确认该消息已被消费     
                    channel.BasicAck(args.DeliveryTag, false);
                }
            };
            //启动消费者
            channel.BasicConsume(string.Empty, false, consumer);

        }

        public void Unsubscribe<T, TH>()
            where T : Event
            where TH : IEventHandle<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        private void CreateExchangeIfExists(string exchangeName, string exchangeType = ExchangeType.Fanout)
        {
            //type：可选项为，fanout，direct，topic，headers。区别如下：
            //fanout：发送到所有与当前Exchange绑定的Queue中
            //direct：发送到与消息的routeKey相同的Queue中
            //topic：fanout的模糊版本
            //headers：发送到与消息的header属性相同的Queue中
            //durable：持久化
            //autoDelete：当最后一个绑定（队列或者exchange）被unbind之后，该exchange自动被删除。
            channel.ExchangeDeclare(exchangeName, exchangeType, true, false);

        }
        private void BindQueue(string exchangeName, string subscriberName, string routerName = "", Dictionary<string, object> headerKeys = null)
        {
            CreateExchangeIfExists(exchangeName);

            //durable：持久化
            //exclusive：如果为true，则queue只在channel存在时存在，channel关闭则queue消失
            //autoDelete：当最后一个绑定（队列或者exchange）被unbind之后，该exchange自动被删除。
            //队列持久化
            //string queueName = channel.QueueDeclare(Queue_KEY, true, false, false).QueueName;
            //发布不需要绑定队列
            //channel.QueueBind(Queue_KEY, EXCHANGE_KEY, eventName);
            channel.QueueDeclare(subscriberName, true, false, false, headerKeys);

            channel.QueueBind(subscriberName, exchangeName, string.Empty);
        }
    }
}
