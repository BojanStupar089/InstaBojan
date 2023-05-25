using FluentValidation;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Validators.ProfilesDtoValidator
{
    public class ProfileDtoValidator:AbstractValidator<ProfileDto>
    {

        public ProfileDtoValidator() {

            RuleFor(profile => profile.ProfilePicture).NotNull();
            RuleFor(profile => profile.ProfileName).NotNull().Length(1,30);
          
           
        
        }
    }
}
