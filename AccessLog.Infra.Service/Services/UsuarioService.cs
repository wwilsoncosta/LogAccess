
using AccessLog.Domain.Entities;

namespace AccessLog.Infra.Service.Services
{
    public class UsuarioService : Domain.Interfaces.IUsuariosService
    {
        public List<Usuarios> GetUsuarios()
        {
                var listUsuarios = new List<Usuarios>();
                var sql = "Select * from Usuarios";
            
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
                                        string admin = "Sim";
                                        if (Convert.ToInt32(dr["IsAdmin"]) == 0)
                                            admin = "Não";
            
            
                                        listUsuarios.Add(new Usuarios(
                                            Convert.ToInt32(dr["UsuarioId"]),
                                            dr["Nome"].ToString(),
                                            dr["Login"].ToString(),
                                            dr["Senha"].ToString(),
                                            admin,
                                            Convert.ToInt32(dr["IsAdmin"])
                                            ));
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
                return listUsuarios;
        }

        public bool Logar(string ipMaquina)
        {
            throw new NotImplementedException();
        }

        public void Salvar()
        {
            throw new NotImplementedException();
        }
    }
}