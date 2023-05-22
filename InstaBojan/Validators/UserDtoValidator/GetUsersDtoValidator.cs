using FluentValidation;
using InstaBojan.Dtos.UsersDto;

namespace InstaBojan.Validators.UserDtoValidator
{
    public class GetUsersDtoValidator:AbstractValidator<GetUsersDto>
    {

        public GetUsersDtoValidator() { 
        
           RuleFor(user=>user.FirstName).NotNull().Length(2,30);
           RuleFor(user=>user.LastName).NotNull().Length(2,30);
           RuleFor(user => user.Email).NotNull().Length(2, 30).EmailAddress();
           RuleFor(user=>user.UserName).NotNull().Length(2,15);
          
        }
    }
}
