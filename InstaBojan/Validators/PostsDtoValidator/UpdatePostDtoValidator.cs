using FluentValidation;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Validators.PostsDtoValidator
{
    public class UpdatePostDtoValidator:AbstractValidator<UpdatePostDto>
    {

        public UpdatePostDtoValidator() {

            RuleFor(post => post.Picture).NotNull();
            RuleFor(post => post.Text).Null();
        }
    }
}
