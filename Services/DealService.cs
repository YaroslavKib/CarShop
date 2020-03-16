using System;
using CarShop.Models;
using CarShop.Data;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class DealService
    {
        private ApplicationDbContext _context;

        public DealService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Deal Create(Deal deal)
        {
            _context.Deals.Add(deal);
            _context.SaveChanges();

            return deal;
        }

        public Deal GetDeal(int id)
        {
            var deal = _context.Deals.Find(id);

            if (deal == null)
                return null;

            return deal;
        }

        public IEnumerable<Deal> GetDealForManager(int managerId)
        {
            var deals = _context.Deals.Where(x => x.ManagerId == managerId);
            
            return deals;
        }

        public IEnumerable<Deal> GetDealForManager(Manager manager)
            => GetDealForManager(manager.Id);
    }
}
