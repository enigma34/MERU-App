using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Meru_Web.Filters;
using Meru_Web.Models;

namespace Meru_Web.Controllers
{
    [APIAuthorize]
    public class FhirController : ApiController
    {
        [HttpGet]
        public IHttpActionResult AccessFHIR()
        {
            try
            {
               
                FHIRModel fhir = new FHIRModel
                {
                   fhirEndpoint  = "http://fhirtest.uhn.ca/baseDstu2",
                   currentTimeServer   = DateTime.Now
                };
                return Ok<FHIRModel>(fhir);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return BadRequest(e.InnerException.Message+"\n"+e.Message);
            }
        }
    }
}
