using AccessLogs.Domain.Entities;
using AccesLog.Repositories;
using System.Net;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace AccesLog.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserRepository _usuarioRepository;
        private readonly ValidPasswordRepository _validaSenhaRepository;
        private readonly static string _conn = WebConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        public LoginController()
        {
            _validaSenhaRepository = new ValidPasswordRepository();
            _usuarioRepository = new UserRepository();
        }

        // GET: Login
        public ActionResult Index()
        {
            if (Session["Erro"] != null)
                ViewBag.Erro = Session["Erro"].ToString();

            return View();
        }

        [HttpPost]
        public ActionResult ValidarLogin()
        {
            string hashSenha = _validaSenhaRepository.CalcHashPassword(Request["senha"]);
            var usuario = new Usuarios();
            usuario.Login = Request["Login"];
            usuario.Senha = hashSenha;


            string host = Dns.GetHostName();
            string ip = Dns.GetHostAddresses(host)[1].ToString();
            if (_usuarioRepository.LogInto(ip, usuario))
            {
                Session["Autorizado"] = "Ok";
                Session["NomeUsuario"] = usuario.Nome;
                Session["PrimeiraLetra"] = usuario.Nome.Substring(0,1);
                Session.Remove("Erro");
                return RedirectToAction("LogAcesso", "Home");
            }
            else
            {
                Session["Erro"] = "Usuario ou senha invalido!";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}