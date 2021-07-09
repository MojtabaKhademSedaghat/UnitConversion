using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zino.Service.Enums;
using Zino.Service.Services;
using Microsoft.Extensions.Logging;
namespace Zino.Tests.ConvertUnits
{
    [TestClass]
    public class ServiceTest
    {

        IServiceConvert _serviceConvert;
        [TestInitialize]
        public void GlobalPrepare()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("SampleApp.Program", LogLevel.Debug)
                       .AddConsole();
            });
            _serviceConvert = new ServiceConvert(loggerFactory, new UnitFurmola()); ;
        }
        /// <summary>
        /// test method UnitWithFormula , 
        /// </summary>
        [TestMethod]
        public void Should_Convert_Unit_With_Formula()
        {

            var response = _serviceConvert.UnitWithFormula(new Service.DTOs.FormulasUnitDTO() { Value = 1, Formula = "(a × 9/5) + 32" });
            Assert.IsNotNull(response);
            Assert.AreEqual(33.8, response.Value);
        }

        [TestMethod]
        public void Shold_Convert_Unit() 
        {
            var response = _serviceConvert.CoefficientUnit(new Service.DTOs.CoefficientUnitDTO() { Symbols = "m" , Value = 5 });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Values.Count > 0, "there is no result!");
        }
    }
}
