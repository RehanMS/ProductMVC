using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMVC.Models
{
    public class CreateViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }       
        public int CategoryId { get; set; }
        public List<Category> Category { get; set; }
    }
}