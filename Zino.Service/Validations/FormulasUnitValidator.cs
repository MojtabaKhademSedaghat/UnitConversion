using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Zino.Service.DTOs;

namespace Zino.Service.Validations
{
    public class FormulasUnitValidator : AbstractValidator<FormulasUnitDTO>
    {
        public FormulasUnitValidator()
        {
            //check formul : use - + * /
            RuleFor(x => x.Formula).NotEmpty().Matches(@"[+*/-]").WithMessage("Plaese checke Mathematical Operators");

            //check formul : use a
            RuleFor(x => x.Formula).NotEmpty().Matches(@"[a]").WithMessage("just use 'a' in fourmola");

            //RuleFor(x => x.EnglishName).NotEmpty();
        }
    }
    public class CoefficientUnitValidator : AbstractValidator<CoefficientUnitDTO>
    {
        public CoefficientUnitValidator()
        {
            RuleFor(x => x.Symbols).NotEmpty();
            RuleFor(x => x.Value).NotEqual(0).WithMessage("value should upper 0!");
        }
    }
}
