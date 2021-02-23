using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IOrganizeService
    {
        PageResult<OrganizationDto> GetOrganizeDto( PageQueryCondition< ProtocolQueryOrganize,PageQuery> query);

        bool AddOrganize(OrganizationDto dto);

        bool UpdateOrganize(OrganizationDto dto);

        bool DeleteOrganize(List<Guid> orgId);
    }
}