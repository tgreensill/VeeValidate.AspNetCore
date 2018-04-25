using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.Sample.Pages
{
    public class ExamplesModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {

        }

        public class InputModel
        {
            [Required]
            public string Required { get; set; }

            [CreditCard]
            [Display(Name = "Credit Card")]
            public string CreditCard { get; set; }
        }
    }
}
