using FluentValidation;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Validators.PostsDtoValidator
{
    public class AddPostDtoValidator:AbstractValidator<AddPostDto>
    {

        public AddPostDtoValidator() {

            RuleFor(post => post.Picture).NotNull();
            RuleFor(post=>post.ProfileId).NotNull();
           
        }
    }
}
