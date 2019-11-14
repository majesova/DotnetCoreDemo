using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.API.Controllers.Dto;
using PDV.API.Data;
using PDV.API.Data.Entities;
using PDV.API.Data.Repositories;
using PDV.API.Infrastructure.Helpers;
using PDV.API.Infrastructure.ManagedResponses;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PDV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly PDVContext _context;
        public ProductController(IMapper mapper, ProductRepository productRepository, PDVContext context)
        {
            _mapper = mapper;
            _context = context;
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(200,Type= typeof(RegisterProductResponseDto))]
        [ProducesResponseType(400, Type = typeof(ManagedErrorResponse))]
        public async Task<IActionResult> Post([FromBody] RegisterProductRequestDto model) {
            
            if (!ModelState.IsValid)
            {
                return BadRequest (new ManagedErrorResponse(ManagedErrorCode.Validation, "Hay errores de validación", ModelState));
            }
            try
            {
                var product = _mapper.Map<Product>(model);
                //StoreId assignment
                var storeId = AccountHelper.CurrentStoreId(User as ClaimsPrincipal);
                product.StoreId = Guid.Parse(storeId);
                _productRepository.Insert(product);
                await _context.SaveChangesAsync();
                var response = _mapper.Map<RegisterProductResponseDto>(product);
                return Ok(response);
            }
            catch (Exception ex) {
                return BadRequest(new ManagedErrorResponse(ManagedErrorCode.Exception, ex.Message, ex));
            }
            

        }
    }
}
