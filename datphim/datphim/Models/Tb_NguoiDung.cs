//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datphim.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tb_NguoiDung()
        {
            this.Tb_HoaDon = new HashSet<Tb_HoaDon>();
        }
    
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string TenKH { get; set; }
        public string SDT { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string Email { get; set; }
        public Nullable<bool> available { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tb_HoaDon> Tb_HoaDon { get; set; }
    }
}
