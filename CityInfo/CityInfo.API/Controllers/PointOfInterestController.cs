using Microsoft.AspNetCore.JsonPatch;
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
    [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
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

    [HttpPost("{cityId}/pointsofinterest")]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestForCreationDto pointOfInterest)
    {
      if (pointOfInterest == null)
        return NotFound();

      if (pointOfInterest.Description == pointOfInterest.Name)
        ModelState.AddModelError("Description", "The provided description should be different from the name.");

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var city = CitiesDataStore.Current.GetCity(cityId);
      if (city == null)
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

    [HttpPut("{cityId}/pointsofinterest/{id}")]
    public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody]PointOfInterestForUpdateDto pointOfInterest)
    {
      if (pointOfInterest == null)
        return NotFound();

      if (pointOfInterest.Description == pointOfInterest.Name)
        ModelState.AddModelError("Description", "The provided description should be different from the name.");

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var city = CitiesDataStore.Current.GetCity(cityId);
      if (city == null)
        return NotFound();

      var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
      if (point == null)
        return NotFound();

      point.Name = pointOfInterest.Name;
      point.Description = pointOfInterest.Description;

      return NoContent(); // as the consumer already has all the information
    }

    [HttpPatch("{cityId}/pointsofinterest/{id}")]
    public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody]JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
    {
      if (patchDoc == null)
        return NotFound();

      var city = CitiesDataStore.Current.GetCity(cityId);
      if (city == null)
        return NotFound();

      var point = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
      if (point == null)
        return NotFound();

      var pointToPatch = new PointOfInterestForUpdateDto()
      {
        Name = point.Name,
        Description = point.Description
      };

      patchDoc.ApplyTo(pointToPatch, ModelState);

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      if (pointToPatch.Description == pointToPatch.Name)
        ModelState.AddModelError("Description", "The provided description should be different from the name.");

      TryValidateModel(pointToPatch);
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      point.Name = pointToPatch.Name;
      point.Description = pointToPatch.Description;

      return NoContent(); // as the consumer already has all the information
    }
  }
}
