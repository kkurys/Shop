using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class News
    {
        public int NewsID { get; set; }
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Treść")]
        public string Content { get; set; }
        [Display(Name = "Obrazek")]
        public string ImgFilename { get; set; }
        [Display(Name = "Data utworzenia")]
        public DateTime? CreationDate { get; set; }
        [Display(Name = "Autor")]
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}