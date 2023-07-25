using FluentValidation;
using InstaBojan.Dtos.ProfilesDto;

namespace InstaBojan.Validators.ProfilesDtoValidator
{
    public class GetProfilesDtoValidator : AbstractValidator<GetProfilesDto>
    {

        public GetProfilesDtoValidator()
        {



            RuleFor(profile => profile.ProfileName).NotNull().Length(1, 30);


        }
    }
}
