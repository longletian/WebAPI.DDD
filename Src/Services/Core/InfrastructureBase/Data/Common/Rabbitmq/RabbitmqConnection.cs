using Microsoft.Extensions.Options;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InfrastructureBase.Data
{
    /// <summary>
    /// rabbitmq连接实体
    /// </summary>
    public class RabbitmqConnection : IRabbitmqConnection
    {

        private readonly IConnectionFactory factory;
        private object lockObjects = new object();
        private readonly int _retryCount;
        bool _disposed;
        IConnection connection;

        public RabbitmqConnection(IConnectionFactory connectionFactory, int retryCount = 5)
        {
            factory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            _retryCount = retryCount;
        }


        public bool IsConnected
        {
            get
            {
                return this.connection != null && this.connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                TryConnect();
            }
            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;
            try
            {
                this.connection.Close();
                this.connection.Dispose();
            }
            catch (Exception ex)
            {
                Log.Information("异常" + ex.Message);
            }
        }

        /// <summary>
        /// 调用polly异常处理
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            Log.Information("rabbitmq 数据连接开始");

            //防止并发调用
            lock (lockObjects)
            {
                // 自定义异常处置
                var policy =
                    Policy.Handle<HttpRequestException>()
                    .WaitAndRetry(_retryCount, retryAction =>
                      TimeSpan.FromSeconds(Math.Pow(2, retryAction)), (ex, time) =>
                      {
                          Log.Warning(ex, "RabbitMQ Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                      }
                    );
                policy.Execute(() =>
                {
                    connection = this.factory.CreateConnection();
                });


                if (IsConnected)
                {
                    //连接关闭
                    connection.ConnectionShutdown += OnConnectionShutdown;
                    connection.CallbackException += OnCallbackException;
                    connection.ConnectionBlocked += OnConnectionBlocked;
                    return true;
                }
                else
                {
                    Log.Error("FATAL ERROR: RabbitMQ connections could not be created and opened");

                    return false;
                }
            }
        }


        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (this._disposed) return;

            Log.Warning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;

            Log.Warning("A RabbitMQ connection throw exception. Trying to re-connect...");

            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            Log.Warning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            TryConnect();
        }

    }
}
