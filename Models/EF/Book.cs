namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Book
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên sách")]
        [Display(Name="Tên sách")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(50)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        [StringLength(50)]
        [Display(Name = "Nhà sản xuất")]
        public string publisher { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        public bool IncludedVAT { get; set; }

        public int? Quantity { get; set; }

        [Display(Name = "Thuộc loại sách")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người tạo")]
        public string CreatedBy { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người cập nhật")]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [Display(Name = "Đang sử dụng")]
        public bool Status { get; set; }

        [Display(Name = "Hot nhất")]
        public bool TopHot { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? ViewCount { get; set; }
    }
}
