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
using System.Data.SqlClient;
using System.Data;

namespace Conference_Room_App
{
    public partial class MainWindow : Window
    {
        static string StrSqlConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Waweł\Desktop\Conference-Room-App_Final\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";
        static string StrSqlConnection2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\szpufipl\OneDrive - B. Braun\Pulpit\Conference-Room-App\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using SqlConnection SqlConn = new SqlConnection(StrSqlConnection);
            try
            {
                if (SqlConn.State == ConnectionState.Closed)
                    SqlConn.Open();
                String Qyery = "Select COUNT(1) FROM tblUser WHERE Username=@UserName AND Password=@Password";
                SqlCommand SqlCmd = new SqlCommand(Qyery, SqlConn);
                SqlCmd.CommandType = CommandType.Text;
                SqlCmd.Parameters.AddWithValue("@UserName", txtUsername.Text);
                SqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int Count = Convert.ToInt32(SqlCmd.ExecuteScalar());
                if (Count == 1)
                {
                    SqlConn.Close();

                    Window1 win1 = new Window1();
                    win1.Left = this.Left - 200;
                    win1.Top = this.Top + 70;
                    win1.Show();
                    Close();
                }
                else
                {
                    LabelIncorect.Text = "Username or Password is incorrect.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SqlConn.Close();
            }
        }
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }
        public void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox tb = (PasswordBox)sender;
            tb.Password = string.Empty;
            tb.GotFocus -= PasswordBox_GotFocus;
        }
    }
}
