using FluentValidation;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Validators.ProfilesDtoValidator
{
    public class GetProfilesDtoValidator:AbstractValidator<GetProfilesDto>
    {

        public GetProfilesDtoValidator() {


            RuleFor(profile => profile.ProfilePicture).NotNull();
            RuleFor(profile => profile.ProfileName).NotNull().Length(1, 30);
            RuleFor(profile => profile.BirthDay).Null();
            RuleFor(profile => profile.Gender).Null();
            RuleFor(profile => profile.NumberFollowers).Null();
            RuleFor(profile => profile.NumberFollowing).Null();

        }
    }
}
