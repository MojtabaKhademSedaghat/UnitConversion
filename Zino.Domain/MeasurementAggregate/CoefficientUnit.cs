using Zino.Domain.SeedWork;

namespace Zino.Domain.Model
{
    public class CoefficientUnit : Entity<int>
    {
        public string PerisanName { get; set; }
        public string EnglishName { get; set; }
        public string Symbols { get; set; }
        public string RelativeToTheBaseUnit { get; set; }
    }
}
