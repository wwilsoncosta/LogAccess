using AccesLog.Repositories;
using AccessLogs.Domain.Entities;
using Antlr.Runtime.Tree;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using DotNet.Highcharts;
using PagedList;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Web.Mvc;

namespace AccesLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserRepository _usuarioRepository;
        private readonly LogAccessRepository _logAccessRepository;
        private readonly FileExport _fileExport;

        public HomeController()
        {
            _usuarioRepository = new UserRepository();
            _logAccessRepository = new LogAccessRepository();
            _fileExport = new FileExport();
        }

        public ActionResult Index()
        {
            if (Session["Autorizado"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "LogAcesso");
            }
        }


        public ActionResult Usuarios()
        {

            ViewBag.Title = "Usuários";
            ViewBag.Message = "Cadastro de Uusários";

            var listUsuarios = _usuarioRepository.GetUsers();
            ViewBag.Usuarios = listUsuarios;

            return View();
        }

        public ActionResult LogAcesso(int? id)
        {
            List<LogAccess> listAccess = _logAccessRepository.GetLogAccess();  
            if (id == null)
            {
                ViewBag.LogAcesso = _logAccessRepository.GetLogAccess();
                id = 0;
            }
            else
                ViewBag.LogAcesso = _logAccessRepository.GetLogAccessId(id);

            ViewBag.LogCountAccess =  _logAccessRepository.GetAccessHour(ViewBag.LogAcesso);

            ViewBag.LogUsuarios = _usuarioRepository.GetUsers();
            ViewBag.UsuarioSelecionadoId = id;

            ViewBag.Title = "Acessos";
            ViewBag.Message = "Histórico de acessos";

            return View();
        }

        [HttpPost]
        public void ExportFile()
        {
            _fileExport.GenerateXML(_logAccessRepository.GetLogAccess());
            Response.Redirect("/Home/LogAcesso");
        }
   }
}