using Grpc.Core;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared.Contracts;
using System.Threading.Tasks;
using System.Windows;

namespace GrpcGreeterWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var channel = GrpcChannel.ForAddress("https://localhost:7077");
            //var client = channel.CreateGrpcService<IGreeterService>();

            //https://protobuf-net.github.io/protobuf-net.Grpc/gettingstarted

            var channel = new Channel("localhost", 5186, ChannelCredentials.Insecure);
            
            try
            {
                var client = channel.CreateGrpcService<IGreeterService>();
                // exception 
                // Grpc.Core.RpcException: 'Status(StatusCode="Unavailable", Detail="failed to connect to all addresses", DebugException="Grpc.Core.Internal.CoreErrorDetailException
                // https://github.com/protobuf-net/protobuf-net.Grpc/issues/274
                var reply = client.SayHelloAsync(
                    new HelloRequest { Name = "GreeterClient" }).GetAwaiter().GetResult();

                textbox1.Text = reply.Message;
            }
            finally
            {
                channel.ShutdownAsync().GetAwaiter().GetResult();
            }
        }
    }
}
