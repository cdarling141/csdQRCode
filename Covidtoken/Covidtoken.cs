using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Covidtoken
{
    public static class Covidtoken
    {
        [FunctionName("Covidtoken")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string token = req.Query["token"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            token = token ?? data?.token;

            if (string.IsNullOrEmpty(token))
            {
                return new BadRequestObjectResult("Token invalid or missing");
            }


            bool pass = false;

            //For demonstration randomly deciding if token is valid or not
            Random rand = new Random();
            int randomresult = rand.Next(1, 10);

            if (randomresult > 5)
            {
                pass = true;
            }
            // ---
            
            /*
             * Replace demonstration code above with code to validate the token attributes (token creation time, user, etc..) 
             * Set the pass / fail flag based on the validation
             */
            
            if (pass)
            {
                return new OkObjectResult("Pass");
            }
            else
            {
                return new OkObjectResult("Fail");
            }
        }
    }
}
