using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zino.Service.DTOs;
using Zino.Service.Services;
using Zino.Service.Validations;

namespace Zino.Api.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UnitConvertController : Controller
    {

        private readonly ILogger<UnitConvertController> _logger;
        private readonly IServiceConvert _serviceConvert;

        public UnitConvertController(ILogger<UnitConvertController> logger, IServiceConvert serviceConvert)
        {
            _logger = logger;
            _serviceConvert = serviceConvert;
        }

        [HttpPost]
        public async Task<IActionResult> UnitWithFormula([FromBody] FormulasUnitDTO request)
        {
            try
            {
                var validator = await new FormulasUnitValidator().ValidateAsync(request);
                if (validator.IsValid)
                    return Json(_serviceConvert.UnitWithFormula(request));
                else
                    throw new Exception(string.Join(",", validator.Errors));
            }
            catch (Exception ex)
            {
                return ErrorManagment.CustombadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CoefficientUnit(CoefficientUnitDTO request)
        {
            try
            {
                var validator = await new CoefficientUnitValidator().ValidateAsync(request);
                if (validator.IsValid)
                    return Json(_serviceConvert.CoefficientUnit(request));
                else
                    throw new Exception(string.Join(",", validator.Errors));
            }
            catch (Exception ex)
            {
                return ErrorManagment.CustombadRequest(ex);
            }
        }
    }
}
