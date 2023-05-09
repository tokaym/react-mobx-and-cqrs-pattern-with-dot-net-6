using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            //AOP
            RuleFor(u => u.UserRegistrationUpdateDto.Name).NotEmpty();
            RuleFor(u => u.UserRegistrationUpdateDto.Mail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.UserRegistrationUpdateDto.Surname).NotEmpty();
            
        }
    }
}