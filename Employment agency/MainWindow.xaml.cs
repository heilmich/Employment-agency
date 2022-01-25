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
using System.Text.Json;
using System.Data.Entity;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Globalization;


namespace Employment_agency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public class ImageConverter : IValueConverter // конвертер изображений для привязки
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "placeholderuser.png";

            return MainWindow.DeserializeImage((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
    public partial class MainWindow : Window
    {
        public static Аккаунт currentAcc = new Аккаунт();
        public static Соискатель currentUser = new Соискатель();
        public static MainWindow currentWindow;
        ResumesPage resumesPage;
        VacanciesPage vacanciesPage;
        ProfilePage profilePage;
        CompanyPage companyPage;


        public MainWindow(Аккаунт acc)
        {
            InitializeComponent();
            currentAcc = acc;
            currentUser = acc.Соискатель.FirstOrDefault();
            currentWindow = this;

            if (currentAcc.Тип_аккаунта == 1)
            {
                profilePage = new ProfilePage(acc);
                MainFrame.Navigate(profilePage);
            }
            else if (currentAcc.Тип_аккаунта == 2)
            {
                companyPage = new CompanyPage(acc);
                MainFrame.Navigate(companyPage);
            }

            
            vacanciesPage = new VacanciesPage();
            resumesPage = new ResumesPage();
        }

        public void Navigate(Page page) 
        {
            MainFrame.Navigate(page);
        }

        private void Click_labelVacancies(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(vacanciesPage);
        }

        private void Click_imageProfile(object sender, MouseButtonEventArgs e)
        {
            if (currentAcc.Тип_аккаунта == 1)
                MainFrame.Navigate(profilePage);
            else if (currentAcc.Тип_аккаунта == 2)
                MainFrame.Navigate(companyPage);
        }

        public static BitmapImage DeserializeImage(string value) // десериализация json строки из БД в изображение
        {
            byte[] array = System.Convert.FromBase64String(JsonSerializer.Deserialize<string>((string)value));
            MemoryStream ms = new MemoryStream(array, 0, array.Length);
            BitmapImage img = new BitmapImage();

            img.BeginInit();
            img.StreamSource = ms;
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            img.Freeze();
            return img;
        }

        public static void Serialize(string path)
        {
            string pic;
            DirectoryInfo dirInfo = new DirectoryInfo(path);

            using (FileStream fs = File.OpenRead(path))
            {
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length);

                pic = JsonSerializer.Serialize(array);
                if (pic == null)
                {
                    MessageBox.Show("Файл не выбран");
                    return;
                }
                currentAcc.Фотография = pic;
                Entities.GetContext().SaveChanges();
            }
        }

        private void Click_labelResumes(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(resumesPage);

        }


    }


}
