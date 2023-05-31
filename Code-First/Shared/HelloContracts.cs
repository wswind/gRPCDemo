using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    [ProtoContract]
    [ProtoInclude(1, typeof(HelloReply))]
    public abstract class EmptyReply
    {
        [ProtoMember(1000)]
        public bool IsEmpty { get; set; }

        public void SetEmpty()
        {
            IsEmpty = true;
        }
    }


    [ProtoContract]
    public class HelloReply : EmptyReply
    {
        [ProtoMember(1)]
        public string Message { get; set; }
    }

    [ProtoContract]
    public class HelloRequest
    {
        [ProtoMember(1)]
        public string Name { get; set; }
    }

    [Service]
    public interface IGreeterService
    {
        Task<HelloReply> SayHelloAsync(HelloRequest request,
            CallContext context = default);
    }
}