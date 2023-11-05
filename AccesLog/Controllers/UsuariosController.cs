using AccessLogs.Domain.Entities;
using AccesLog.Repositories;
using System.Web.Mvc;

namespace AccesLog.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserRepository _usuarioRepository;
        private readonly ValidPasswordRepository _validaSenhaRepository;

        public UsuariosController()
        {
            _usuarioRepository = new UserRepository();
            _validaSenhaRepository = new ValidPasswordRepository();
        }

        // GET: Usuarios
        public ActionResult AdicionarUsuarios()
        {
            if (Session["Autorizado"] != null)
            {
                ViewBag.Title = "Usuários";
                ViewBag.Message = "Cadastro de Usuários";

                if (Session["ErroCadastro"] != null)
                    ViewBag.ErroCadastro = Session["ErroCadastro"].ToString();

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        public ActionResult AlterarUsuarios(int id)
        {
            if (Session["Autorizado"] != null)
            {
                ViewBag.Title = "Usuários";
                ViewBag.Message = "Atualizar de Usuários";

                ViewBag.Usuarios = _usuarioRepository.GetUserId(id);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult ExcluirUsuarios(int id)
        {
            if (Session["Autorizado"] != null)
            {
                ViewBag.Title = "Usuários";
                ViewBag.Message = "Excluir Usuário";

                ViewBag.Usuarios = _usuarioRepository.GetUserId(id);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Salvar()
        {
            string hashSenha = _validaSenhaRepository.CalcHashPassword(Request["senha"]);
            Usuarios usuarios = new Usuarios();
            usuarios.UsuarioId = int.Parse(Request["usuarioId"].ToString());
            usuarios.Nome = Request["nome"];
            usuarios.Login = Request["login"];
            usuarios.Senha = hashSenha;
            usuarios.IsAdmin = int.Parse(Request["isadmin"]);

            var senhaValida = _validaSenhaRepository.PasswordValid(Request["senha"]);
            if (senhaValida)
            {
                _usuarioRepository.Save(usuarios);
                if (Session["ErroCadastro"] != null)
                    ViewBag.ErroCadastro = "";
            }

            else
            {
                Session["ErroCadastro"] = "Senha inválida!";
                return RedirectToAction("AdicionarUsuarios", "Usuarios");
            }

            return RedirectToAction("Usuarios", "Home");

        }
        [HttpPost]
        public void Excluir()
        {
            _usuarioRepository.Delete(int.Parse(Request["usuarioId"].ToString()));

            Response.Redirect("/Home/Usuarios");

        }
    }
}