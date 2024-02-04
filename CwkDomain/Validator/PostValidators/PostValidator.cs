using CwkDomain.Aggragrate.PostAggragrate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkDomain.Validator.PostValidators
{
    public class PostValidator : AbstractValidator<PostModel>
    {
        public PostValidator()
        {
            RuleFor(p => p.TextContent)
          .NotNull().WithMessage("Post text content can't be null")
          .NotEmpty().WithMessage("Post text content can't be empty");
        }
    }
}
