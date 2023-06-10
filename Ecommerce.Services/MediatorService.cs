using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecommerce.Models;
using Ecommerce.Repositry;
using EcommerceViewModel.Midetor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class MediatorService:IMediatorServices
    {
        private readonly IgenricRepo<Mediator,string> mediator;
        UnitOfWork UnitOfWork;
        IMapper mapper;
        public MediatorService( IgenricRepo<Mediator,string> _mediator, UnitOfWork _UnitOfWork, IMapper _mapper) 
        {
        mediator = _mediator;
        UnitOfWork = _UnitOfWork;
        mapper = _mapper;
        }

       public IEnumerable<MeditorDto>  GetAll()
        {
            var mediators =mediator.GetAll();
             return  mediators.ProjectTo<MeditorDto>(mapper.ConfigurationProvider);

        }
       public IEnumerable<MeditorDto> Get(Expression<Func<Mediator, bool>> expression)
        {
            var mediators= mediator.Get(expression);
            return mediators.ProjectTo<MeditorDto>(mapper.ConfigurationProvider);

        }
        Mediator GetById(string id)
        {
           return mediator.GetById(id);
        }

        public Mediator Add(Mediator item)
        {
            mediator.Add(item);
            UnitOfWork.SaveChange();
            return item;
        }
        public void UpdateAllProperty(Mediator item)
        {
            mediator.UpdateAllProperty(item);
            UnitOfWork.SaveChange();
        }
        public void Update(Mediator item, string[] properties)
        {
            mediator.Update(item, properties);

            UnitOfWork.SaveChange();
        }
       public void Delete(string id)
        {
            mediator.Delete(id);
            UnitOfWork.SaveChange();
        }
    }
}
