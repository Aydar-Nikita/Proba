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
using System.Windows.Shapes;
using enjoydemo.model;
using enjoydemo.windows;
using System.Threading;
using System.Windows.Threading;

namespace enjoydemo.windows
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        Trade1Entities dataEntities = new Trade1Entities();
        int error = 0;
        private DispatcherTimer timer;
        public Authorization()
        {
            InitializeComponent();
            GoLikeAuthorizUser.Background = new SolidColorBrush(Color.FromRgb(204, 152, 0));
            GoLikeGuest.Background = new SolidColorBrush(Color.FromRgb(204, 152, 0));
            login.Background = new SolidColorBrush(Color.FromRgb(255, 204, 0));
            password.Background = new SolidColorBrush(Color.FromRgb(255, 204, 0));
        }

        private void LikeAuthoriz(object sender, RoutedEventArgs e)
        {
            string Cheklog = login.Text;
            string Chekpass = password.Password;
            string Name = "";
            string Surname = "";
            string Patronymic = "";
            string Role = "";

            var Login = dataEntities.Users.ToList().FindAll(x => x.UserLogin == Cheklog);
            var Password = Login.ToList().FindAll(x => x.UserPassword == Chekpass);

            if (Login.Count != 0 && Password.Count != 0)
            {
                foreach (var item in Login.ToList())
                {
                    Name = item.UserName;
                    Surname = item.UserSurname;
                    Patronymic = item.UserPatronymic;
                    Role = item.Role.RoleName;
                }
                MainMenu GoToMenu = new MainMenu();
                GoToMenu.RoleInfo.Content =Role;
                GoToMenu.UserFullname.Content = Name + " " + Surname + " " + Patronymic;
                GoToMenu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("error");
                error += 1;
            }
            //
            if(error == 2)
            {
                GoLikeAuthorizUser.IsEnabled = false;
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_end);
                timer.Interval = new TimeSpan(0, 0, 10);
                timer.Start();
                MessageBox.Show("blocking 10 seconds");
                Thread.Sleep(10000);
                error = 0;
            }
        }

        private void timer_end(object sender, EventArgs e)
        {
            GoLikeAuthorizUser.IsEnabled = true;
        }
        //
        private void LikeGuest(object sender, RoutedEventArgs e)
        {
            MainMenu GoToMenu = new MainMenu();
            GoToMenu.RoleInfo.Content = "Guest";
            GoToMenu.UserFullname.Content = "-" + " " + "-" + " " + "-";
            GoToMenu.Show();
            this.Close();
        }
    }
}
