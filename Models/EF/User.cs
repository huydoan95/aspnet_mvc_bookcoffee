namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập")]
        [StringLength(50)]
        [Display(Name="Tên đăng nhập")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [StringLength(200)]
        [Display(Name ="Mật khẩu")]
        [MinLength(6,ErrorMessage ="Mật khấu ít nhất 6 ký tự")]
        //[RegularExpression(@"(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).*", ErrorMessage ="Password gồm ít nhât 1 ký tự IN, ít nhất 1 ký tự THƯỜNG, ít nhất 1 ký tự SỐ, và ít nhất 6 ký tự")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }


        [StringLength(50)]
        [Display(Name ="Họ và tên")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name ="Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage ="Vui lòng nhập đúng định dạng Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name ="Điện thoại")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Yêu cầu nhập ký tự số")]
        public string Phone { get; set; }

        [Display(Name ="Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name ="Tạo bởi")]
        public string CreatedBy { get; set; }

        [Display(Name ="Ngày chỉnh sửa")]

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name ="Chỉnh sửa bởi")]
        public string ModifiedBy { get; set; }

        [Display(Name ="Trạng thái")]
        public bool Status { get; set; }
    }
}
