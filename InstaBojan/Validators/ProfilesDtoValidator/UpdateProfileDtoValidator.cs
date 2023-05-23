using FluentValidation;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Validators.ProfilesDtoValidator
{
    public class UpdateProfileDtoValidator:AbstractValidator<UpdateProfileDto>
    {

        public UpdateProfileDtoValidator() {

            RuleFor(profile => profile.ProfilePicture).NotNull();
            RuleFor(profile => profile.ProfileName).NotNull().Length(1, 30);
          

        }
    }
}
