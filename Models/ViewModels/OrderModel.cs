using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models.ViewModels
{
    public class OrderModel
    {
        public long ID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        [Display(Name ="Họ và tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name ="Số điện thoại")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Yêu cầu nhập ký tự số")]
        [Required(ErrorMessage ="Vui lòng để lại số điện thoại")]
        public string Mobile { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(250)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng Email")]
        public string Email { get; set; }

        [Display(Name ="Ngày tổ chức")]
        [Required(ErrorMessage ="Vui lòng cho biết ngày tổ chức")]
        public DateTime? HoldDate { get; set; }

        [Display(Name ="Ghi chú")]
        [Column(TypeName = "ntext")]
        public string Remark { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập số người dự kiến")]
        [Display(Name ="Số người")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Yêu cầu nhập ký tự số")]
        public int Quantity { get; set; }

        public bool status { get; set; }
    }
}