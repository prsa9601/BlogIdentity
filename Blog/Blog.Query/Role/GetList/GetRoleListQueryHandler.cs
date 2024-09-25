using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Role.DTOs;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Role.GetList;

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly BlogContext _context;

    public GetRoleListQueryHandler(BlogContext context)
    {
        _context = context;
    }
    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(role => new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Name = role.Name
        }).ToListAsync(cancellationToken);
    }
}