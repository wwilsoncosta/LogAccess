using AccessLogs.Domain.Entities;
using AccessLogs.Domain.Interfaces;
using System.IO;
using System.Collections.Generic;
using System;

namespace AccesLog.Repositories
{
    public class FileExport : IFileExportRepository
    {
        public const string diretorio = @"C:\Temp\";
        public string _xmlFilename;

        public FileExport()
        {
            _xmlFilename = @diretorio + "export" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year+ "-" + DateTime.Now.TimeOfDay.ToString().Replace(":","") + ".xml";
        }

        public string GenerateXML(List<LogAccess> logAccesses)
        {
            VerifiDir();
            try
            {
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<LogAccess>));

                var file = new StreamWriter(_xmlFilename);
                writer.Serialize(file, logAccesses);
                file.Close();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return _xmlFilename;
        }

        public string VerifiDir()
        {
            DirectoryInfo dir = new DirectoryInfo(diretorio);
            if(dir.Exists)
                return dir.FullName;
            else
                dir.Create();
            return dir.FullName;
        }
    }
}