using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SysAquarismo.Interfaces
{
    /// <summary>
    /// Interface IUsuario
    /// </summary>
    interface IUsuario
    {
        /// <summary>
        /// Realiza query para pegar o id do usuario
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <returns>int</returns>
        public int GetIdUsuarioAPI(string nomeUsuario);
    }
}
