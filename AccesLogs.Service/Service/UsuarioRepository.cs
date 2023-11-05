using System;
using System.Collections.Generic;
using System.Web.Configuration;
using Microsoft.Data.SqlClient;
using AccessLogs.Domain.Entities;
using AccessLogs.Domain.Interfaces;

namespace AccesLogs.Service.Service
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LogAccessRepository _logAccessRepository;
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public UsuarioRepository()
        {
            _logAccessRepository = new LogAccessRepository();
        }

        public void Excluir(int UsuarioId)
        {
            var sql = "";
            sql = "DELETE FROM Usuarios WHERE UsuarioId = " + UsuarioId;
            try
            {
                using (var con = new SqlConnection(_conn))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha :" + ex.Message);
            }
        }

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

        public Usuarios GetUsuariosId(int usuarioId)
        {
            var listUsuarios = new List<Usuarios>();
            var sql = "Select * from Usuarios where UsuarioId = " + usuarioId;
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
                                if (dr.Read())
                                {

                                    usuario.UsuarioId = usuarioId;
                                    usuario.Nome = dr["Nome"].ToString();
                                    usuario.Login = dr["Login"].ToString();
                                    usuario.Senha = dr["Senha"].ToString();
                                    usuario.IsAdmin = Convert.ToInt32(dr["IsAdmin"]);

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
            return usuario;
        }

        public bool Logar(string ipMaquina, Usuarios usuarios)
        {
            var result = false;
            var sql = "Select UsuarioId, Nome, Senha From Usuarios where Login = '" + usuarios.Login + "' and Senha = '" + usuarios.Senha + "'";

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
                                if (dr.Read())
                                {
                                    if (usuarios.Senha == dr["Senha"].ToString())
                                    {
                                        _logAccessRepository.LoginAccess(Convert.ToInt32(dr["UsuarioId"]), ipMaquina);
                                        usuarios.Nome = dr["Nome"].ToString();
                                        result = true;
                                    }
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

            return result;
        }

        public void Salvar(Usuarios usuarios)
        {
            var sql = "";
            if (usuarios.UsuarioId == 0)
                sql = "INSERT INTO Usuarios (Nome, Login, Senha, IsAdmin) values(@Nome, @Login, @Senha, @IsAdmin)";
            else
                sql = "UPDATE Usuarios SET Nome = @Nome, Login =@Login, Senha = @Senha, IsAdmin = @IsAdmin WHERE UsuarioId = " + usuarios.UsuarioId;
            try
            {
                using (var con = new SqlConnection(_conn))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", usuarios.Nome);
                        cmd.Parameters.AddWithValue("@Login", usuarios.Login);
                        cmd.Parameters.AddWithValue("@Senha", usuarios.Senha);
                        cmd.Parameters.AddWithValue("@IsAdmin", usuarios.IsAdmin);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha :" + ex.Message);
            }
        }


    }
}