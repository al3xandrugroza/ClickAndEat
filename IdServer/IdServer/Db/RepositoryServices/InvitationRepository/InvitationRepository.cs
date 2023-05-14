using AutoMapper;
using IdServer.Db.Models;
using IdServer.Dtos;
using IdServer.Utils;
using Microsoft.EntityFrameworkCore;

namespace IdServer.Db.RepositoryServices.InvitationRepository;

public class InvitationRepository : IInvitationRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public InvitationRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InvitationDto> GetByIdentifier(Guid key, CancellationToken cancellationToken)
    {
        var invitationEntity = await _context.InvitationEntities.SingleOrDefaultAsync(i => i.Identifier.Equals(key), cancellationToken) ?? throw new Exception("not found");
        await _context.Entry(invitationEntity)
            .Reference(i => i.OrganizationEntity)
            .LoadAsync(cancellationToken);

        var invitationDto = _mapper.Map<InvitationDto>(invitationEntity);
        invitationDto.OrganizationId = invitationEntity.OrganizationEntity.Identifier;

        return invitationDto;
    }

    public async Task<InvitationDto> CreateInvitation(string email, string role, Guid organizationId, CancellationToken cancellationToken)
    {
        if (!role.Equals(RoleType.Admin) && !role.Equals(RoleType.Emp)) throw new Exception("bad req");

        var organizationEntity = await _context.OrganizationEntities.SingleOrDefaultAsync(o => o.Identifier.Equals(organizationId), cancellationToken) ?? throw new Exception("not found");

        var invitationEntity = new InvitationEntity
        {
            Email = email,
            Role = role,
            OrganizationEntity = organizationEntity
        };

        await _context.InvitationEntities.AddAsync(invitationEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var invitationDto = _mapper.Map<InvitationDto>(invitationEntity);
        invitationDto.OrganizationId = invitationEntity.OrganizationEntity.Identifier;

        return invitationDto;
    }

    public async Task DeleteAllInvitationForEmailInAllOrganizations(string email, CancellationToken cancellationToken)
    {
        var invitationEntities = await _context.InvitationEntities
            .Where(i => i.Email.Equals(email))
            .ToListAsync(cancellationToken);

        _context.InvitationEntities.RemoveRange(invitationEntities);
        await _context.SaveChangesAsync(cancellationToken);
    }
}