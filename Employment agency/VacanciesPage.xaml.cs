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
        public int filterIndex = 0;                             //индекс фильтрации
        public PageInfo pageInfo = new PageInfo(10, 4, 0);      //объект информации о странице
        public string searchQuery;                              //поисковый запрос
        public SortItem currentSort;                            //объект сортировки
        public List<SortItem> sortList = new List<SortItem>     // лист с объектами сортировки
        {
            new SortItem("DESC", "Вакансия.Код_вакансии", "Без сортировки"),
            new SortItem("ASC", "Вакансия.Базовый_оклад", "По возрастанию: оклад"),
            new SortItem("DESC", "Вакансия.Базовый_оклад", "По убыванию: оклад")
        };


        public VacanciesPage()
        {
            InitializeComponent();
            sortCB.ItemsSource = sortList;
            currentSort = sortList[0];
            Update();
            
        }

        public void Update()
        {
            lvVacancies.ItemsSource = TakeData();
            Pagination();
        }

        public ObservableCollection<Вакансия> TakeData()
        {
            try
            {
                return new ObservableCollection<Вакансия>(Entities.GetContext().Вакансия.SqlQuery($"SELECT * FROM Вакансия " +
                             $"JOIN Организация ON Вакансия.Код_организации = Организация.Код_Организации " +
                             $"WHERE ( Вакансия.Должность LIKE N'%{searchQuery}%' OR Вакансия.Адрес LIKE N'%{searchQuery}%') " +
                             $" AND ( Вакансия.Базовый_оклад >= {tbMinZP.Text} AND Вакансия.Базовый_оклад <= {tbMaxZP.Text} ) " +
                             $"ORDER BY {currentSort.SortProperty} {currentSort.SortDir} " +
                             $"OFFSET {(pageInfo.pageIndex - 1) * pageInfo.pageSize} ROWS " +
                             $"FETCH NEXT {pageInfo.pageSize} ROWS ONLY; "));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка:\n" + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }


        public void Pagination()    // функция разбивки на страницы
        {
            pageList.Children.Clear(); // чистим список элементов

            // расчёт кол-ва элементов в запросе
            pageInfo.totalItems = Entities.GetContext().Вакансия.Count();


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

        private void Search_field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            searchQuery = search_field.Text;
            Update();
        }

        private void Search_Click(object sender, MouseButtonEventArgs e)
        {
            searchQuery = search_field.Text;
            Update();
        }

        private void Click_Company(object sender, MouseButtonEventArgs e)
        {
            CompanyPage companyPage = new CompanyPage(((Вакансия)(lvVacancies.SelectedItem)).Организация);
            MainWindow.currentWindow.Navigate(companyPage);
        }
    }
}
