using Zino.Domain.SeedWork;

namespace Zino.Domain.Model
{
    public class UnitOfMeasurement : Entity<int>, IAggregateRoot
    {
        public string PerisanName { get; set; }
        public string EnglishName { get; set; }
        public string Symbols { get; set; }
        public string Dimension { get; set; }
    }
}
