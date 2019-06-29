using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  //[Route("api/[controller]")]
  [Route("api/cities")]
  public class CitiesController : Controller
  {
    [HttpGet()]
    public IActionResult GetCities()
    {
      return Ok(CitiesDataStore.Current.Cities);
    }
    [HttpGet("{id}")]
    public IActionResult GetCity(int id)
    {
      var city = CitiesDataStore.Current.GetCity(id);
      if (city == null)
        return NotFound();
      return Ok(city);
    }
  }
}
