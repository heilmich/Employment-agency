﻿using System;
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
    /// Логика взаимодействия для AddResumeWindow.xaml
    /// </summary>
    public partial class AddResumeWindow : Window
    {
        public Резюме резюме;
        public AddResumeWindow(Соискатель соискатель)
        {
            InitializeComponent();
            резюме = new Резюме();
            резюме.Код_соискателя = соискатель.Код_Соискателя;
            this.DataContext = резюме;
        }

        private void Click_addResume(object sender, RoutedEventArgs e)
        {
            try 
            {

                Entities.GetContext().Резюме.Add(резюме);
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