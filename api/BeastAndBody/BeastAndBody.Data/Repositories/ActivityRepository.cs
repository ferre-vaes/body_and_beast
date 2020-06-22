using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BeastAndBody.Data.Models;
using BeastAndBody.Data.Repositories.Interfaces;

namespace BeastAndBody.Data.Repositories
{
    public class ActivityRepository: IActivityRepository
    {
        private readonly BeastAndBodyContext _context;

        public ActivityRepository(BeastAndBodyContext context)
        {
            this._context = context;
        }

        public Activity GetActivity(int id)
        {
            return _context.Activities.Find(id);
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public Activity Add(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
            return activity;
        }

        public Activity Update(Activity activity)
        {
            _context.Activities.Attach(activity);
            _context.SaveChanges();
            return activity;
        }

        public Activity Delete(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                _context.SaveChanges();
            }

            return activity;
        }
    }
}
