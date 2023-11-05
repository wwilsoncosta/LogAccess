using AccesLog.Repositories;
using AccessLogs.Domain.Entities;
using AccessLogs.Domain.Interfaces;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AccesLogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogAccessRepository _logAccessRepository;
        private readonly IEmpregadoRepository _empregadoRepository;
        public UsuariosController(IUsuarioRepository usuarioRepository, ILogger<UsuariosController> logger, ILogAccessRepository logAccessRepository, IEmpregadoRepository empregadoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _logAccessRepository = logAccessRepository;
            _empregadoRepository = empregadoRepository;
        }

        [HttpGet("api/getusuarios")]
        public ActionResult<List<Usuarios>> GetUsers()
        {
            try
            {
                var list = _usuarioRepository.GetUsers();
                return Ok(list);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("api/getusuarios/{id}")]
        public ActionResult<List<Usuarios>> GetUserId(int id)
        {
            try
            {
                var list = _usuarioRepository.GetUserId(id);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult<Usuarios> SaveUsers(Usuarios usuarios)
        {
            try
            {
                _usuarioRepository.Save(usuarios);
                return Ok(usuarios);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<Usuarios> UpdateUser(Usuarios usuarios)
        {
            try
            {
                _usuarioRepository.Save(usuarios);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public ActionResult<Usuarios> DeleteUser(int Id)
        {
            try
            {
                _usuarioRepository.Delete(Id);
                return Ok(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("api/logacessos")]
        public ActionResult<List<LogAccessCount>> GetLogAccess()
        {
            try
            {
                var listAccess = _logAccessRepository.GetLogAccess();
                var list = _logAccessRepository.GetAccessHour(listAccess);
                return Ok(list.OrderBy(x => x.hora));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/Employees")]
        public async Task<ActionResult<List<Empregado>>> GetEmployees()
        {
            try
            {
                HttpClient cliente = new HttpClient();
                var dadosEmpregado = await cliente.GetStringAsync("https://dummy.restapiexample.com/api/v1/employees");

                var result = _empregadoRepository.SelecionaEmpregados(dadosEmpregado);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
