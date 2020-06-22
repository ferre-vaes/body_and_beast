using System;
using System.Collections.Generic;
using System.Text;
using BeastAndBody.Data.Models;

namespace BeastAndBody.Data.Repositories.Interfaces
{
    public interface IActivityRepository
    {
        Activity GetActivity(int id);
        IEnumerable<Activity> GetAllActivities();
        Activity Add(Activity activity);
        Activity Update(Activity activity);
        Activity Delete(int id);
    }
}
