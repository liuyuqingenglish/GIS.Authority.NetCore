using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface ISystemService
    {
        PageResult<SystemDto> GetSystemDto( PageQueryCondition<ProtocolQuerySystem,PageQuery> query);

        bool AddSystem(SystemDto dto);

        bool UpdateSystem(SystemDto dto);

        bool DeleteSystem(Guid sysId);
    }
}