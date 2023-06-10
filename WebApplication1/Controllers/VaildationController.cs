using AutoMapper;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Repositry;
using Ecommerce.Services;
using EcommerceViewModel;
using EcommerceViewModel.Midetor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaildationController : ControllerBase
    {
        private readonly UserManager<Applicationuser> userManager;
        private readonly IConfiguration config;
         MediatorService mediatorService;
        private readonly UnitOfWork unitOfWork;
        IMapper _mapper;
        public VaildationController(UserManager<Applicationuser> userManager,
            IConfiguration config, UnitOfWork unitOfWork,MediatorService mediatorService,IMapper mapper)
        {
           this.userManager = userManager;
            this.config = config;
            this.unitOfWork = unitOfWork;
            this.mediatorService = mediatorService;
            this._mapper = mapper;
            
        }

        [HttpPost("registermidetor")]
        public async Task<ActionResult<Result>> RegisterAsync([FromForm] MeditorDto meditor)

        {

            if (ModelState.IsValid)
            {
               Applicationuser midet = new Applicationuser();

                //midet.Email = meditor.Email;
                //midet.UserName = meditor.UserName;
                //midet.FirstName = meditor.FirstName;
                //midet.LastName = meditor.LastName;
                //midet.Adreess = meditor.Adreess;
                 midet = _mapper.Map<Applicationuser>(meditor);
                IdentityResult result = await userManager.CreateAsync(midet, meditor.password);
            Result result1 = new Result();

              if (result.Succeeded)
                {
                   Mediator mid = new Mediator();

                      mid.Id = midet.Id;
                   
                    mediatorService.Add(mid);
                    unitOfWork.CommitTransaction();
                    await userManager.AddToRoleAsync(midet, "Midetor");
                    
                    result1.Message = "sucess";
                    result1.IsPass = true;
                    
                    return Ok(result1);
                }
                else
                {
                    result1.Message = "the register failed";
                    result1.IsPass = false;
                    return BadRequest(result1);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result>> LoginAsync(LoginDTO loginDTO)
        {

            if (ModelState.IsValid)
            {
               Applicationuser Usr = await userManager.FindByNameAsync(loginDTO.username);
                if (Usr != null && await userManager.CheckPasswordAsync(Usr, loginDTO.password))
              {


                    List<Claim> myClaims = new List<Claim>();

                    myClaims.Add(new Claim(ClaimTypes.NameIdentifier, Usr.Id));
                    myClaims.Add(new Claim(ClaimTypes.Name, Usr.UserName));
                    myClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    List<string> Roles = (List<string>)await userManager.GetRolesAsync(Usr);
                    if(Roles!=null)
                    {
                        foreach(string role in Roles) 
                        {
                            myClaims.Add(new Claim(ClaimTypes.Role,role));
                        }
                    }
                    var authSecritKey =
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecuriytKey"]));

                    SigningCredentials credentials =
                        new SigningCredentials(authSecritKey, SecurityAlgorithms.HmacSha256);



                    JwtSecurityToken jtw = new JwtSecurityToken
                        (
                            issuer: "ValidIss",
                            audience: "ValidAud",
                            expires: DateTime.Now.AddHours(0.5),
                            claims: myClaims,
                            signingCredentials: credentials
                        );
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jtw),
                        expiration = jtw.ValidTo,
                        message = "sucesss"
                        
                    }); ;

                }
                Result result = new Result();
                result.Message = "failed";
                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }
    }
}
