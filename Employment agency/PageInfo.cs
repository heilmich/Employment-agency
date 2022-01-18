using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment_agency
{
    public class PageInfo
    {
        public int pageIndex = 1;                                   // индекс текущей страницы
        public int pageSize;                                   // размер страницы
        public int visiblePagesCount;                           // количество отображаемых страниц в pageList
        public int totalItems;                                   // кол-во объектов
        public PageInfo(int size, int visibleCount, int total) 
        {
            pageSize = size;                                  
            visiblePagesCount = visibleCount;                           
            totalItems = total;
    }
        public int totalPages                                      // кол-во страниц
        {
            get { return (int)Math.Ceiling((decimal)totalItems / pageSize); }
        }
    }
}
