//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class year
    {
        public year()
        {
            this.salaries = new HashSet<salary>();
        }
    
        public int id { get; set; }
        public string year1 { get; set; }
    
        public virtual ICollection<salary> salaries { get; set; }
    }
}
