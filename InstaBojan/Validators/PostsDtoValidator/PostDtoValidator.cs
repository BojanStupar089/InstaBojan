using FluentValidation;
using InstaBojan.Dtos.PostsDto;

namespace InstaBojan.Validators.PostsDtoValidator
{
    public class PostDtoValidator:AbstractValidator<PostDto>
    {

        public PostDtoValidator() {

            RuleFor(post => post.Picture).NotNull();
            
           
        }
    }
}
