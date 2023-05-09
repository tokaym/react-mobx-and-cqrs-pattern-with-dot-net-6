using Application.Services.IRomaniaZm20Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RomaniaZm20s.Commands;

public class CreateRomaniaZm20Command : IRequest<bool>
{

    public List<RomaniaZm20> RomaniaZm20s { get; set; }
    public class CreateRomaniaZm20CommandHandler : IRequestHandler<CreateRomaniaZm20Command, bool>
    {
        private readonly IRomaniaZm20Repository _romaniaRomaniaZm20Repository;
        private readonly IMapper _mapper;


        public CreateRomaniaZm20CommandHandler(
            IRomaniaZm20Repository romaniaRomaniaZm20Repository,
            IMapper mapper
           )
        {
            _romaniaRomaniaZm20Repository = romaniaRomaniaZm20Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateRomaniaZm20Command request, CancellationToken cancellationToken)
        {
            bool result = await _romaniaRomaniaZm20Repository.AddListAsync(request.RomaniaZm20s) > 0 ? true : false;
            return result;
        }

    }

}

