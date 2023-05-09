using Application.Services.IZm20Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ZM20S.Commands;

public class CreateZm20Command : IRequest<bool>
{

    public List<Zm20> Zm20s { get; set; }
    public class CreateZm20CommandHandler : IRequestHandler<CreateZm20Command, bool>
    {
        private readonly IZm20Repository _zm20Repository;
        private readonly IMapper _mapper;


        public CreateZm20CommandHandler(
            IZm20Repository zm20Repository,
            IMapper mapper
           )
        {
            _zm20Repository = zm20Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateZm20Command request, CancellationToken cancellationToken)
        {
            bool result = await _zm20Repository.AddListAsync(request.Zm20s) > 0 ? true : false;
            return result;
        }

    }

}

