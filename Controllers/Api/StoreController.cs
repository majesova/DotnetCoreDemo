using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PDV.API.Controllers.Dto;
using PDV.API.Data;
using PDV.API.Data.Entities;
using PDV.API.Data.Repositories;
using PDV.API.Infrastructure.Authentication;
using PDV.API.Infrastructure.ManagedResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(401, Type = typeof(string))]
    public class StoreController : ControllerBase
    {
        private readonly SignInManager<Account> _signInManager;
        private readonly UserManager<Account> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;
        private JwtSettings _jwtSettings;
        private IMapper _mapper;
        StoreRepository _storeRepository;
        private PDVContext _context;
        private AccountStoreRepository _accountStoreRepository;
        public StoreController(UserManager<Account> userManager, SignInManager<Account> signInManager, 
            IConfiguration configuration, JwtSettings jwtSettings, RoleManager<AppRole> roleManager, 
            IMapper mapper, StoreRepository storeRepository, PDVContext context, AccountStoreRepository accountStoreRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _roleManager = roleManager;
            _mapper = mapper;
            _storeRepository = storeRepository;
            _context = context;
            _accountStoreRepository = accountStoreRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RegisterStoreRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ManagedErrorResponse(ManagedErrorCode.Validation, "Hay errores de validación", ModelState));
            }

            //Register User data, new user
            var user = new Account { UserName = model.UserName.Trim(), Email = model.Email.Trim() };
            var result = await _userManager.CreateAsync(user, model.Password.Trim());
            //Register Store data
            if (result.Succeeded)
            {
                //Store
                var store = new Store();
                store.Name = model.StoreName;
                store.Address = model.StoreAddress;
                _storeRepository.Insert(store);
                //Relationship
                var accountStore = new AccountStore();
                accountStore.AccountId = user.Id;
                accountStore.StoreId = store.Id;
                _accountStoreRepository.Insert(accountStore);
                await _context.SaveChangesAsync();
                //Realiza el login y devuelve ya el token
                SecurityManager mgr = new SecurityManager(_jwtSettings, _userManager, _roleManager, _accountStoreRepository);
                var authUser = await mgr.BuildAuthenticatedUserObject(user);
                return Ok(authUser);
            }
            else
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return BadRequest(new ManagedErrorResponse(ManagedErrorCode.Validation, "Identity validation errors", errors));
            }
        }
    }
}
