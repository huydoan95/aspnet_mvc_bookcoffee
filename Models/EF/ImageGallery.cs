namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ImageGallery
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Album")]
        [Required(ErrorMessage = "Vui lòng nhập tên Album")]
        public string Name { get; set; }

        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Display(Name = "Tóm tắt")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Detail { get; set; }

        [Display(Name ="Hình đại diện")]
        public string ImagePath { get; set; }

        [Column(TypeName ="xml")]
        public string ImageDetail { get; set; }
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

        public bool Status { get; set; }

        public bool TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
