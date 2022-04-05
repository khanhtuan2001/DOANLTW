namespace ChickenGang_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuan = new HashSet<BinhLuan>();
            ChiTietDonDat = new HashSet<ChiTietDonDat>();
            ChiTietPhieuNhap = new HashSet<ChiTietPhieuNhap>();
        }

        [Key]
        public int MaSP { get; set; }

        [StringLength(255)]
        public string TenSP { get; set; }

        public decimal? DonGia { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string Mota { get; set; }

        public string Mota2 { get; set; }

        public string Mota3 { get; set; }

        public string HinhAnh { get; set; }

        public int? SoLuongTon { get; set; }

        public int? LuotXem { get; set; }

        public int? LuotBinhChon { get; set; }

        public int? LuotBinhLuan { get; set; }

        public int? SoLanMua { get; set; }

        public int? Moi { get; set; }

        public int? MaNCC { get; set; }

        public int? MaNSX { get; set; }

        public int? MaLoaiSP { get; set; }

        public int? DaXoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonDat> ChiTietDonDat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }
        public List<NhaCungCap> ListNCC = new List<NhaCungCap>();
        public List<NhaSanXuat> ListNSX = new List<NhaSanXuat>();
        public List<LoaiSanPham> ListLSP = new List<LoaiSanPham>();

    }
}
