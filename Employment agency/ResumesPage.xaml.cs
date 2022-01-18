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
    /// Логика взаимодействия для ResumesPage.xaml
    /// </summary>
    public partial class ResumesPage : Page
    {
        public ResumesPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            lvResumes.ItemsSource = Entities.GetContext().Резюме.ToList();
        }
    }
}
