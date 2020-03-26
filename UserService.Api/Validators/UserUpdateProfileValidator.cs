﻿using FluentValidation;
using UserService.Infrastructure.DTO;

namespace UserService.Api.Validators
{
    public class UserUpdateProfileValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateProfileValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.FirstName).NotNull().MaximumLength(255).WithMessage("FirstName is empty or more than 255 simbols");
            RuleFor(x => x.SecondName).NotNull().MaximumLength(255).WithMessage("SecondName is empty or more than 255 simbols");
            RuleFor(x => x.Email).NotEmpty().MaximumLength(127).WithMessage("Email is empty or more than 127 simbols"); ;
        }
    }
}
