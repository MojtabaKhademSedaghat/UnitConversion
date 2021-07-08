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
            RuleFor(x => x.FormulaFromUnit).NotEmpty().Matches(@"[+*/-]").WithMessage("Plaese checke Mathematical Operators");
            //check formul : use - + * /
            RuleFor(x => x.FormulaToUnit).NotEmpty().Matches(@"[+*/-]").WithMessage("Plaese checke Mathematical Operators");

            //check formul : use a
            RuleFor(x => x.FormulaToUnit).NotEmpty().Matches(@"[a]").WithMessage("just use 'a' in fourmola");
            //check formul : use a
            RuleFor(x => x.FormulaFromUnit).NotEmpty().Matches(@"[a]").WithMessage("just use 'a' in fourmola");
        }
    }
}
