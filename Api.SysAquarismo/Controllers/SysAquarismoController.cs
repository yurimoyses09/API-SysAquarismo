using Api.SysAquarismo.Entities;
using Api.SysAquarismo.Enums;
using Api.SysAquarismo.Funcao;
using Api.SysAquarismo.RetornoEntities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Api.SysAquarismo.Controllers
{
    /// <summary>
    /// API SysAquarismo
    /// </summary>
    [ApiController]
    [Route("api/v2/SysAquarisno/")]
    public class SysAquarismoController : ControllerBase
    {
        #region comandos
        readonly ComandosUsuario comando = new ComandosUsuario();
        readonly UsuarioAPI comandoGetId = new UsuarioAPI();
        #endregion

        /// <summary>
        /// Endpoint que Realiza login
        /// </summary>
        /// <param name="nmUsuario"></param>
        /// <param name="senha"></param>
        /// <returns>string</returns>
        [Route("RealizaLogin")]
        [HttpGet]
        public string RealizarLogin(string nmUsuario, string senha)
        {
            try
            {
                return comando.RealizaLogin(nmUsuario, senha);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Edpoint que Cadastra usuario no sistema
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [Route("CadastrarUsuario")]
        [HttpPost]
        public string CadastraUsuario(Usuario usuario)
        {
            try
            {
                var cm = comando.CadastraUsuario(usuario);
                if (!cm.Equals(EnumStatusRequisicao.OK.ToString()))
                    return "Usuario nao cadastrado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Usuario Cadastrado";
        }

        /// <summary>
        /// EndPoint que cadastra peixe no sistema
        /// </summary>
        /// <param name="peixe"></param>
        /// <returns></returns>
        [Route("CadastrarPeixe")]
        [HttpPost]
        public string CadastraPeixe(Peixe peixe)
        {
            string retornoRequisicaoCadastrarPeixe;
            try
            {
                retornoRequisicaoCadastrarPeixe = comando.CadastrarPeixe(peixe);
                if (retornoRequisicaoCadastrarPeixe.Equals(EnumStatusRequisicao.OK.ToString())) return "Peixe cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return retornoRequisicaoCadastrarPeixe;
        }

        /// <summary>
        /// EndPoint que seleciona peixes por usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        [Route("PeixesPerUsuario")]
        [HttpGet]
        public List<c_RetornoPeixe> PeixesPerUsuario(int id_usuario)
        {
            var cm = comando.VisualizarPeixes(id_usuario);
            if (cm.Count > 0)
                return cm;
            return cm;
        }

        /// <summary>
        /// endpoint que seleciona todos os peixes do usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_peixe"></param>
        /// <returns></returns>
        [Route("PeixeUsuario")]
        [HttpGet]
        public c_RetornoPeixe PeixesPUsuario(int id_usuario, int id_peixe)
        {
            var cm = comando.VisualizarPeixe(id_usuario, id_peixe);
            if (!cm.Equals(null))
                return cm;
            return cm;
        }

        [Route("IdUsuario")]
        [HttpGet]
        public int IdUsuario(string nomeUsuario)
        {
            return comandoGetId.GetIdUsuarioAPI(nomeUsuario);
        }
    }
}
