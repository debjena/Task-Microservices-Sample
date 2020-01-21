using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Task_Auth.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet, AllowAnonymous]
        [Route("api/identity/token")]
        public async Task<ActionResult> GetIdToken(string clientid= "m2m.short", string clientsecret="secret",string granttype= "client_credentials", string scope="api")
        {
            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(_config["id4"]);
            if (disco.IsError)
            {
                return new BadRequestObjectResult("error");
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientid,
                ClientSecret = clientsecret,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                return new BadRequestObjectResult("error");
            }
            return Ok(tokenResponse.AccessToken);
        }
        //[HttpGet, AllowAnonymous]
        //[Route("api/cognito/token")]
        //public async Task<ActionResult<string>> GetToken(string username = "dxc", string password="beta")
        //{
        //    var cognito = new AmazonCognitoIdentityProviderClient(Amazon.RegionEndpoint.USEast1);
        //    var request = new InitiateAuthRequest
        //    {
        //        ClientId = _config["AWS:UserPoolClientId"],
        //        AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
        //    };
        //    request.AuthParameters.Add("USERNAME", username);
        //    request.AuthParameters.Add("PASSWORD",password); 
        //    var response = await cognito.InitiateAuthAsync(request);
        //    return Ok(response.AuthenticationResult.IdToken);


            
        //}
    }
}