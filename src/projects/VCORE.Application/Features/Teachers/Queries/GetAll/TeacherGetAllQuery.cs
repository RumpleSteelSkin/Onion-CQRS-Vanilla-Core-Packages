using Core.Application.Pipelines.Caching;
using MediatR;
using VCORE.Application.Features.Teachers.Constant;

namespace VCORE.Application.Features.Teachers.Queries.GetAll;

public class TeacherGetAllQuery : IRequest<ICollection<TeacherGetAllResponseDto>>, ICacheAbleRequest
{
    public string? CacheKey => "TeacherGetAllQuery";
    public string? CacheGroupKey => TeacherConstants.TeacherCacheGroup;
    public TimeSpan? SlidingExpiration => null;
}