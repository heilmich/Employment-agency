//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Employment_agency
{
    using System;
    using System.Collections.Generic;
    
    public partial class Вакансия
    {
        public int Код_вакансии { get; set; }
        public int Код_организации { get; set; }
        public string Название { get; set; }
        public string Должность { get; set; }
        public decimal Базовый_оклад { get; set; }
        public string Описание { get; set; }
        public string Требование { get; set; }
        public string Адрес { get; set; }
    
        public virtual Организация Организация { get; set; }
    }
}
