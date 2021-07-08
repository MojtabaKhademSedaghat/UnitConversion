using FluentValidation;
using Microsoft.Extensions.Logging;
using Zino.Service.DTOs;

namespace Zino.Service.Validations
{
    public class MeasurementDimensionsValidator : AbstractValidator<MeasurementDimensionsDTO>
    {
        public MeasurementDimensionsValidator(ILogger<MeasurementDimensionsDTO> logger)
        {
            RuleFor(order => order.EnglishName).NotEmpty().WithMessage("EnglishName is empty!");
            RuleFor(order => order.PersianName).NotEmpty().WithMessage("PersianName is empty!");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
