using Shared.Contracts;
using ProtoBuf.Grpc;

public class GreeterService : IGreeterService
{
    public async Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default)
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        //return EmptyReply.GetEmptyReply<HelloReply>();
        return null;
    }
}