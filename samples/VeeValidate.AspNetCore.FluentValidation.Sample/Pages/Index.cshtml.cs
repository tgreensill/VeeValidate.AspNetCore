using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.FluentValidation.Sample.Pages
{
    public class IndexModel : PageModel
    {
        [Display(Name = "Not Empty")]
        public string NotEmpty { get; set; }

        public string Matches { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Credit Card")]
        public string CreditCard { get; set; }

        public int? Range { get; set; }

        [Display(Name = "Date Range")]
        public DateTime? DateRange { get; set; }

        public string Compare { get; set; }

        public string Confirm { get; set; }

        [Display(Name = "Min Length")]
        public string MinLength { get; set; }

        [Display(Name = "Max Length")]
        public string MaxLength { get; set; }

        public void OnGet()
        {

        }
    }

    public class IndexModelValidator : AbstractValidator<IndexModel>
    {
        public IndexModelValidator()
        {
            RuleFor(x => x.NotEmpty)
                .NotEmpty();

            RuleFor(x => x.Matches)
                .Matches("/^[A-Z0-9]*$/");

            RuleFor(x => x.EmailAddress)
                .EmailAddress();

            RuleFor(x => x.CreditCard)
                .CreditCard();

            RuleFor(x => x.Range)
                .InclusiveBetween(1, 10);

            RuleFor(x => x.DateRange)
                .InclusiveBetween(new DateTime(2018, 12, 1), new DateTime(2018, 12, 31));

            RuleFor(x => x.MinLength)
                .MinimumLength(3);

            RuleFor(x => x.MaxLength)
                .MaximumLength(6);

            RuleFor(x => x.Confirm)
                .Equal(x => x.Compare);
        }
    }
}
