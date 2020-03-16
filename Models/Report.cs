using CarShop.Models;

namespace CarShop
{
    public class Report
    {
        public Manager Manager { get; private set; }
        public int TotalMoneyFromDeals { get; private set; }
        public int DealsCount { get; private set; }
        public Brand RecommendedBrand { get; private set; }

        public Report(Manager manager, int totalMoneyFromDeals, int dealsCount, Brand recommendedBrand)
        {
            Manager = manager;
            TotalMoneyFromDeals = totalMoneyFromDeals;
            DealsCount = dealsCount;
            RecommendedBrand = recommendedBrand;
        }
    }
}
