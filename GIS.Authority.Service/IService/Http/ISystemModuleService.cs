using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface ISystemModuleService
    {
        List<ModuleDto> GetSystemModuleDto(ProtocolQuerySystemModule query);

        bool UpdateSystemModule(ModuleDto dto);
    }
}