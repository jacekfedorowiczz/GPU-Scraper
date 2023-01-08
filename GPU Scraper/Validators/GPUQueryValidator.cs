using FluentValidation;
using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPUScraper.Validators
{
    public class GPUQueryValidator : AbstractValidator<GPUQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15, };
        private string[] allowedSortByColumnNames = new[] { nameof(GPU.Name), nameof(GPU.LowestPrice), nameof(GPU.HighestPrice) };
        public GPUQueryValidator()
        {
            RuleFor(x => x.pageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Numer strony musi być większy lub równy 1.");

            RuleFor(x => x.pageSize)
                .Custom((value, context) =>
                {
                    if (!allowedPageSizes.Contains(value))
                    {
                        context.AddFailure("PageSize", $"Wartość PageSize musi mieć następujące wartości: [{string.Join(',', allowedPageSizes)}]");
                    }
                });

            RuleFor(x => x.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Wartość SortBy jest opcjonalna lub musi przyjmować następujące wartości: [{string.Join(',', allowedSortByColumnNames)}]");
        }
    }
}
