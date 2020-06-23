using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeastAndBody.Data.Models;
using BeastAndBody.Data.Models.Enums;
using BeastAndBody.Data.Repositories.Interfaces;

namespace BeastAndBody.Data.Repositories
{
    public class FollowRepository: IFollowRepository
    {
        private readonly BeastAndBodyContext _context;

        public FollowRepository(BeastAndBodyContext context)
        {
            _context = context;
        }

        public ICollection<Follow> FindByActivityId(int activityId)
        {
            return _context.Follows.Where(f => f.ActivityId == activityId).ToList();
        }

        public void Follow(int activityId, FollowType type)
        {
            
        }
    }
}
