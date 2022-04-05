namespace ChickenGang_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonDatHang")]
    public partial class DonDatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonDatHang()
        {
            ChiTietDonDat = new HashSet<ChiTietDonDat>();
        }

        [Key]
        public int MaDDH { get; set; }

        public DateTime? NgayDat { get; set; }

        public bool? TinhTrangGiaoHang { get; set; }

        public DateTime? NgayGiao { get; set; }

        public bool? DaThanhToan { get; set; }

        public int? MaKH { get; set; }

        public int? UuDai { get; set; }

        public string DiaChiGiaoHang { get; set; }

        [StringLength(11)]
        public string Sdt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDat> ChiTietDonDat { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
