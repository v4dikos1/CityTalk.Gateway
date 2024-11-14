using CommonLibrary.Protos;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(User.UserClient userClient) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<AccountResponse> GetAccount([FromRoute] string id)
        {
            var response = await userClient.GetAccountAsync(new GetAccountRequest { ExternalUserId = id });
            return response;
        }
    }
}
