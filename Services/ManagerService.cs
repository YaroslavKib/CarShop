using System;
using CarShop.Models;
using CarShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class ManagerService
    {
        private ApplicationDbContext _context;

        public ManagerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Manager GetManager(int id)
        {
            var man = _context.Managers.Find(id);

            if (man == null)
                return null;

            return man;
        }

        public bool IsUserIdManager(string userId)
        {
            return _context.Managers.Count(x => x.UserId == userId) >= 1;
        }

        public Manager Create(Manager manager)
        {
            if (IsUserIdManager(manager.UserId))
                throw new Exception("User is already manager");
            
            _context.Managers.Add(manager);
            _context.SaveChanges();

            return manager;
        }

        public Manager Delete(Manager manager)
        {
            if (manager == null)
                return null;

            _context.Managers.Remove(manager);
            _context.SaveChanges();

            return manager;
        }

        public Report GenerateReportForManager(Manager manager)
        {
            // collecting main information
            var managerDeals = _context.Deals.Where(x => x.ManagerId == manager.Id).ToList();
            var dealsCount = managerDeals.Count();
            var totalMoneyFromDeals = managerDeals
                                      .Select(x => x.Price)
                                      .Aggregate((a, b) => a + b);

            // counting brand information
            var averageMoney = (totalMoneyFromDeals / dealsCount) * 25 / 100;
            var vehicle = _context.Vehicles
                .Select(x => new { Vehicle = x, Distance = Math.Abs(x.Price - averageMoney) })
                .OrderBy(x => x.Distance)
                .First()
                .Vehicle;
            
            // load vehicle's model
            _context.Entry(vehicle).Reference(x => x.Model).Load();

            // load model's brand
            _context.Entry(vehicle.Model).Reference(x => x.Brand).Load();

            Brand recommendedBrand = vehicle.Model.Brand;

            return new Report(
                manager,
                totalMoneyFromDeals,
                dealsCount,
                recommendedBrand);
        }

        public DateTimeRangeReport FindBestManager(DateTimeRangeReport range)
        {
            var groupedDeals = _context.Deals
                .Where(x => x.Date >= range.Start && x.Date < range.End)
                .GroupBy(x => x.ManagerId)
                .Select(g => new { ManagerId = g.Key, Count = g.Count() })
                .OrderByDescending(c => c.Count);

            var bestDeal = groupedDeals.First();
            var bestManagerId = bestDeal.ManagerId;
            var bestManager = GetManager(bestManagerId);

            if (bestManager == null)
                return null;

            range.DealsCount = bestDeal.Count;
            range.Manager = bestManager;
            
            return range;
        }

        public IEnumerable<Deal> GetManagerDeals(Manager manager)
        {
            return _context.Deals.Where(x => x.ManagerId == manager.Id);
        }
    }
}
