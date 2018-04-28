using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.Sample.Pages
{
    public class AttributesModel : PageModel
    {
        [Required]
        public string Required { get; set; }

        [Url]        
        public string Url { get; set; }

        [RegularExpression("/^[A-Z0-9]*$/")]
        public string Regex { get; set; }

        [EmailAddress]
        [Display(Name ="Email Address")]
        public string EmailAddress { get; set; }

        [CreditCard]
        [Display(Name = "Credit Card")]
        public string CreditCard { get; set; }

        [Range(1, 10)]
        public int? Range { get; set; }

        [Range(typeof(DateTime), "2018/01/01", "2018/12/25")]
        [Display(Name = "Date Range")]
        public DateTime? DateRange { get; set; }

        [Compare(nameof(CompareTo))]
        public string Compare { get; set; }

        public string CompareTo { get; set; }

        [MinLength(3)]
        public string MinLength { get; set; }

        [MaxLength(6)]
        public string MaxLength { get; set; }

        [StringLength(6, MinimumLength = 3)]
        public string StringLength { get; set; }

        [FileExtensions(Extensions = "jpg,png,gif")]
        public string FileExtensions { get; set; }

        public void OnGet()
        {
        }
    }
}
