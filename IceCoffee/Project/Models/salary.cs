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
    
    public partial class salary
    {
        public long id { get; set; }
        public Nullable<int> worker { get; set; }
        public Nullable<byte> month { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<decimal> summm { get; set; }
        public Nullable<decimal> premium { get; set; }
        public Nullable<long> count { get; set; }
        public Nullable<decimal> payment { get; set; }
        public Nullable<long> count_making { get; set; }
        public Nullable<long> count_buying { get; set; }
        public Nullable<long> count_selling { get; set; }
    
        public virtual worker worker1 { get; set; }
        public virtual month month1 { get; set; }
        public virtual year year1 { get; set; }
    }
}
