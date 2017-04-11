using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data.SqlClient;
using System.Net;

namespace LibraryCheckoutWPF
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

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            //change IsCheckedOutStatus to False
            var url = $"http://localhost:52489/api/library/UpdateBook?ID={CheckInInput.Text}&attribute=IsCheckedOut&newValue=False";
            var request = WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            var json = $"{{\"ID\":\"{CheckInInput.Text}\",\"attribute\":\"IsCheckedOut\",\"newValue\":\"False\"}}";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
                writer.Flush();
            }

            var response = request.GetResponse();
            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                while (reader.Peek() > -1)
                {
                   APIoutput.Text = reader.ReadLine();
                }
            }
        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            var url = $"http://localhost:52489/api/checkout/CheckOutBook?ID={CheckOutInput.Text}";
            var request = WebRequest.Create(url);
            request.Method = "POST";

            var json = $"{{\"ID\":\"{CheckOutInput.Text}\"}}";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
                writer.Flush();
            }

            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                APIoutput.Text = reader.ReadLine();
            }
        }
    }
}
