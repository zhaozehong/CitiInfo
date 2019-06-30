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
    [HttpGet("{cityId}/pointsofinterest/{id}", Name="GetPointOfInterest")]
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

    [HttpPost("{cityId}/pointofinterest")]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestForCreationDto pointOfInterest)
    {
      if (pointOfInterest == null)
        return NotFound();

      if (pointOfInterest.Description == pointOfInterest.Name)
        ModelState.AddModelError("Description", "The provided description should be different from the name.");

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var city = CitiesDataStore.Current.GetCity(cityId);
      if(city == null)
        return NotFound();

      var finalPointOfInterest = new PointOfInterestDto()
      {
        Id = city.NumberOfPointsInterest + 1,
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description
      };

      city.PointsOfInterest.Add(finalPointOfInterest);

      return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
    }


  }
}
