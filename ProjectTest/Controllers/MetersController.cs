using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetersController : ControllerBase
    {

        [HttpGet("{serialNumber}/{dateTime}")]
        public string GetData(string serialNumber, string dateTime)
        {
             
            try
            {
               var date = Convert.ToDateTime(dateTime);
                Console.WriteLine(date);
                return JsonConvert.SerializeObject(XMLfile.GetData(Convert.ToInt32(serialNumber), date));
            }
            catch 
            {
                return Get();
            }
            
        }
        
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(XMLfile.GetSerialNumberAll());

        }
        
    }
}
