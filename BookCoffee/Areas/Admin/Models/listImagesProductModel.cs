﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCoffee.Areas.Admin.Models
{
    public class listImagesProductModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<string> images { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }

    }
}