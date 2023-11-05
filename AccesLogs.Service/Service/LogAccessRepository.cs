using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using AccessLogs.Domain.Entities;
using AccessLogs.Domain.Interfaces;

namespace AccesLogs.Service.Service
{
    public class LogAccessRepository : ILogAccessRepository
    {
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public void LoginAccess(int usuarioId, string ipMaquina)
        {
            try
            {
                using (var con = new SqlConnection(_conn))
                {
                    con.Open();

                    Dapper.DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("UsuarioId", usuarioId);
                    parameters.Add("EnderecoIp", ipMaquina);

                    var customer = con.QuerySingleOrDefault<LogAccess>("SP_LogAccess", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha :" + ex.Message);
            }
        }
        public List<LogAccess> GetLogAccess()
        {
            DataSet ds = new DataSet();

            var listLogAcess = new List<LogAccess>();

            try
            {
                using (var con = new SqlConnection(_conn))
                {

                    SqlDataAdapter da;
                    da = new SqlDataAdapter("SP_GetLogAccess", con);

                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    da.Fill(ds);
                    int i = 0;
                    while (ds.Tables[0].Rows.Count > i)
                    {
                        LogAccess log = new LogAccess();
                        log.Nome = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                        log.ipAcesso = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                        log.DataHoraAcesso = DateTime.Parse(ds.Tables[0].Rows[i].ItemArray[2].ToString());
                        log.UsuarioId = int.Parse(ds.Tables[0].Rows[i].ItemArray[3].ToString());
                        listLogAcess.Add(log);
                        i++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha :" + ex.Message);
            }
            return listLogAcess;
        }
        public List<LogAccessCount> GetAccessHour(List<LogAccess> logAccesses)
        {
            List<LogAccessCount> listLogAcess = new List<LogAccessCount>();
            for (int i = 0; i < 23; i++)
            {
                var count = logAccesses.FindAll(x => x.DataHoraAcesso.Hour == i);
                LogAccessCount logAccessCount = new LogAccessCount(count.Count, i.ToString());
                listLogAcess.Add(logAccessCount);
            }
            return listLogAcess;
        }
        public List<LogAccess> GetLogAccessId(int? usuarioId)
        {
            var listLogAcess = new List<LogAccess>();
            var sql = @"	SELECT  U.Nome, 
				            L.EnderecoIp, 
				            L.DataHoraAcesso,
				            U.UsuarioId
		            FROM [dbo].[LogAcesso] AS L
		            INNER JOIN [dbo].[Usuarios] AS U ON L.UsuarioId = U.UsuarioId
		             where L.UsuarioId = " + usuarioId + " ORDER BY L.DataHoraAcesso";

            var usuario = new Usuarios();
            try
            {
                using (var con = new SqlConnection(_conn))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    LogAccess log = new LogAccess();
                                    log.Nome = dr["Nome"].ToString();
                                    log.ipAcesso = dr["EnderecoIp"].ToString();
                                    log.DataHoraAcesso = DateTime.Parse(dr["DataHoraAcesso"].ToString());
                                    log.UsuarioId = int.Parse(dr["UsuarioId"].ToString());
                                    listLogAcess.Add(log);
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha :" + ex.Message);
            }
            return listLogAcess;
        }
    }
}