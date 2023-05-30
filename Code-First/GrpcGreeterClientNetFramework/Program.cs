using Grpc.Core;
using ProtoBuf.Grpc.Client;
using Shared.Contracts;
using System;
using System.Threading.Tasks;

namespace GrpcGreeterClientNetFramework
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var channel = new Channel("localhost", 5186, ChannelCredentials.Insecure);

            try
            {
                var client = channel.CreateGrpcService<IGreeterService>();
                var reply = await client.SayHelloAsync(
                    new HelloRequest { Name = "GreeterClient" });

                Console.WriteLine(reply.Message);
            }
            finally
            {
                await channel.ShutdownAsync();
            }
        }
    }
}
