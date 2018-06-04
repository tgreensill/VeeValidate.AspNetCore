using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.Sample.Pages
{
    public class IndexModel : PageModel
    {
        [Required]
        public string Required { get; set; }

        [Url]
        public string Url { get; set; }

        [RegularExpression("/^[A-Z0-9]*$/")]
        public string Regex { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [CreditCard]
        [Display(Name = "Credit Card")]
        public string CreditCard { get; set; }

        [Range(1, 10)]
        public int? Range { get; set; }

        [Range(typeof(DateTime), "2018/12/01", "2018/12/31")]
        [Display(Name = "Date Range")]
        public DateTime? DateRange { get; set; }

        public string Compare { get; set; }

        [Compare(nameof(Compare))]
        public string Confirm { get; set; }

        [MinLength(3)]
        [Display(Name = "Min Length")]
        public string MinLength { get; set; }

        [MaxLength(6)]
        [Display(Name = "Max Length")]
        public string MaxLength { get; set; }

        [StringLength(6, MinimumLength = 3)]
        [Display(Name = "String Length")]
        public string StringLength { get; set; }

        [FileExtensions(Extensions = "txt,pdf")]
        [Display(Name = "File Extensions")]
        public string FileExtensions { get; set; }

        public void OnGet()
        {

        }
    }
}
