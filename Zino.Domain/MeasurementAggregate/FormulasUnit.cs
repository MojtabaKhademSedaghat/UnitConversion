using System;
using System.Collections.Generic;
using System.Text;
using Zino.Domain.SeedWork;

namespace Zino.Domain.Model
{
    public class FormulasUnit : Entity<int>
    {
        public string PerisanName { get; set; }
        public string EnglishName { get; set; }
        public string Symbols { get; set; }
        public string FormulaFromUnit { get; set; }
        public string FormulaToUnit { get; set; }
    }
}
