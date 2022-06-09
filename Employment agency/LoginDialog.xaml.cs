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

namespace Employment_agency
{
    /// <summary>
    /// Логика взаимодействия для LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void btn_SignIn(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            Аккаунт user = (Аккаунт)authorization.SignIn(login.Text, password.Password, Entities.GetContext());

            if (user == null) return;

            MainWindow.currentAcc = user;
           
            this.Close();
        }


        private void btn_SignUp_Click(object sender, RoutedEventArgs e)
        {
            Аккаунт acc = new Аккаунт();
            Организация org = new Организация();

            acc.Тип_аккаунта = 2;
            acc.Логин = loginSignUp.Text;
            acc.Пароль = passwordSignUp.Password;

            org.Название = titleSignUp.Text;
            org.Номер_телефона = phoneSignUp.Text;

            Entities.GetContext().Аккаунт.Add(acc);
            Entities.GetContext().SaveChanges();

            org.Код_аккаунта = acc.Код_аккаунта;
            Entities.GetContext().Организация.Add(org);
            Entities.GetContext().SaveChangesAsync();

            MessageBox.Show("Аккаунт создан");

            Authorization authorization = new Authorization();
            Аккаунт user = (Аккаунт)authorization.SignIn(acc.Логин, acc.Пароль, Entities.GetContext());
            if (user == null) return;

            MainWindow.currentAcc = user;

            this.Close();
        }
    }

    
}
