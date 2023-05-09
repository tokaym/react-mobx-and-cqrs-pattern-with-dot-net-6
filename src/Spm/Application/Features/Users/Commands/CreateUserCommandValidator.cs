using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //AOP
            RuleFor(u => u.RegisterDto.Name).NotEmpty();
            // RuleFor(u => u.RegisterDto.Password).NotEmpty();
            RuleFor(u => u.RegisterDto.EmployeeNo).NotEmpty();
            RuleFor(u => u.RegisterDto.Mail)
                .NotEmpty()
                .EmailAddress();
            
        }
    }
}