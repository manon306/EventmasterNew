

namespace WebApi.net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM dto)
        {
            var result = await accountService.Register(dto);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM dto)
        {
            var result = await accountService.Login(dto);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(new
            {
                token = result.token,
                expiration = result.expiration
            });
        }
    }
    // token : eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRiMzRkYWQ4LTZhOWMtNDBjYy1iYTkyLTExNTAzNzg4ZjllNiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJNSCIsImp0aSI6ImExNThhMDk0LWUxY2UtNGJmNi1iNGExLWE2NDk1MjUzNTdkYyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlJlZ2lzdGVyZWQiLCJleHAiOjE3NjkyNjkzMjUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzI2MSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NzI2MSJ9.b_S2JZitzFTnutn2dzWbn3F3uhu8_w9xM2sefKB45CU

}
