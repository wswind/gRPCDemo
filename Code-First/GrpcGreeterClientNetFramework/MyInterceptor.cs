using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Threading.Tasks;

namespace GrpcGreeterClientNetFramework
{
    //from https://anthonygiretti.com/2020/03/31/grpc-asp-net-core-3-1-global-error-handling-in-grpc-grpc-status-codes/
    public class MyInterceptor : Interceptor
    {
        public MyInterceptor()
        {
        }
        public override AsyncClientStreamingCall<TRequest, TResponse> AsyncClientStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncClientStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            return base.AsyncClientStreamingCall(context, continuation);
        }

        public override AsyncDuplexStreamingCall<TRequest, TResponse> AsyncDuplexStreamingCall<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, AsyncDuplexStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            return base.AsyncDuplexStreamingCall(context, continuation);
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            return base.AsyncServerStreamingCall(request, context, continuation);
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return base.BlockingUnaryCall(request, context, continuation);
        }

        public override Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context, ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            return base.ClientStreamingServerHandler(requestStream, context, continuation);
        }

        public override Task DuplexStreamingServerHandler<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            return base.DuplexStreamingServerHandler(requestStream, responseStream, context, continuation);
        }

        public override Task ServerStreamingServerHandler<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context, ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            return base.ServerStreamingServerHandler(request, responseStream, context, continuation);
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            Console.WriteLine(context.Method);
            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(HandleResponse(call.ResponseAsync), call.ResponseHeadersAsync, call.GetStatus, call.GetTrailers, call.Dispose);
        }


        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> t)
        {
            try
            {
                var response = await t;
                Console.WriteLine($"Response received: {response}");
                return response;
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Call error: {ex.Message}");
                return default;
            }
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            return base.UnaryServerHandler(request, context, continuation);
        }



        //public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        //{
        //    try
        //    {
        //        var obj = base.AsyncUnaryCall(request, context, continuation);
        //        return obj;
        //    }
        //    catch (RpcException rpcEx)
        //    {
        //        if (rpcEx.StatusCode == StatusCode.Cancelled && rpcEx.Status.Detail == "No message returned from method.")
        //        {
        //            Console.WriteLine("Return Null");
        //        }
        //        Console.WriteLine(rpcEx.Message);
        //        return null;
        //    }
        //}

        //public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        //{
        //    //logger.Debug($"{Environment.NewLine}GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request, Formatting.Indented)}");
        //    Console.WriteLine($"{Environment.NewLine}GRPC Request{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(request)}");

        //    var response = await base.UnaryServerHandler(request, context, continuation);

        //    Console.WriteLine($"{Environment.NewLine}GRPC Response{Environment.NewLine}Method: {context.Method}{Environment.NewLine}Data: {JsonConvert.SerializeObject(response)}");

        //    return response;
        //}
    }
}
