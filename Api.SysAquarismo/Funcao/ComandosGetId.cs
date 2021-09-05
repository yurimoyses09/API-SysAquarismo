using Api.SysAquarismo.Dominos;
using Api.SysAquarismo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SysAquarismo.Funcao
{
    public class ComandosGetId : Interfaces.IUsuario
    {
        /// <summary>
        /// Classe com todos os comando no sql
        /// </summary>
        #region Clase onde tem todas as consultas no SQL
        readonly DataBaseSQL sql = new DataBaseSQL();
        #endregion
        
        /// <summary>
        /// String de retorno
        /// </summary>
        #region string de retorno
        string retorno = "NOK";
        int retornoIdUsuario = -1;
        #endregion

        /// <summary>
        /// Metodo que executa query para trazar o id
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        public int GetIdUsuarioAPI(string nomeUsuario)
        {
            if (!nomeUsuario.Equals(String.Empty))
            {
                try
                {
                    int query = sql.SelectIdUsuario(nomeUsuario);
                    if (!query.Equals(null))
                    {
                        retornoIdUsuario = query;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return retornoIdUsuario;
        }
    }
}
