using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Conference_Room_App
{

    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static string StrSqlConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Waweł\Desktop\Conference-Room-App_Final\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";
        static string StrSqlConnection2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\szpufipl\OneDrive - B. Braun\Pulpit\Conference-Room-App\Conference-Room-App\ConferenceRoomDB.mdf;Integrated Security=True";

        public ReservationView ReservationViewWindow;
        public AddDelete AddDeleteWindow;

        Button ClickedButton = null;
        int IndexPeople;
        string Room = null;
        DateTime datetime = DateTime.Today;
        DateTime StartDateTime = DateTime.Today;
        DateTime EndDateTime = DateTime.Today;

        public Window1()
        {
            InitializeComponent();
            ReservationViewWindow = new ReservationView(this);
            AddDeleteWindow = new AddDelete(this);
            MenuMainWindow_TextBlock.Background = Brushes.White;
            Calendar1.SelectedDate = DateTime.Today;
            Refresh_ComboBoxItem_tblPeople();
            Refresh_StackPanel_RoomButtons();

            String NumberStr;
            for (int i = 0; i < 60; i++)
            {
                NumberStr = AddZero(i);
                if (i < 24)
                {
                    ComboBox_StartHour.Items.Add(NumberStr);
                }
                ComboBox_StartMinute.Items.Add(NumberStr);
            }//Add hours and minutes to ComboBoxes 00,01,02,03..

        }
        private string AddZero(int intiger)
        {
            string str = intiger.ToString();
            if (str.Length == 1)
            {
                str = "0" + str;
                return str;
            }
            return str;
        }//intiger 0 to string "00", 1 to "01", 2 to "02" ...
        public void Refresh_StackPanel_RoomButtons()
        {
            StackPanel_RoomButtons.Children.Clear();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = "Select RoomName from tblRooms";
                    using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                var Button = new Button()
                                {
                                    Content = Reader.GetString(0),
                                    Foreground = Brushes.Black,
                                    Height = 50,
                                    FontSize = 32,

                                }; // Creating button
                                Button.Click += SelectRoomName; //Hooking up to event
                                if (Room != null && (string)Button.Content == Room)
                                {
                                    Button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#00b482");
                                    StackPanel_RoomButtons.Children.Insert(0, Button);
                                }
                                else
                                    StackPanel_RoomButtons.Children.Add(Button);
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
        private void Refresh_StackPanel_ReservationPreview()
        {
            ReservationPreview.Children.Clear();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = $"Select StartHour,StartMinute,EndHour,EndMinute,Name,Lastname from Stays where  RoomName=\'{Room}\' AND StartDay={StartDateTime.Day} AND StartMonth={StartDateTime.Month} And StartYear={StartDateTime.Year} ORDER BY StartHour,StartMinute";
                    using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                    {
                        TitleReservationPreview.Text = "Sala : ";
                        TitleReservationPreview.Text += Room;
                        TitleReservationPreview.Text += "\n";
                        TitleReservationPreview.Text += $"Dzień : {AddZero(StartDateTime.Day) + "." + AddZero(StartDateTime.Month) + "." + StartDateTime.Year}";
                        TitleReservationPreview.Foreground = Brushes.White;
                        TitleReservationPreview.FontSize = 16;
                        TitleReservationPreview.TextAlignment = TextAlignment.Center;

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            for (int i = 0; Reader.Read(); i++)
                            {
                                StackPanel MainPanel = new StackPanel();
                                Label TimeLabel = new Label();
                                TimeLabel.Content = AddZero(Reader.GetInt32(0)) + ":" + AddZero(Reader.GetInt32(1)) + " - " + AddZero(Reader.GetInt32(2)) + ":" + AddZero(Reader.GetInt32(3));
                                TimeLabel.FontSize = 18;
                                TimeLabel.HorizontalAlignment = HorizontalAlignment.Center;
                                Label PersonLabel = new Label();
                                PersonLabel.Content = Reader.GetString(4).Trim(' ') + " " + Reader.GetString(5).Trim(' ');
                                PersonLabel.FontSize = 16;
                                PersonLabel.HorizontalAlignment = HorizontalAlignment.Right;
                                MainPanel.Children.Add(TimeLabel);
                                MainPanel.Children.Add(PersonLabel);
                                Border border = new Border();
                                if (i % 2 == 0)
                                    border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFAAAAAA");
                                else
                                    border.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#CCCCCC");
                                border.Child = MainPanel;
                                ReservationPreview.Children.Add(border);

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
        private void SelectRoomName(object sender, RoutedEventArgs e)
        {
            StackPanel_RoomButtons.Children.OfType<Button>().FirstOrDefault().Background = Brushes.Transparent;

            if (ClickedButton != null)
                ClickedButton.Background = Brushes.Transparent;

            ClickedButton = (Button)sender;
            ClickedButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#00b482");
            Room = sender.ToString().Remove(0, sender.GetType().ToString().Length + 2);
            Refresh_StackPanel_ReservationPreview();
        }

        private void Refresh_ComboBoxItem_tblPeople()
        {
            ComboBox_tblPeople.Items.Clear();
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {
                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = "Select Name,Lastname from tblPeople";
                    using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                ComboBox_tblPeople.Items.Add(Reader.GetString(0).Trim() + " " + Reader.GetString(1).Trim());
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationViewWindow.Close();
            AddDeleteWindow.Close();
            Close();
        }

        private void Maximized_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                ReservationViewWindow.WindowState = WindowState.Maximized;
                AddDeleteWindow.WindowState = WindowState.Maximized;
                this.Left = 0;
                this.Top = 0;
                ReservationViewWindow.Left = 0;
                ReservationViewWindow.Top = 0;
                AddDeleteWindow.Left = 0;
                AddDeleteWindow.Top = 0;
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 8;
                AddDeleteWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 8;
                ReservationViewWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 8;

                MainBorder.CornerRadius = new CornerRadius(0);
                MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

                AddDeleteWindow.MainBorder.CornerRadius = new CornerRadius(0);
                AddDeleteWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

                ReservationViewWindow.MainBorder.CornerRadius = new CornerRadius(0);
                ReservationViewWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 0);

            }

            else
            {
                WindowState = WindowState.Normal;
                AddDeleteWindow.WindowState = WindowState.Normal;
                ReservationViewWindow.WindowState = WindowState.Normal;

                ReservationViewWindow.Width = 900;
                ReservationViewWindow.Height = 600;
                ReservationViewWindow.Left = this.Left;
                ReservationViewWindow.Top = this.Top;


                AddDeleteWindow.Width = 900;
                AddDeleteWindow.Height = 600;
                AddDeleteWindow.Left = this.Left;
                AddDeleteWindow.Top = this.Top;

                MainBorder.CornerRadius = new CornerRadius(30);
                MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);

                AddDeleteWindow.MainBorder.CornerRadius = new CornerRadius(30);
                AddDeleteWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);

                ReservationViewWindow.MainBorder.CornerRadius = new CornerRadius(30);
                ReservationViewWindow.MenuBorder.CornerRadius = new CornerRadius(0, 30, 0, 30);
            }
        }

        private void Minimized_Button_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                WindowState = WindowState.Minimized;
                ReservationViewWindow.WindowState = WindowState.Minimized;
                AddDeleteWindow.WindowState = WindowState.Minimized;
            }

        }

        private void QueryButtom()
        {
            using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
            {

                try
                {
                    if (SqlConn.State == ConnectionState.Closed)
                        SqlConn.Open();
                    String Qyery = $"Select RoomName from tblRooms group by RoomName EXCEPT Select RoomName from tblReservation right join tblRooms on Room_ID = tblRooms.Id where StartYear={StartDateTime.Year} AND StartMonth={StartDateTime.Month} AND StartDay={StartDateTime.Day} ";

                    using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                    {
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            StackPanel_RoomButtons.Children.Clear();
                            while (Reader.Read())
                            {
                                var Button = new Button()
                                {
                                    Content = Reader.GetString(0),
                                    Foreground = Brushes.Black,
                                    Height = 50,
                                    FontSize = 32
                                };
                                Button.Click += SelectRoomName;
                                ; //Hooking up to event
                                if (Room != null && (string)Button.Content == Room)
                                {
                                    Button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#00b482");
                                    StackPanel_RoomButtons.Children.Insert(0, Button);
                                    ButtonScrollBar.ScrollToVerticalOffset(0);
                                }
                                else
                                    StackPanel_RoomButtons.Children.Add(Button);
                            }
                        }
                    }

                    Qyery = $"SELECT RoomName,StartHour,StartMinute,EndHour,EndMinute from stays WHERE StartYear={StartDateTime.Year} AND StartMonth={StartDateTime.Month} AND StartDay={StartDateTime.Day}";
                    using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                    {
                        var sList = new ArrayList();
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            int StartHour;
                            int StartMinut;
                            int EndHour;
                            int EndMinut;
                            while (Reader.Read())
                            {
                                StartHour = Reader.GetInt32(1);
                                StartMinut = Reader.GetInt32(2);
                                EndHour = Reader.GetInt32(3);
                                EndMinut = Reader.GetInt32(4);

                                if (((StartHour * 100 + StartMinut >= StartDateTime.Hour * 100 + StartDateTime.Minute && StartDateTime.Hour * 100 + StartDateTime.Minute <= EndHour * 100 + EndMinut)
                                && (StartHour * 100 + StartMinut >= EndDateTime.Hour * 100 + EndDateTime.Minute && EndDateTime.Hour * 100 + EndDateTime.Minute <= EndHour * 100 + EndMinut))
                                || ((StartHour * 100 + StartMinut <= StartDateTime.Hour * 100 + StartDateTime.Minute && StartDateTime.Hour * 100 + StartDateTime.Minute >= EndHour * 100 + EndMinut)
                                && (StartHour * 100 + StartMinut <= EndDateTime.Hour * 100 + EndDateTime.Minute && EndDateTime.Hour * 100 + EndDateTime.Minute >= EndHour * 100 + EndMinut)))
                                {
                                    sList.Add(Reader.GetString(0));
                                }
                            }
                            var sNew = sList.ToArray();
                            var dist = sNew.Distinct().ToArray();
                            for (int i = 0; i < dist.Length; i++)
                            {
                                var Button = new Button()
                                {
                                    Content = dist[i],
                                    Foreground = Brushes.Black,
                                    Height = 50,
                                    FontSize = 32
                                }; // Creating button
                                Button.Click += SelectRoomName; //Hooking up to event
                                if (Room != null && (string)Button.Content == Room)
                                {
                                    Button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#00b482");
                                    StackPanel_RoomButtons.Children.Insert(0, Button);
                                    ButtonScrollBar.ScrollToVerticalOffset(0);
                                }
                                else
                                    StackPanel_RoomButtons.Children.Add(Button);
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
        }//Now not reserved

        private void SelectedChanged_StartTime(object sender, SelectionChangedEventArgs e)
        {
            if (ClickedButton != null)
                ClickedButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#00b482");
            ComboBox_EndHour.SelectedItem = null;
            ComboBox_EndMinute.SelectedItem = null;
            datetime = ((DateTime)Calendar1.SelectedDate);
            if (ComboBox_StartHour.SelectedItem != null)
            {
                double doub;
                doub = Convert.ToDouble(ComboBox_StartHour.SelectedItem.ToString());
                datetime = datetime.AddHours(doub);
                if (ComboBox_StartMinute.SelectedItem == null)
                {
                    ComboBox_StartMinute.SelectedIndex = 0;
                }
                doub = Convert.ToDouble(ComboBox_StartMinute.SelectedItem.ToString());
                datetime = datetime.AddMinutes(doub);
            }
            StartDateTime = datetime;

            TitleReservationPreview.Text = "Sala :       ";
            TitleReservationPreview.Text += Room;
            TitleReservationPreview.Text += "\n";
            TitleReservationPreview.Text += $"Dzień : {AddZero(StartDateTime.Day) + "." + AddZero(StartDateTime.Month) + "." + StartDateTime.Year}";
            TitleReservationPreview.FontSize = 16;
            TitleReservationPreview.TextAlignment = TextAlignment.Center;
            TitleReservationPreview.Foreground = Brushes.White;
            if (Room != null)
                Refresh_StackPanel_ReservationPreview();
            QueryButtom();

        }
        private void ClearButton(object sender, RoutedEventArgs e)
        {
            ComboBox_StartHour.SelectedItem = null;
            ComboBox_StartMinute.SelectedItem = null;
            ComboBox_EndHour.SelectedItem = null;
            ComboBox_EndMinute.SelectedItem = null;
            ComboBox_tblPeople.SelectedItem = null;
            Room = null;
            TitleReservationPreview.Text = "Sala :       ";
            TitleReservationPreview.Text += Room;
            TitleReservationPreview.Text += "\n";
            TitleReservationPreview.Text += $"Dzień : {AddZero(StartDateTime.Day) + "." + AddZero(StartDateTime.Month) + "." + StartDateTime.Year}";
            TitleReservationPreview.FontSize = 16;
            TitleReservationPreview.TextAlignment = TextAlignment.Center;
            ReservationPreview.Children.Clear();
            if (ClickedButton != null)
                ClickedButton.Background = Brushes.Transparent;
            StackPanel_RoomButtons.Children.OfType<Button>().FirstOrDefault().Background = Brushes.Transparent;
        }
        private void SelectedChanged_EndTime(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_EndMinute.SelectedItem == null)
            {
                ComboBox_EndMinute.Items.Add("00");
                ComboBox_EndMinute.SelectedIndex = 0;
            }

            if (ComboBox_EndHour.SelectedItem != null && ComboBox_EndMinute.SelectedItem != null)
            {

                double doub;


                datetime = ((DateTime)Calendar1.SelectedDate);

                doub = Convert.ToDouble(ComboBox_EndHour.SelectedItem.ToString());

                datetime = datetime.AddHours(doub);



                doub = Convert.ToDouble(ComboBox_EndMinute.SelectedItem.ToString());

                datetime = datetime.AddMinutes(doub);
                EndDateTime = datetime;
                QueryButtom();
            }

        }

        private void Reservation_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Calendar1.SelectedDate != null
            && ComboBox_StartHour.SelectedItem != null
            && ComboBox_StartMinute.SelectedItem != null
            && ComboBox_EndHour.SelectedItem != null
            && ComboBox_EndMinute.SelectedItem != null
            && Room != null)
            {
                using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
                {
                    int RoomId;
                    try
                    {
                        if (SqlConn.State == ConnectionState.Closed)
                            SqlConn.Open();
                        String Qyery = $"Select Id from tblRooms Where RoomName=\'{Room}\'";
                        using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                        {
                            using (SqlDataReader Reader = Command.ExecuteReader())
                            {
                                Reader.Read();
                                RoomId = Reader.GetInt32(0);
                            }
                        }
                        bool NotReserved = true;
                        Qyery = $"SELECT StartHour,StartMinute,EndHour,EndMinute from Stays WHERE RoomName=\'{Room}\' AND StartYear={StartDateTime.Year} AND StartMonth={StartDateTime.Month} AND StartDay={StartDateTime.Day}";
                        using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                        {

                            using (SqlDataReader Reader = Command.ExecuteReader())
                            {
                                int StartHour;
                                int StartMinut;
                                int EndHour;
                                int EndMinut;
                                while (Reader.Read())
                                {
                                    StartHour = Reader.GetInt32(0);
                                    StartMinut = Reader.GetInt32(1);
                                    EndHour = Reader.GetInt32(2);
                                    EndMinut = Reader.GetInt32(3);

                                    if (((StartHour * 100 + StartMinut >= StartDateTime.Hour * 100 + StartDateTime.Minute && StartDateTime.Hour * 100 + StartDateTime.Minute <= EndHour * 100 + EndMinut)
                                    && (StartHour * 100 + StartMinut >= EndDateTime.Hour * 100 + EndDateTime.Minute && EndDateTime.Hour * 100 + EndDateTime.Minute <= EndHour * 100 + EndMinut))
                                    || ((StartHour * 100 + StartMinut <= StartDateTime.Hour * 100 + StartDateTime.Minute && StartDateTime.Hour * 100 + StartDateTime.Minute >= EndHour * 100 + EndMinut)
                                    && (StartHour * 100 + StartMinut <= EndDateTime.Hour * 100 + EndDateTime.Minute && EndDateTime.Hour * 100 + EndDateTime.Minute >= EndHour * 100 + EndMinut)))
                                    {
                                        NotReserved = true;
                                    }
                                    else
                                    {
                                        NotReserved = false;
                                        break;
                                    }
                                }
                            }

                        }
                        if (NotReserved)
                        {
                            Qyery = $"EXEC [dbo].[Add_Reservation] @Room={RoomId},@StartYear={StartDateTime.Year},@StartMonth={StartDateTime.Month},@StartDay={StartDateTime.Day},@StartHour={StartDateTime.Hour},@StartMinute={StartDateTime.Minute},@EndHour={EndDateTime.Hour},@EndMinute={EndDateTime.Minute},@Person_ID={IndexPeople}";
                            //insert//EXEC [dbo].[Add_Reservation] @Room = N'311',	@Start = N'11-11-2021',	@End = N'11-12-2021',@Person_ID = 1 
                            using (SqlCommand Command2 = new SqlCommand(Qyery, SqlConn))
                            {
                                if (Command2.ExecuteNonQuery() != 0)
                                {
                                    MessageBox.Show("Pokój został zarezerwowamy poprawnie!");
                                    ComboBox_StartHour.SelectedItem = null;
                                    ComboBox_StartMinute.SelectedItem = null;
                                    ComboBox_EndHour.SelectedItem = null;
                                    ComboBox_EndMinute.SelectedItem = null;
                                    ComboBox_tblPeople.SelectedItem = null;
                                    Room = null;
                                }
                                else
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Pokój {Room} jest zajety w tych godzinach!");
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
            else if (Calendar1.SelectedDate == null
            || ComboBox_StartHour.SelectedItem == null
            || ComboBox_StartMinute.SelectedItem == null
            || ComboBox_EndHour.SelectedItem == null
            || ComboBox_EndMinute.SelectedItem == null)
            {
                MessageBox.Show("Wypełnij wszystkie dane!");
            }
            else if (Room == null)
            {
                MessageBox.Show("Wybierz pokój");
            }
            else
            {
                MessageBox.Show("Bład rezerwacji!");
            }
            Refresh_StackPanel_ReservationPreview();
            Refresh_StackPanel_RoomButtons();
        }
        private void ComboBox_tblPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_tblPeople.SelectedItem != null)
            {
                String[] StringToQuery = ComboBox_tblPeople.SelectedItem.ToString().Split(' ');
                using (SqlConnection SqlConn = new SqlConnection(StrSqlConnection))
                {
                    try
                    {
                        if (SqlConn.State == ConnectionState.Closed)
                            SqlConn.Open();
                        String Qyery = $"Select Id from tblPeople where Name=\'{StringToQuery[0]}\' AND Lastname=\'{StringToQuery[1]}\'";
                        using (SqlCommand Command = new SqlCommand(Qyery, SqlConn))
                        {
                            using (SqlDataReader Reader = Command.ExecuteReader())
                            {
                                while (Reader.Read())
                                {
                                    IndexPeople = Reader.GetInt32(0);
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

        }

        private void MenuReservationView_TextBlock_MouseDown(object sender, RoutedEventArgs e)
        {
            ReservationViewWindow.Show();
            ReservationViewWindow.Left = this.Left;
            ReservationViewWindow.Top = this.Top;
            ReservationViewWindow.Width = this.Width;
            ReservationViewWindow.Height = this.Height;
            Hide();
        }
        private void MenuAddDelete_TextBlock_MouseDown(object sender, RoutedEventArgs e)
        {
            AddDeleteWindow.Show();
            AddDeleteWindow.Left = this.Left;
            AddDeleteWindow.Top = this.Top;
            AddDeleteWindow.Width = this.Width;
            AddDeleteWindow.Height = this.Height;
            Hide();
        }

        private void OnDropDownOpened_ComboBox_tblPeople(object sender, EventArgs e)
        {
            Refresh_ComboBoxItem_tblPeople();
        }
        private void OnDropDownOpened_ComboBox_EndHour(object sender, EventArgs e)
        {
            ComboBox_EndHour.Items.Clear();
            for (int i = StartDateTime.Hour; i < 24; i++)
            {
                ComboBox_EndHour.Items.Add(AddZero(i));
            }
            ComboBox_EndMinute.Items.Clear();
        }
        private void OnDropDownOpened_ComboBox_EndMinute(object sender, EventArgs e)
        {
            if (ComboBox_EndHour.SelectedItem != null && ComboBox_StartHour.SelectedItem != null)
            {
                if (ComboBox_EndHour.SelectedItem.ToString() == ComboBox_StartHour.SelectedItem.ToString())
                {
                    for (int i = Convert.ToInt32(ComboBox_StartMinute.SelectedItem.ToString()); i < 60; i++)
                    {
                        ComboBox_EndMinute.Items.Add(AddZero(i));
                    }
                }
                else
                {
                    if (ComboBox_EndMinute.Items.Count != 60) ComboBox_EndMinute.Items.Clear();
                    for (int i = 0; i < 60; i++)
                    {
                        ComboBox_EndMinute.Items.Add(AddZero(i));
                    }
                }
            }
        }

    }
}