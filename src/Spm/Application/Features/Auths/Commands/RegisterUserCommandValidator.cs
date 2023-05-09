using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Auths.Commands;
using FluentValidation;

namespace Application.Features.Users.Commands
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            //AOP
            RuleFor(u => u.RegisterDto.Name)
                .NotEmpty()
                .MinimumLength(2);

            // RuleFor(u => u.RegisterDto.Password).NotEmpty();

            RuleFor(u => u.RegisterDto.Mail)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.RegisterDto.EmployeeNo)
                .NotEmpty();

        }
    }
}