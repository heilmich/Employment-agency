using Microsoft.Win32;
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

namespace Employment_agency
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public Соискатель currentUser = new Соискатель();
        public ProfilePage(Аккаунт acc)
        {
            InitializeComponent();
            currentUser = acc.Соискатель.FirstOrDefault();
            this.DataContext = currentUser;
            Update();

        }

        public ProfilePage(Соискатель acc)
        {
            InitializeComponent();

            currentUser = acc;
            this.DataContext = currentUser;

            btnEditPhoto.Visibility = Visibility.Collapsed;

            Update();

        }

        public void Update() 
        {
            lvResume.ItemsSource = currentUser.Резюме.ToList();
        }


        private void Click_btnEditPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg; *.png; *.jpeg";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Выберите изображение";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != null)
                MainWindow.Serialize(openFileDialog.FileName);
            else MessageBox.Show("Вы не выбрали изображение");
        }
    }
}
