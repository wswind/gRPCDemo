using ProtoBuf;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    [ProtoContract]
    [ProtoInclude(1, typeof(HelloReply))]
    public abstract class ProtoBase
    {
        [ProtoMember(1000)]
        public string Code { get; set; }
        [ProtoMember(1001)]
        public string Msg { get; set; }
        /// <summary>
        /// 设置成功
        /// </summary>
        public void SetOK()
        {
            Code = "OK";
        }
        /// <summary>
        /// 判断是否成功
        /// </summary>
        /// <returns></returns>
        public bool IsOK()
        {
            return (Code == "OK");
        }
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="code">代号</param>
        /// <param name="msg">消息</param>
        public void SetCodeMsg(string code, string msg)
        {
            Code = code;
            Msg = msg;
        }
        /// <summary>
        /// 设置返回为Null
        /// </summary>
        public void SetReturnNull()
        {
            SetCodeMsg("ReturnNull", "Return value is null.");
        }

        /// <summary>
        /// 判断返回Null
        /// </summary>
        /// <returns></returns>
        public bool IsReturnNull()
        {
            return (Code == "ReturnNull");
        }
    }


    [ProtoContract]
    public class HelloReply : ProtoBase
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