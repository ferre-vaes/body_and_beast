using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BeastAndBody.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Activity = BeastAndBody.Data.Models.Activity;

namespace BeastAndBody.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Activity model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_activityRepository.Add(model));
        }

        [HttpGet]
        public IActionResult GetAllActivities() => Ok(_activityRepository.GetAllActivities());

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var activity = _activityRepository.Delete(id);

            if (activity != null)
            {
                return Ok(activity);
            }

            return BadRequest("Couldn't find the activity you were looking for.");
        }
    }
}
