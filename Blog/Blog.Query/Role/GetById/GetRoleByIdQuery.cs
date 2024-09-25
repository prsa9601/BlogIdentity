 using Blog.Infrastructure.Persistent.Ef;
using Blog.Query.Role.DTOs;
using Blog.Query.Role.GetById;
using Common.Query;
using Microsoft.EntityFrameworkCore;

namespace Blog.Query.Role.GetById
{
    public record class GetRoleByIdQuery(string roleId) : IQuery<RoleDto?>;

}

public class GetQueryByIdHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly BlogContext _context;

    public GetQueryByIdHandler(BlogContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == request.roleId);
        if (role == null)
            return null;
        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s=>s.Permission).ToList(),
            Name = role.Name
        };
    }
}