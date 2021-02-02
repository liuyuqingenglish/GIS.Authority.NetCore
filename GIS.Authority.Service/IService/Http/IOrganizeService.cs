using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface IOrganizeService
    {
        List<OrganizationDto> GetOrganizeDto(ProtocolQueryOrganize query);

        bool AddOrganize(OrganizationDto dto);

        bool UpdateOrganize(OrganizationDto dto);

        bool DeleteOrganize(List<Guid> orgId);
    }
}