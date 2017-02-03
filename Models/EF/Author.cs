namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Author")]
    public class Author
    {

        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên tác giả")]
        [Required(ErrorMessage ="Vui lòng nhập tên tác giả")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        [Display(Name = "Điện thoại")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Vui lòng nhập đúng số điện thoại")]
        public string Phone { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage ="Vui lòng nhập Email đúng")]
        public string Email { get; set; }

        [Display(Name = "Địa chỉ")]
        [StringLength(50)]
        public string Address { get; set; }

        [Display(Name ="Ghi chú thêm")]
        [StringLength(250)]
        public string Content { get; set; }

        [Display(Name ="Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name ="Trạng thái")]
        public bool status { get; set; }
    }
}
