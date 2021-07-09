using System;
using System.Collections.Generic;
using System.Text;
using Zino.Service.DTOs;
using Zino.Service.Enums;

namespace Zino.Service.Services
{
    public interface IServiceConvert
    {
        Dictionary<string, Unit> CoefficientUnit(CoefficientUnitDTO request);
        FormulasUnitResponse FormulasUnit(FormulasUnitDTO request);
    }
}
