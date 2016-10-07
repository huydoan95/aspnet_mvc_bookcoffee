namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuType")]
    public partial class MenuType
    {
        [Display(Name ="Mã MenuType")]
        public int ID { get; set; }

        [StringLength(50)]
        [Display(Name ="Tên MenuType")]
        [Required(ErrorMessage ="Vui lòng nhập tên MenuType")]
        public string Name { get; set; }

        [Display(Name ="Sử dụng")]
        public bool status { get; set; }
    }
}
