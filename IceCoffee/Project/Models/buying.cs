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
    
    public partial class buying
    {
        public int id { get; set; }
        public Nullable<int> raw { get; set; }
        public Nullable<decimal> count { get; set; }
        public Nullable<int> worker { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<decimal> sum { get; set; }
    
        public virtual raw raw1 { get; set; }
        public virtual worker worker1 { get; set; }
    }
}
