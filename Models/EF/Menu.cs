namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [Display(Name ="Mã Menu")]
        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Vui lòng nhập tên Menu")]
        [Display(Name ="Tên Menu")]
        public string Text { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập liên kết")]
        [Display(Name ="Liên kết")]
        [StringLength(250)]
        public string Link { get; set; }

        [Display(Name ="Thứ tự xuất hiện")]
        [Required(ErrorMessage ="Vui lòng nhập số thứ tự")]
        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Vui lòng nhập kiểu mở trang")]
        [Display(Name ="Kiểu mở trang")]
        public string Target { get; set; }

        [Display(Name ="Trạng thái")]
        public bool status { get; set; }

        [Display(Name ="Thuộc MenuType")]
        public int? TypeID { get; set; }
    }
}
