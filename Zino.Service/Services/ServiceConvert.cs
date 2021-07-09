using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Zino.Core.Utilities;
using Zino.Service.DTOs;
using Zino.Service.Enums;

namespace Zino.Service.Services
{
    public class ServiceConvert : IServiceConvert
    {
        #region [Props]
        public ILogger _logger { get; }
        public IUnitFurmola _unitFurmola { get; }
        #endregion [Props]

        #region [Ctor]

        public ServiceConvert(ILoggerFactory logger, IUnitFurmola unitFurmola)
        {
            _logger = logger.CreateLogger("ServiceConvert");
            _unitFurmola = unitFurmola;
        }

        #endregion [Ctor]

        #region [Methods]

        /// <summary>
        /// convert units
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Dictionary of Convert units</returns>
        public Dictionary<string, Unit> CoefficientUnit(CoefficientUnitDTO request)
        {
            Dictionary<string, Unit> result = new Dictionary<string, Unit>();
            try
            {
                //daryafte unit morede nazar baraye tabdil
                UnitTypes unitDetail = EnumExtensions.ParseEnum<UnitTypes>(request.Symbols.ToLowerInvariant());
                Unit _setUnit = new Unit(request.Value, unitDetail);
                result.Add(UnitTypes.mm.ToString(), _setUnit.To(UnitTypes.mm));// tabdil be milimeter
                result.Add(UnitTypes.cm.ToString(), _setUnit.To(UnitTypes.cm));// tabdil be inch
                result.Add(UnitTypes.km.ToString(), _setUnit.To(UnitTypes.km));// tabdil be kilometer
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return result;
        }

        /// <summary>
        /// calculate value in formula
        /// </summary>
        /// <param name="request"></param>
        /// <returns>return value after calculate</returns>
        public FormulasUnitResponse UnitWithFormula(FormulasUnitDTO request)
        {
            var result = new FormulasUnitResponse();
            try
            {
                string formula = request.Formula.Replace("×", "*").Replace("−", "-").Replace("a", request.Value.ToString());
                var responseService = _unitFurmola.Eval(formula);
                return new FormulasUnitResponse()
                {
                    Value = responseService,
                    Formula = formula
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        #endregion [Methods]
    }
}
