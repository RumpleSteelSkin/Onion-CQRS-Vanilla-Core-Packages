﻿using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using VCORE.Application.Constants;
using VCORE.Application.Features.Teachers.Constant;

namespace VCORE.Application.Features.Teachers.Commands.Create;

public class TeacherAddCommand : IRequest<string>, IRoleExists,ICacheRemoverRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime HireDate { get; set; }
    public string[] Roles { get; } = [GeneralOperationClaims.Admin];

    public string? CacheKey => null;
    public string? CacheGroupKey => TeacherConstants.TeacherCacheGroup;
}