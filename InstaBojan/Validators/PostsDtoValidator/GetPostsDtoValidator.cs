using FluentValidation;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Validators.PostsDtoValidator
{
    public class GetPostsDtoValidator:AbstractValidator<GetPostsDto>
    {

        public GetPostsDtoValidator() {

            RuleFor(post => post.Picture).NotNull();
            
        }
    }
}
