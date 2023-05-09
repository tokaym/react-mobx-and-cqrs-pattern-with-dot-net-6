using Application.Services.IMb51Repository;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MB51s.Commands;

public class CreateMb51Command : IRequest<bool>
{
    public List<Mb51> Mb51s { get; set; }

    public class CreateMb51CommandHandler : IRequestHandler<CreateMb51Command, bool>
    {
        private readonly IMb51Repository _mb51Repository;
        private readonly IMapper _mapper;


        public CreateMb51CommandHandler(
            IMb51Repository mb51Repository,
            IMapper mapper
           )
        {
            _mb51Repository = mb51Repository;
            _mapper = mapper;
        }


        public async Task<bool> Handle(CreateMb51Command request, CancellationToken cancellationToken)
        {
            bool result = await _mb51Repository.AddListAsync(request.Mb51s) > 0 ? true : false;
            return result;
        }

    }

}

