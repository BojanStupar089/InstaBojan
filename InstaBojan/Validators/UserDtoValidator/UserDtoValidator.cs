using FluentValidation;
using InstaBojan.Dtos.UsersDto;

namespace InstaBojan.Validators.UserDtoValidator
{
    public class UserDtoValidator:AbstractValidator<UserDto>
    {

        public UserDtoValidator() {
            
            RuleFor(user => user.Email).NotNull().Length(2, 30).EmailAddress();
            RuleFor(user => user.UserName).NotNull().Length(2, 15);
            RuleFor(user=>user.Password).NotNull().Length(2, 15);
        }
    }
}
