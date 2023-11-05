using AccessLogs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessLogs.Domain.Interfaces
{
    public interface IFileExportRepository
    {
        string GenerateXML(List<LogAccess> logAccesses);
        string VerifiDir();

    }
}
