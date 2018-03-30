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
using System.Threading;

namespace Clientv01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

           // string[] files1 = Directory.GetFiles(@"c:\0001");

            //MessageBox.Show(files1[0]);

            var ServiceUpdate = new Service.ServiceUpdateClient("BasicHttpBinding_IServiceUpdate");

            String[] list = ServiceUpdate.getFileinfo();

            txtblock.Text = "";

            for (int i = 0; i < list.Length; i++)
            {
                // MessageBox.Show(list[i]);
                // int l = list[0].Length;
                
                txtblock.Text += "  " + list[i] + "\n";
                MessageBox.Show(list[i].Substring(0, 3) + "0001" + list[i].Substring(7, list[i].Length - 7 ));// list[0].Substring(7, 16));
            }



            for (int i = 0; i < list.Length; i++)
            {
                int lenght = ServiceUpdate.LenghtFile(list[i]);
                Stream file = ServiceUpdate.getFile(list[i]);
                byte[] bytes_file_w = new byte[lenght];
                file.Read(bytes_file_w, 0, lenght);

                string path = @list[i].Substring(0, 3) + "0001" + list[i].Substring(7, list[i].Length - 7);
                //string path = @"c:\0001\test.txt";
                FileStream file_w = File.Open(path, FileMode.Create);
                file_w.Write(bytes_file_w, 0, bytes_file_w.Length);

               // File.WriteAllBytes(@"c:\\MyDownloadedBooks\\" + "test.txt", bytes_file_w);
                file_w.Close();
            }
            ServiceUpdate.Close();
        }
    }
}
