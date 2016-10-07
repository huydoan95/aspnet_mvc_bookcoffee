namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên tin tức")]
        [Display(Name ="Tên tin tức")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name ="Tóm tắt")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name ="Hình đại diện")]
        public string Image { get; set; }

        [Display(Name ="Loại tin")]
        [Required(ErrorMessage ="Vui lòng chọn loại tin")]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name ="Chi tiết tin")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        [Display(Name ="Ngày tạo")]
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

        [Display(Name = "Cho phép đăng")]
        public bool Status { get; set; }

        [Display(Name = "Tin nóng nhất")]
        public bool TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }
    }
}
