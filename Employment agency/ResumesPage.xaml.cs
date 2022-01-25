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

using System.Collections.ObjectModel;

namespace Employment_agency
{
    /// <summary>
    /// Логика взаимодействия для ResumesPage.xaml
    /// </summary>
    public partial class ResumesPage : Page
    {
        public int filterIndex = 0;                             //индекс фильтрации
        public PageInfo pageInfo = new PageInfo(10, 4, 0);      //объект информации о странице
        public string searchQuery;                              //поисковый запрос
        public SortItem currentSort;                            //объект сортировки
        public List<SortItem> sortList = new List<SortItem>     // лист с объектами сортировки
        {
            new SortItem("ASC", "Резюме.Код_резюме", "Без сортировки"),
            new SortItem("ASC", "Резюме.Стаж_работы", "По возрастанию: стаж работы"),
            new SortItem("DESC", "Резюме.Стаж_работы", "По убыванию: стаж работы"),
            new SortItem("ASC", "Соискатель.Возраст", "По возрастанию: возраст соискателя"),
            new SortItem("DESC", "Соискатель.Возраст", "По убыванию: возраст соискателя")
        };


        public ResumesPage()
        {
            InitializeComponent();
            sortCB.ItemsSource = sortList;
            currentSort = sortList[0];
            Update();
        }

        public void Update()
        {
            lvResumes.ItemsSource = TakeData();
            Pagination();
        }

        public ObservableCollection<Резюме> TakeData()
        {
            return new ObservableCollection<Резюме>(Entities.GetContext().Резюме.SqlQuery($"SELECT * FROM Резюме " +
                         $"JOIN Соискатель ON Резюме.Код_соискателя = Соискатель.Код_Соискателя " +
                         $"WHERE ( Резюме.Желаемая_должность LIKE N'%{searchQuery}%' OR Соискатель.Адрес LIKE N'%{searchQuery}%') " +
                         $"ORDER BY {currentSort.SortProperty} {currentSort.SortDir} " +
                         $"OFFSET {(pageInfo.pageIndex - 1) * pageInfo.pageSize} ROWS " +
                         $"FETCH NEXT {pageInfo.pageSize} ROWS ONLY; "));
        }

        public void Pagination()    // функция разбивки на страницы
        {
            pageList.Children.Clear(); // чистим список элементов

            // расчёт кол-ва элементов в запросе
            pageInfo.totalItems = Entities.GetContext().Резюме.Count();


            for (int i = pageInfo.pageIndex; i <= pageInfo.totalPages; i++) // рассчёт кол-ва страниц в списке страниц
            {
                if (i >= pageInfo.pageIndex && i <= pageInfo.pageIndex + 3)
                {
                    TextBlock page = new TextBlock();
                    page.Text = Convert.ToString(i);
                    page.Style = this.FindResource("PageLabel") as Style;
                    page.MouseLeftButtonDown += SelectPageClick;
                    if (i == pageInfo.pageIndex) page.TextDecorations = TextDecorations.Underline;

                    pageList.Children.Add(page);
                }
            }
        }

        private void Search_field_KeyDown(object sender, KeyEventArgs e) // поиск, вызывается при нажатии на Enter
        {
            if (e.Key != Key.Enter) return;
            searchQuery = search_field.Text;
            Update();
        }

        private void SortChanged(object sender, SelectionChangedEventArgs e) // изменение сортировки
        {
            currentSort = (SortItem)((ComboBox)sender).SelectedItem;
            Update();
        }

        private void PrevPageClick(object sender, MouseButtonEventArgs e)
        {
            if (pageInfo.pageIndex != 0)
            {
                pageInfo.pageIndex -= 1;
                Update();
            }
        }
        private void NextPageClick(object sender, MouseButtonEventArgs e)
        {
            if (pageInfo.pageIndex != pageInfo.totalPages)
            {
                pageInfo.pageIndex += 1;
                Update();
            }
        }
        private void SelectPageClick(object sender, MouseButtonEventArgs e)
        {
            pageInfo.pageIndex = Convert.ToInt32(((TextBlock)sender).Text);
            Update();

        }

        private void Search_Click(object sender, MouseButtonEventArgs e)
        {
            searchQuery = search_field.Text;
            Update();
        }

        private void Click_User(object sender, MouseButtonEventArgs e)
        {
            Соискатель user = ((Резюме)lvResumes.SelectedItem).Соискатель;
            ProfilePage profilePage = new ProfilePage(user);
            MainWindow.currentWindow.Navigate(profilePage);
        }
    }
}
