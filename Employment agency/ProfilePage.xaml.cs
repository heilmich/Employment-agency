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
            lvResume.ItemsSource = currentUser.Резюме;

        }
    }
}
