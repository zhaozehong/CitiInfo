using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  [Route("api/cities")]
  public class PointOfInterestController : Controller
  {
    [HttpGet("{cityId}/pointsofinterest")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      var city = CitiesDataStore.Current.GetCity(cityId);
      if (city == null)
        return NotFound();
      return Ok(city.PointsOfInterest);
    }
    [HttpGet("{cityId}/pointsofinterest/{id}")]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
      var city = CitiesDataStore.Current.GetCity(cityId);
      if (city == null)
        return NotFound();

      var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
      if (point == null)
        return NotFound();

      return Ok(point);
    }
  }
}
