using CommonLibrary.Protos;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{limit}/{offset}")]
        public async Task<AccountsListRsponse> GetAccountsList(int limit, int offset)
        {
            var response = await userClient.GetAccountsListAsync(new GetAccountsListRequest
            {
                Limit = limit,
                Offset = offset
            });
            return response;
        }
    }
}
