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
    /// Логика взаимодействия для AddVacancyWindow.xaml
    /// </summary>
    public partial class AddVacancyWindow : Window
    {
        public Вакансия вакансия;
        public AddVacancyWindow(Организация организация)
        {
            InitializeComponent();
            вакансия = new Вакансия();
            вакансия.Код_организации = организация.Код_Организации;
            this.DataContext = вакансия;
        }

        private void Click_addVacancy(object sender, RoutedEventArgs e)
        {
            try
            {

                Entities.GetContext().Вакансия.Add(вакансия);
                Entities.GetContext().SaveChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка", ex.Message);
            }
        }
    }
}
