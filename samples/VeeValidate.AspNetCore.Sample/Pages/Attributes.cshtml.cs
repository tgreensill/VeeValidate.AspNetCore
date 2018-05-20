using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.Sample.Pages
{
    [Route("api/[controller]")]
    public class AttributesController : Controller
    {
        [HttpPost]
        public IActionResult OnPost([FromBody]AttributesModel.InputModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }

    public class AttributesModel : PageModel
    { 
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
            Input = new InputModel();
        }
       
        public class InputModel
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

            [Range(typeof(DateTime), "2018/01/01", "2018/12/25")]
            [Display(Name = "Date Range")]
            public DateTime? DateRange { get; set; }

            [Display(Name="Compare To")]
            public string CompareTo { get; set; }

            [Compare(nameof(CompareTo))]
            public string Compare { get; set; }            

            [MinLength(3)]
            [Display(Name = "Min Length")]
            public string MinLength { get; set; }

            [MaxLength(6)]
            [Display(Name = "Max Length")]
            public string MaxLength { get; set; }

            [StringLength(6, MinimumLength = 3)]
            [Display(Name = "String Length")]
            public string StringLength { get; set; }

            [FileExtensions(Extensions = "jpg,png,gif")]
            [Display(Name = "File Extensions")]
            public string FileExtensions { get; set; }
        }
    }

    
}
