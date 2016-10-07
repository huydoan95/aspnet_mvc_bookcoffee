namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên sản phẩm")]
        [Display(Name ="Tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô tả vè sản phẩm")]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [Display(Name = "Giá bán")]
        [DataType(DataType.Currency,ErrorMessage ="Vui lòng nhập giá đúng")]
        public decimal? Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [DataType(DataType.Currency, ErrorMessage = "Vui lòng nhập giá đúng")]
        public decimal? PromotionPrice { get; set; }

        [Display(Name = "Đã có VAT")]
        public bool IncludedVAT { get; set; }

        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Display(Name = "Thuộc loại")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi tiết về SP")]
        public string Detail { get; set; }

        [Display(Name = "Bảo hành")]
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

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [Display(Name ="Bán chạy")]
        public bool TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
