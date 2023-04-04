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
            var channel = GrpcChannel.ForAddress("https://localhost:7077");
            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" }).GetAwaiter().GetResult();

            textbox1.Text = reply.Message;
        }
    }
}
