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
    /// Логика взаимодействия для CompanyPage.xaml
    /// </summary>
    public partial class CompanyPage : Page
    {
        public Организация currentCompany = new Организация();
        public CompanyPage(Аккаунт acc)
        {
            InitializeComponent();
            
            currentCompany = acc.Организация.FirstOrDefault();
            this.DataContext = currentCompany;

            Update();
        }

        public CompanyPage(Организация comp) 
        {
            InitializeComponent();

            currentCompany = comp;
            this.DataContext = currentCompany;

            btnEditPhoto.Visibility = Visibility.Collapsed;
            btnAddVacancy.Visibility = Visibility.Collapsed;
            EditInfoBTN.Visibility = Visibility.Collapsed;
            Update();
        }

        public void Update()
        {
            lvVacancies.ItemsSource = currentCompany.Вакансия.ToList();
        }

        private void Click_addVacancy(object sender, RoutedEventArgs e)
        {
            AddVacancyWindow addVacancyWindow = new AddVacancyWindow(currentCompany);
            addVacancyWindow.ShowDialog();
            Update();
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

        private void EditInfoBTN_Click(object sender, RoutedEventArgs e)
        {
            EditCompanyWindow editCompanyWindow = new EditCompanyWindow(currentCompany);
            editCompanyWindow.Show();
        }
    }
}
