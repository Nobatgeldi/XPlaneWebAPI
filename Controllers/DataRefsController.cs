using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using XPlaneConnector;
using XPlaneConnector.DataRefs;

namespace XPlaneDotNetCoreWebAPI.Controllers
{
    

    [Route("api/[controller]/")]
    [ApiController]
    public class DataRefsController : ControllerBase
    {
        IActionResult MyErrorStatusCode(ApiProgress value)
        {
            return StatusCode(255, value);
        }

        [Route("[action]/{search?}")]
        [HttpPost]
        public IActionResult FindDataref(string search)
        {
            if (search.Length < 3) return MyErrorStatusCode(ApiProgress.Error("Min search character count is 4!!!",true));

            DataRefs dataRefs = new DataRefs(); 
            var properties = typeof(DataRefs).GetProperties().ToList();

            List<DataRefElement> dataRefElements = new List<DataRefElement>();

            IEnumerable<PropertyInfo> datarefs = properties.Where(property => property.PropertyType == typeof(DataRefElement));

            foreach (var dataref in datarefs)
                dataRefElements.Add(dataref.GetValue(dataRefs) as DataRefElement);
            var searchResault = dataRefElements.Where(x => x.DataRef.Contains(search));

            if(searchResault.Count()>200) return MyErrorStatusCode(ApiProgress.Error("Result count bigger than 200!!! Try with  more specific string. Result count:"+ searchResault.Count(), true));
            
            else if (searchResault.Count() ==0) return MyErrorStatusCode(ApiProgress.Error("Not found!!!", false));
            
            return Ok(dataRefElements.Where(x => x.DataRef.Contains(search)));
        }
    }
}
