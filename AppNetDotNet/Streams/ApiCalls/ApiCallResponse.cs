using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AppNetDotNet.Streams.ApiCalls
{
    public class ApiCallResponse
    {
        public bool success { get; set; }
        public string content { get; set; }
        public HttpStatusCode statusCode { get; set; }
        public string errorMessage { get; set; }
        public string errorDescription { get; set; }
        Helper.Response.RateLimits rateLimits { get; set; }
        public Streams.Model.Meta meta { get; set; }

        public ApiCallResponse()
        {
            success = false;
            content = "";
            errorMessage = "";
            errorDescription = "";
            statusCode = HttpStatusCode.OK;
            rateLimits = new Helper.Response.RateLimits();
        }

        public override string ToString()
        {
            return "Success: " + success.ToString();
        }

        public ApiCallResponse(Helper.Response response)
        {
            if (response != null)
            {
                this.success = response.Success;
                this.errorMessage = response.Error;
                this.content = response.Content;
                this.statusCode = response.StatusCode;
                this.rateLimits = response.rateLimits;
            }
        }
    }
}
