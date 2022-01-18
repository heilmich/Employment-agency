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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Globalization;

namespace Employment_agency
{
    /// <summary>
    /// Логика взаимодействия для VacanciesPage.xaml
    /// </summary>
    /// 
    
    public partial class VacanciesPage : Page
    {
        public VacanciesPage()
        {
            InitializeComponent();
            Update();
            
        }

        public void Update()
        {
            lvVacancies.ItemsSource = Entities.GetContext().Вакансия.ToList();
        }
    }
}
