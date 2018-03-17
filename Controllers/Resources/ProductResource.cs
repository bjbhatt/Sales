using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sales.Controllers.Resources
{
    //[Table("Products", Schema = "dbo")]
    public class ProductResource
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImageURL { get; set; }
        public string ThumbnailURL { get; set; }

    }
}