using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Nop.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return Json(new { });
        }

        static string grantType = "authorization_code";
        static string clientId = "bcdc04fb-blahblah";
        static string clientSecret = "2f55d16c-blahblabetyblah";
        static string scope = "scope.fullaccess";
        static string redirectUrl = "http://localhost/token";
        static Uri authorizationServerTokenIssuerUri;
       
        public async Task<IActionResult> AuthTest()
        {
            //authorization server parameters owned from the client
            //this values are issued from the authorization server to the client through a separate process (registration, etc...)

            authorizationServerTokenIssuerUri = new Uri("http://localhost/oauth/authorize");

            //access token request
            string rawToken = RequestTokenToAuthorizationServer(
                 authorizationServerTokenIssuerUri,
                 clientId,
                 scope,
                 clientSecret)
                .GetAwaiter()
                .GetResult();
            var x = rawToken;
            //...some more code
            return Json(new { result = x });
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

    }
}