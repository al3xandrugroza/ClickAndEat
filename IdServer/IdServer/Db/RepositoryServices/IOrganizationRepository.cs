﻿using IdServer.Db.Models;

namespace IdServer.Db.RepositoryServices;

public interface IOrganizationRepository
{
    Task<OrganizationEntity> CreateOrganization(CancellationToken cancellationToken);
    Task<OrganizationEntity> GetByIdentifier(Guid key, CancellationToken cancellationToken);
}