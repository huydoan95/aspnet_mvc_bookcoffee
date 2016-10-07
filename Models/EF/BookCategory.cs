namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookCategory")]
    public partial class BookCategory
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên loại sách")]
        [Display(Name ="Tên loại sách")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        [Display(Name ="Thứ tự xuất hiện")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

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

        [Display(Name = "Đang sử dung")]
        public bool Status { get; set; }

        [Display(Name = "Lên trang chủ")]
        public bool ShowOnHome { get; set; }
    }
}
