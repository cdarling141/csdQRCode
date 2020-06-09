using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing;
using Microsoft.Net.Http.Headers;
using System.Net.Http;

namespace csdQRCode
{
    public static class QRCodeit
    {
        [FunctionName("QRCodeit")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Validation URL (URL that will return human friendly image of Pass / Fail
            string validationUrl = System.Environment.GetEnvironmentVariable("QRCodeValidationURL");
            string validationcode = System.Environment.GetEnvironmentVariable("QRCodeValidationKEY");

            //Get the user parameter off the request url if present
            string user = req.Query["user"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            user = user ?? data?.user;

            //Write to a backend store the userId, time of registration, and registration guid for validation later
            string tempRegGuid = Guid.NewGuid().ToString();

            /*
             *  Put your code here to write to a backend store for the covidtoken function to validate
             *
             */


            //Validate user exists
            if (user != null)
            {
                
                log.LogInformation("Processed request for " + user + ". Creating qrcode for guid " + tempRegGuid);
                
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //Put the URL and guid in the qrcodedata
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{validationUrl}/?code={validationcode}&token={tempRegGuid}", QRCodeGenerator.ECCLevel.Q);

                /* Base 64 - Optional alternate way to output the QRCode in a web friendly manner
                var imgType = Base64QRCode.ImageType.Jpeg;
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                string qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
                var htmlPictureTag = $"<img alt=\"Embedded QR Code\" src=\"data:image/{imgType.ToString().ToLower()};base64,{qrCodeImageAsBase64}\" />";
                OkObjectResult result = new OkObjectResult(htmlPictureTag);
                 */
                
                // return as png - Return the png with the correct media type
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);
                FileContentResult fileContentResult = new FileContentResult(qrCodeAsPngByteArr, "image/png");
                return (ActionResult)fileContentResult;
            }
            else
            {
                return new BadRequestObjectResult("User invalid or missing");
            }
        }
    }

}
