using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Employment_agency
{
    public class SortItem : INotifyPropertyChanged
    {
        private string sortDir;     // направление сортировки
        public string SortDir
        {
            get
            { return sortDir; }
            set
            {
                if (sortDir != value)
                {
                    sortDir = value;
                    OnPropertyChanged("sortDir");
                }
            }
        }
        private string sortProperty;      // свойство(запрос) сортировки                                  
        public string SortProperty
        {
            get
            { return sortProperty; }
            set
            {
                if (sortProperty != value)
                {
                    sortProperty = value;
                    OnPropertyChanged("sortProperty");
                }
            }
        }
        private string sortTitle;   // наименование сортировки
        public string SortTitle
        {
            get
            { return sortTitle; }
            set
            {
                if (sortTitle != value)
                {
                    sortTitle = value;
                    OnPropertyChanged("sortTitle");
                }
            }
        }
        public SortItem(string dir, string prop, string title)
        {
            sortDir = dir;
            sortProperty = prop;
            sortTitle = title;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

}
