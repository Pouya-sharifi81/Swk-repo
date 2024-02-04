using CwkApplication.Enums;
using CwkApplication.Models;
using CwkApplication.Post.Queries;
using CwkDataAcces;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.QueryHandler
{
    public class GetAllPostHandler : IRequestHandler<GetAllPost, OperationResault<List<PostModel>>>
    {
        public readonly DataContext _ctx;
        public GetAllPostHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async  Task<OperationResault<List<PostModel>>> Handle(GetAllPost request, CancellationToken cancellationToken)
        {

                var result = new OperationResault<List<PostModel>>();
            try
            {
                var posts = await _ctx.Posts.ToListAsync();
                result.PayLoad = posts; 
            }
            catch (Exception e)
            {

                var error = new Error() { code = ErrorCode.ServerError, Massage = e.Message };
                result.IsError = true;
                result.Errors.Add(error);
            }
                return result;
        }
    }
}
