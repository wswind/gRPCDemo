using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    /// <summary>
    /// 空返回的父类
    /// 子类都要包含在 ProtoInclude
    /// </summary>
    [ProtoContract]
    [ProtoInclude(1, typeof(HelloReply))]
    public abstract class EmptyReply
    {
        /// <summary>
        /// 是否为空
        /// 固定序号为 1000，不能与子类重复
        /// </summary>
        [ProtoMember(1000)]
        public bool IsEmpty { get; set; }

        public static T GetEmptyReply<T>() where T : EmptyReply, new()
        {
            var empty = new T() { IsEmpty = true };
            return empty;
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