using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VeeValidate.AspNetCore.Sample.Pages
{
    public class FluentValidationModel : PageModel
    {
        [Display(Name = "Not Empty")]
        public string NotEmpty { get; set; }

        public string Matches { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Credit Card")]
        public string CreditCard { get; set; }

        [Display(Name = "Inclusive")]
        public int? Range { get; set; }

        [Display(Name = "Inclusive Date")]
        public DateTime? DateRange { get; set; }

        public string Compare { get; set; }

        public string Confirm { get; set; }

        [Display(Name = "Minimum")]
        public int? MinValue { get; set; }

        [Display(Name = "Minimum Date")]
        public DateTime? MinDate { get; set; }

        [Display(Name = "Maximum")]
        public int? MaxValue { get; set; }

        [Display(Name = "Maximum Date")]
        public DateTime? MaxDate { get; set; }

        [Display(Name = "Minimum Length")]
        public string MinLength { get; set; }

        [Display(Name = "Maximum Length")]
        public string MaxLength { get; set; }

        [Display(Name = "Length")]
        public string StringLength { get; set; }

        public void OnGet()
        {
        }
    }

    public class FluentValidationModelValidator : AbstractValidator<FluentValidationModel>
    {
        public FluentValidationModelValidator()
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

            RuleFor(x => x.MinValue)
                .GreaterThanOrEqualTo(3);

            RuleFor(x => x.MinDate)
                .GreaterThanOrEqualTo(new DateTime(2018, 3, 1));

            RuleFor(x => x.MaxValue)
                .LessThanOrEqualTo(10);

            RuleFor(x => x.MaxDate)
                .LessThanOrEqualTo(new DateTime(2018, 12, 31));

            RuleFor(x => x.MinLength)
                .MinimumLength(3);

            RuleFor(x => x.MaxLength)
                .MaximumLength(6);

            RuleFor(x => x.StringLength)
                .Length(3, 6);

            RuleFor(x => x.Compare)
                .Equal(x => x.Confirm);
        }
    }
}