namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("event")]
    public partial class _event
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên sự kiện")]
        [Display(Name ="Tên sự kiện")]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name ="Tóm tắt")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Detail { get; set; }

        [Display(Name ="Ngày tổ chức")]
        public DateTime? EventDate { get; set; }

        [StringLength(50)]
        [Display(Name ="Từ")]
        public string StartTime { get; set; }

        [StringLength(50)]
        [Display(Name = "Đến")]
        public string EndTime { get; set; }

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

        [Display(Name = "Top Hot")]
        public bool TopHot { get; set; }


        [Display(Name = "Số lượt xem")]
        public int? ViewCount { get; set; }
    }
}
