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
    /// Логика взаимодействия для EditCompanyWindow.xaml
    /// </summary>
    public partial class EditCompanyWindow : Window
    {
        public Организация currentOrg; 
        public EditCompanyWindow(Организация currentOrg)
        {
            InitializeComponent();
            this.currentOrg = currentOrg;
            companyInfoB.DataContext = this.currentOrg;
        }

        private void SaveBTN_Click(object sender, RoutedEventArgs e)
        {
            Entities.GetContext().SaveChanges();
        }
    }
}
