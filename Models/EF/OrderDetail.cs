namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public long ID { get; set; }

        public long? Order_ID { get; set; }

        public long? ProductID { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }
    }
}
