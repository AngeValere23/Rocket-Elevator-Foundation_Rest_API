using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ReStatus.Models;


namespace ReStatusApi.Controllers
{
    [Route ("api/buildings")]
    [ApiController]
    public class buildingsController : ControllerBase
    {
        private readonly elevatorsContext _context;

        public buildingsController(elevatorsContext context)
        {
            _context = context;
        }
        
        // GET: api/buildings
        // Retrieving a list of Buildings requiring intervention 
        [HttpGet]
        public ActionResult<List<buildings>> GetToFixBuildings()
        {
            IQueryable<buildings> ToFixBuildingsList = from ObjectStatus in _context.buildings
            join batteries in _context.batteries on ObjectStatus.id equals batteries.building_id
            join columns in _context.columns on batteries.id equals columns.battery_id
            join elevators in _context.elevators on columns.id equals elevators.column_id
            where (batteries.Status == "Intervention") || (columns.Status == "Intervention") || (elevators.Status == "Intervention")
            select ObjectStatus;
            return ToFixBuildingsList.Distinct().ToList();
        }
    }
}