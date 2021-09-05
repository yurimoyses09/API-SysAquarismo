using Api.SysAquarismo.Dominos;
using Api.SysAquarismo.Entities;
using Api.SysAquarismo.Enums;
using Api.SysAquarismo.Interfaces;
using Api.SysAquarismo.RetornoEntities;
using System;
using System.Collections.Generic;

namespace Api.SysAquarismo.Funcao
{
    public class ComandosUsuario : IFuncionalidades
    {
        #region Clase onde tem todas as consultas no SQL
        readonly DataBaseSQL sql = new DataBaseSQL();
        #endregion

        /// <summary>
        /// String de retorno
        /// </summary>
        #region string de retorno
        string retorno = "";
        readonly RetornoRequisicao GetRetorno = new RetornoRequisicao();
        string ExecuteQuery = ""; // Executa comando no sql
        #endregion

        /// <summary>
        /// Metodo que realiza cadastro de um peixe no sistema
        /// </summary>
        /// <param name="peixe"></param>
        /// <returns></returns>
        public string CadastrarPeixe(Peixe peixe)
        {
            if (!peixe.id_usuario.Equals(null) &&
                !peixe.nm_peixe.Equals(null) &&
                !peixe.ds_especie.Equals(null) &&
                !peixe.ds_status_saude.Equals(null) &&
                !peixe.ds_data_aquisicao.Equals(null) &&
                !peixe.ds_sexo.Equals(null))
            {
                try
                {
                    var query = sql.InsertPeixe(peixe);
                    if (query.Equals(EnumStatusRequisicao.OK.ToString())) return retorno = EnumStatusRequisicao.OK.ToString();

                }
                catch (Exception ex)
                {
                    if (retorno.Equals("NOK")) return retorno + "-" + ex.Message;
                }
            }
            else
            {
                retorno = EnumStatusRequisicao.NOK.ToString() + " " + "campos obrigatorios estao vazios";
            }

            return retorno;
        }

        /// <summary>
        /// Metodo que realiza cadastro de um usuario no sistema 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string CadastraUsuario(Usuario usuario)
        {
            try
            {
                var query = sql.InsertUsuario(usuario);
                if (query.Equals(EnumStatusRequisicao.OK.ToString())) return retorno = EnumStatusRequisicao.OK.ToString();
            }
            catch (Exception ex)
            {
                if (retorno.Equals(EnumStatusRequisicao.NOK.ToString())) return retorno + " - " + ex.Message;
            }
            return retorno;
        }

        /// <summary>
        /// Metodo que verifica se usuario existe no banco de dados(realiza login)
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public string RealizaLogin(string nomeUsuario, string senha)
        {
            if (String.IsNullOrEmpty(nomeUsuario) || String.IsNullOrEmpty(senha)) // Vrifica se campos estao vazios
                return GetRetorno.retorno = "Alguns dados estão vazios ou nulos";

            try
            {
                ExecuteQuery = sql.SelectUsuarioLogin(nomeUsuario, senha);

                GetRetorno.retorno = ExecuteQuery switch
                {
                    "OK" => $"{ExecuteQuery} - Usuario Logou com sucesso",
                    "NOK" => $"{ExecuteQuery} - Usuario não existe na base de dados",
                    _ => $"{ExecuteQuery} - Exception",
                };
            }
            catch (Exception ex)
            {
                return $"{ExecuteQuery} - {ex.Message}";
            }
            return GetRetorno.retorno;
        }

        /// <summary>
        /// Metodo que retorna os dados de um peixe especifico por usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idPeixe"></param>
        /// <returns></returns>
        public c_RetornoPeixe VisualizarPeixe(int idUsuario, int idPeixe)
        {
            c_RetornoPeixe query;
            try
            {
                query = sql.InfoPeixe(idUsuario, idPeixe);
                if (!query.Equals(null))
                    return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return query;
        }

        /// <summary>
        /// Metodo que pea todos s peixes cadastrados pelo usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<c_RetornoPeixe> VisualizarPeixes(int idUsuario)
        {
            List<c_RetornoPeixe> query;
            try
            {
                query = sql.ListaPeixesPerUsuario(idUsuario);
                if (!query.Equals(null))
                    return query;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return query;
        }
    }
}
