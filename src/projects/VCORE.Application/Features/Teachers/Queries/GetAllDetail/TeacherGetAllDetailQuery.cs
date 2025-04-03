using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using MediatR;
using VCORE.Application.Constants;
using VCORE.Application.Features.Teachers.Constant;

namespace VCORE.Application.Features.Teachers.Queries.GetAllDetail;

public class TeacherGetAllDetailQuery : IRequest<ICollection<TeacherGetAllDetailResponseDto>>, ICacheAbleRequest,
    IRoleExists
{
    public string? CacheKey => "TeacherGetAllDetailQuery";
    public string? CacheGroupKey => TeacherConstants.TeacherCacheGroup;
    public TimeSpan? SlidingExpiration => null;
    public string[] Roles => [GeneralOperationClaims.Admin];
}