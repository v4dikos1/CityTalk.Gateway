using CommonLibrary.Protos;
using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;

namespace Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        private readonly User.UserClient _client;
        public UserController()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7121");
            _client = new User.UserClient(_channel);
        }

        [HttpGet("{id}")]
        public async Task<UserProfile> GetUserProfileAsync([FromRoute] string id)
        {
            try
            {
                var response = await _client.GetUserProfileAsync(new GetUserProfileRequest { Id = id });
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
