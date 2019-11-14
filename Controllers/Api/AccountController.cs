using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PDV.API.Controllers.Dtos;
using PDV.API.Data.Entities;
using PDV.API.Data.Repositories;
using PDV.API.Infrastructure.Authentication;
using PDV.API.Infrastructure.ManagedResponses;

namespace MadBugAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {   
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly IMapper _mapper;
        private readonly AccountStoreRepository _accountStoresRepository;
          public AccountController(UserManager<Account> userManager,SignInManager<Account> signInManager, IConfiguration configuration, JwtSettings jwtSettings,
              RoleManager<AppRole> roleManager, IMapper mapper, AccountStoreRepository accountStoresRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _roleManager = roleManager;
            _mapper = mapper;
            _accountStoresRepository = accountStoresRepository;
        }
       
        [HttpPost("token")]
        [ProducesResponseType(200, Type = typeof(AuthenticatedUser))]
        [ProducesResponseType(400, Type = typeof(ManagedErrorResponse))]
        public async Task<IActionResult> Login(LoginDto model){
            SecurityManager mgr = new SecurityManager(_jwtSettings, _userManager, _roleManager,_accountStoresRepository);
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {   
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName.Trim() == model.UserName.Trim());
                var authUser = await mgr.BuildAuthenticatedUserObject(appUser);
                return Ok(authUser);
            }
            else
            {
                return BadRequest(new ManagedErrorResponse(ManagedErrorCode.Validation,"La combinación usuario/password no es correcta"));
            }  
        }
    }
}