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
    
    public partial class Резюме
    {
        public int Код_резюме { get; set; }
        public string Желаемая_должность { get; set; }
        public string Образование { get; set; }
        public string Последнее_место_работы { get; set; }
        public int Стаж_работы { get; set; }
        public string О_себе { get; set; }
        public int Код_соискателя { get; set; }
    
        public virtual Соискатель Соискатель { get; set; }
    }
}
