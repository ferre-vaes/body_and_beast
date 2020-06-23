using System;
using System.Collections.Generic;
using System.Text;
using BeastAndBody.Data.Models;
using BeastAndBody.Data.Models.Enums;

namespace BeastAndBody.Data.Repositories.Interfaces
{
    public interface IFollowRepository
    {
        ICollection<Follow> FindByActivityId(int activityId);
        void Follow(int activityId, FollowType type);
    }
}
