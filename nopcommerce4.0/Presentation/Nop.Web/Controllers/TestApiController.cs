using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Nop.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/TestApi")]
    public class TestApiController : Controller
    {
        static string grantType = "authorization_code";
        static string clientId = "bcdc04fb-blah blah";
        static string clientSecret = "2f55d16c-blah blabety blah";
        static string scope = "scope.fullaccess";
        static string redirectUrl = "http://localhost/token";
        static Uri authorizationServerTokenIssuerUri;
        [HttpGet("AuthTest")]
        public async Task<IActionResult> AuthTest()
        {
            //authorization server parameters owned from the client
            //this values are issued from the authorization server to the client through a separate process (registration, etc...)

            authorizationServerTokenIssuerUri = new Uri("http://localhost/oauth/authorize");

            //access token request
            string rawToken =  RequestTokenToAuthorizationServer(
                 authorizationServerTokenIssuerUri,
                 clientId,
                 scope,
                 clientSecret)
                .GetAwaiter()
                .GetResult();
            var x = rawToken;
            //...some more code
            return  Json(new { result = x });
    }

        private static async Task<string> RequestTokenToAuthorizationServer(Uri uriAuthorizationServer, string clientId, string scope, string clientSecret)
        {
            HttpResponseMessage responseMessage;
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, uriAuthorizationServer);

                string queryParameters = string.Format("client_id={0}&client_secret={1}&code={2}&grant_type={3}&redirect_uri={4}", clientId, clientSecret, "", grantType, redirectUrl);

                HttpContent httpContent = new FormUrlEncodedContent(
                    new[]
                    {
                    new KeyValuePair<string, string>("grant_type", grantType),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("scope", scope),
                    new KeyValuePair<string, string>("code", ""),
                    new KeyValuePair<string, string>("redirect_uri", redirectUrl),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>(" response_type", "code")
                     });
                tokenRequest.Content = httpContent;
                responseMessage = await client.SendAsync(tokenRequest);
            }
            return await responseMessage.Content.ReadAsStringAsync();
        }


        public class UserAccessModel
        {
            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string ServerUrl { get; set; }

            public string RedirectUrl { get; set; }
        }















        // GET: api/TestApi
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TestApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/TestApi
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/TestApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
