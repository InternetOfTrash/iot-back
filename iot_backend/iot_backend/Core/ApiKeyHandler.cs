using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iot_backend
{
    public class APIKeyHandler : DelegatingHandler
    {
        private static readonly string API_KEY = ConfigurationManager.AppSettings["Api_key"];

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var isValidApiKey = false;
            //Validate that the api key exists

            var foundKey =
                request.Headers.Where(h => h.Key == "API_KEY").Select(x => x.Value.FirstOrDefault()).FirstOrDefault();
            if (API_KEY.Equals(foundKey) || request.Headers.Host.Contains("localhost"))
                isValidApiKey = true;

            //If the key is not valid, return an http status code.
            if (!isValidApiKey)

                return request.CreateResponse(HttpStatusCode.Forbidden, "Bad API Key");

            //Allow the request to process further down the pipeline
            var response = await base.SendAsync(request, cancellationToken);
            
            //Return the response back up the chain
            return response;
        }
    }
}
