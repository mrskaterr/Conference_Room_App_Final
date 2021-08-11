using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Conference_Room_App
{
    /// <summary>
    /// Logika interakcji dla klasy AddDelete.xaml
    /// </summary>
    public partial class AddDelete : Window
    {   
        
        Window1 win1;
        static string StrSqlConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Waweł\Desktop\Conference-Room-App\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";
        static string StrSqlConnection2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\szpufipl\OneDrive - B. Braun\Pulpit\Conference-Room-App\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";
        private void RefreshPeopleComboBox()
        {
            ComboBox_Delete_Person.Items.Clear();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = "Select Name,Lastname from tblPeople";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComboBox_Delete_Person.Items.Add(reader.GetString(0).Trim() + " " + reader.GetString(1).Trim());
                            }
                        }
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
        }
        private void RefreshRoomsComboBox()
        {
            ComboBox_Delete_Room.Items.Clear();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = "Select RoomName from tblRooms";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComboBox_Delete_Room.Items.Add(reader.GetString(0));
                            }
                        }
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
        }
        public AddDelete(Window1 win)
        {
            win1 = win;
            InitializeComponent();
            MenuAddDelete_TextBlock.Background = Brushes.White;
            RefreshPeopleComboBox();
            RefreshRoomsComboBox();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            win1.Close();
            win1.ReservationViewWindow.Close();
            Close();
        }

        private void Maximized_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                win1.WindowState = WindowState.Maximized;
                win1.ReservationViewWindow.WindowState = WindowState.Maximized;
                this.Left = 0;
                this.Top = 0;
                win1.Left = 0;
                win1.Top = 0;
                win1.ReservationViewWindow.Left = 0;
                win1.ReservationViewWindow.Top = 0;
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 8;
                win1.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 8;
                win1.ReservationViewWindow.MaxHeight= SystemParameters.MaximizedPrimaryScreenHeight - 8;

                MainBorder.CornerRadius = new CornerRadius(0);
                MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

                win1.MainBorder.CornerRadius = new CornerRadius(0);
                win1.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

                win1.ReservationViewWindow.MainBorder.CornerRadius = new CornerRadius(0);
                win1.ReservationViewWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

            }

            else
            {
                WindowState = WindowState.Normal;
                win1.WindowState = WindowState.Normal;
                win1.ReservationViewWindow.WindowState = WindowState.Normal;
                win1.Width = 900;
                win1.Height = 600;
                win1.Left = this.Left;
                win1.Top = this.Top;
                win1.ReservationViewWindow.WindowState = WindowState.Normal;
                win1.ReservationViewWindow.Width = 900;
                win1.ReservationViewWindow.Height = 600;
                win1.ReservationViewWindow.Left = this.Left;
                win1.ReservationViewWindow.Top = this.Top;

                MainBorder.CornerRadius = new CornerRadius(30);
                MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);

                win1.MainBorder.CornerRadius = new CornerRadius(30);
                win1.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);


                //win1.ReservationViewWindow.MainBorder.CornerRadius = new CornerRadius(30);
                //win1.ReservationViewWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);
            }
        }

        private void Minimized_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                WindowState = WindowState.Minimized;
                win1.WindowState = WindowState.Minimized;
                win1.ReservationViewWindow.WindowState = WindowState.Minimized;
            }

        }
        private void MenuMainWindow_TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            win1.Show();
            win1.Left = this.Left;
            win1.Top = this.Top;
            win1.Width = this.Width;
            win1.Height = this.Height;
            win1.Refresh_StackPanel_RoomButtons();
            Hide();
        }
        private void MenuReservationView_TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            win1.ReservationViewWindow.Show();
            win1.ReservationViewWindow.Left = this.Left;
            win1.ReservationViewWindow.Top = this.Top;
            win1.ReservationViewWindow.Width = this.Width;
            win1.ReservationViewWindow.Height = this.Height;
            Hide();
        }

        private void Add_Person_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Name.Text == "" || TextBox_Lastname.Text == "") return;
            int count = 1;
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = $"Select Count(Id) from tblPeople where Name=\'{TextBox_Name.Text}\' AND Lastname=\'{TextBox_Lastname.Text}\'";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader.GetInt32(0);
                            }
                        }
                        if (count == 0)
                        {
                            Qyery = $"Insert into tblPeople(Name,Lastname) Values (\'{TextBox_Name.Text}\',\'{TextBox_Lastname.Text}\')";
                            using (SqlCommand command2 = new SqlCommand(Qyery, SqlConn))
                            {
                                if (command2.ExecuteNonQuery() != 0)
                                {
                                    MessageBox.Show($"{TextBox_Name.Text + " "+ TextBox_Lastname.Text} is insert.");
                                    TextBox_Name.Text = "";
                                    TextBox_Lastname.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
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
        }
        private void Add_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Room.Text == "") return;
            int count = 1;
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();

                    String Qyery = $"Select Count(Id) from tblRooms where RoomName=\'{TextBox_Room.Text}\'";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                count = reader.GetInt32(0);
                            }
                        }
                        if (count == 0)
                        {
                            Qyery = $"Insert into tblRooms(RoomName) Values (\'{TextBox_Room.Text}\')";
                            using (SqlCommand command2 = new SqlCommand(Qyery, SqlConn))
                            {
                                if (command2.ExecuteNonQuery() != 0)
                                {
                                    MessageBox.Show($"{TextBox_Room.Text} is insert.");
                                    TextBox_Room.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
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
        }
        private void Delete_Person_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Delete_Person.SelectedItem == null) return;
            string[] NameLastname = ComboBox_Delete_Person.SelectedItem.ToString().Split(' ');
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();

                    String Qyery = $"DELETE FROM tblPeople WHERE Name=\'{NameLastname[0]}\' AND Lastname=\'{NameLastname[1]}\'";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        if (command.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show($"{NameLastname[0] +" "+ NameLastname[1]} is delete.");
                            ComboBox_Delete_Person.SelectedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
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
        }
        
        void OnDropDownOpened_People_ComboBox(object sender, EventArgs e)
        {
            RefreshPeopleComboBox();
        }

        private void Delete_Room_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Delete_Room.SelectedItem == null) return;
            string RoomName = ComboBox_Delete_Room.SelectedItem.ToString();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();

                    String Qyery = $"DELETE FROM tblRooms WHERE RoomName=\'{RoomName}\'";
                    using (SqlCommand command = new SqlCommand(Qyery, SqlConn))
                    {
                        if (command.ExecuteNonQuery() != 0)
                        {
                            MessageBox.Show($"{RoomName} is delete.");
                            ComboBox_Delete_Room.SelectedItem = null;
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
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
        }
        void OnDropDownOpened_Rooms_ComboBox(object sender, EventArgs e)
        {
            RefreshRoomsComboBox();
        }
    }
}
