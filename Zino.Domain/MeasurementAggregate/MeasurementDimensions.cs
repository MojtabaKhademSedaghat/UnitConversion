using System.Collections.Generic;
using Zino.Domain.SeedWork;

namespace Zino.Domain.Model
{
    public class MeasurementDimensions : ValueObject
    {
        public string PersianName { get; set; }
        public string EnglishName { get; set; }

        public MeasurementDimensions() { }

        public MeasurementDimensions(string persianName, string englishName)
        {
            PersianName = persianName;
            EnglishName = englishName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PersianName;
            yield return EnglishName;
        }
    }
}
