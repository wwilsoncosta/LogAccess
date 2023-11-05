using AccessLogs.Domain.Entities;
using AccessLogs.Domain.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace AccessLog.Service.Services
{
    public class EmpregadosService : IEmpregadoRepository
    {
        public IEnumerable<Empregado> SelecionaEmpregados(string dados)
        {
            Empregados? empregados = Newtonsoft.Json.JsonConvert.DeserializeObject<Empregados>(dados.ToString());

            List<Empregado> listaEmpregados = new List<Empregado>();
            foreach (var data in empregados.data)
            {
                if(data.employee_age > 30)
                {
                    Empregado emp = new Empregado();
                    emp.Id = data.id;
                    emp.Nome = data.employee_name;
                    emp.Idade = data.employee_age;

                    listaEmpregados.Add(emp);
                }
            }
            return listaEmpregados;
        }
    }
}
