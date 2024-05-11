using Backend.DTOs;
using FluentValidation;

namespace Backend.Validators
{
	public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
	{

		public BeerInsertValidator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Name).Length(2,18);
			RuleFor(x => x.Alcohol)
				.GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor que 0");

		}

	}
} 

