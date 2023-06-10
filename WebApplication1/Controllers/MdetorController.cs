using Ecommerce.Repositry;
using Ecommerce.Services;
using EcommerceViewModel.Midetor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MdetorController : ControllerBase
    {
        UnitOfWork UnitOfWork;
        MediatorService mediatorServices;
        public MdetorController(UnitOfWork unitOfWork, MediatorService mediatorServices) 
        {
            this.mediatorServices=mediatorServices;
            this.mediatorServices = mediatorServices;

        }

        [HttpGet]
        public List<MeditorDto> get()
        {
            return mediatorServices.GetAll().ToList();
        }
       











    }
}
