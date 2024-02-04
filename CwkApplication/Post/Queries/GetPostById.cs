﻿using CwkApplication.Models;
using CwkDomain.Aggragrate.PostAggragrate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwkApplication.Post.Queries
{
    public class GetPostById : IRequest<OperationResault<PostModel>>
    {
        public Guid PostId { get; set; }

    }
}
