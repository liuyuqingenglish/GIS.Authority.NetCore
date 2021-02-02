using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface ISystemService
    {
        List<SystemDto> GetSystemDto(ProtocolQuerySystem query);

        bool AddSystem(SystemDto dto);

        bool UpdateSystem(SystemDto dto);

        bool DeleteSystem(Guid sysId);
    }
}