using System;
using System.Collections.Generic;
using System.Text;

namespace AccessLogs.Domain.Interfaces
{
    public interface IValidaSenha
    {
        bool PasswordValid(string senha);
        string CalcHashPassword(string senha);

    }
}
