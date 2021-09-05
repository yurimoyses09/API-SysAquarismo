using Api.SysAquarismo.Entities;
using Api.SysAquarismo.Enums;
using Api.SysAquarismo.Querys;
using Api.SysAquarismo.RetornoEntities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Api.SysAquarismo.Dominos
{
    public class DataBaseSQL
    {
        /// <summary>
        /// Configuracao
        /// </summary>
        #region Configuracao
#pragma warning disable IDE0052 // Remove unread private members
        private IConfiguration configuration;
#pragma warning restore IDE0052 // Remove unread private members
        public void ValuesConfig(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public IConfigurationRoot GetConnection()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            return builder;
        }
        #endregion

        /// <summary>
        /// Objetos staticos
        /// </summary>
        #region Objetos Estaticos
        public static SqlConnection sqlconnection = new SqlConnection();
        public static SqlCommand comando = new SqlCommand();
        public static SqlParameter parametro = new SqlParameter();
        #endregion 

        /// <summary>
        /// Obtem a string de conecao
        /// </summary>
        /// <returns></returns>
        #region Obter SqlConnection
        public SqlConnection Connection()
        {
            try
            {
                string dadosConexao = GetConnection().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                sqlconnection = new SqlConnection(dadosConexao);

                if (sqlconnection.State == ConnectionState.Closed)
                    sqlconnection.Open();
                return sqlconnection;

            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Abre conexao com o banco de dados
        /// </summary>
        #region Abre Conexão
        public void Open()
        {
            sqlconnection.Open();
        }
        #endregion

        /// <summary>
        /// Fecha conexao com o banco de dados
        /// </summary>
        #region Fecha Conexão
        public void Close()
        {
            sqlconnection.Close();
        }
        #endregion

        /// <summary>
        /// Strings de retorno
        /// </summary>
        #region String de retono
        string retorno = "NOK";
        readonly RetornoRequisicao GetRetorno = new RetornoRequisicao();
        #endregion

        /// <summary>
        /// Querys para realizar comandos no sql
        /// </summary>
        #region quetys sql
        QuerysSql querys = new QuerysSql();
        #endregion

        /// <summary>
        /// Realiza select para verificar se usuario existe no banco de dados
        /// </summary>
        /// <param name="nmUsuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public string SelectUsuarioLogin(string nmUsuario, string senha)
        {
            var conn = Connection();

            if (conn.Equals(null))
                return GetRetorno.retorno = "Erro ao obter string de conexão ao sql";
            try
            {
                if (sqlconnection.State == ConnectionState.Open)
                {
                    comando = new SqlCommand(querys.SelectUsuarioLogin(), Connection());

                    comando.Parameters.AddWithValue("@nmUsuario", nmUsuario);
                    comando.Parameters.AddWithValue("@ds_senha", senha);

                    var execute = comando.ExecuteReader();
                    if (execute.Read())
                        GetRetorno.retorno = EnumStatusRequisicao.OK.ToString(); // Usuario logou
                    else
                        GetRetorno.retorno = EnumStatusRequisicao.NOK.ToString(); // Usuario nao existe no banco de dados
                }
            }
            catch (Exception ex)
            {
                return GetRetorno.retorno = ex.Message;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }

            return GetRetorno.retorno;
        }

        /// <summary>
        /// Realiza insert de um usuario nas bases de dados
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public string InsertUsuario(Usuario usuario)
        {
            try
            {
                var conn = Connection();
                if (!conn.Equals(null))
                {
                    if (sqlconnection.State == ConnectionState.Open)
                    {
                        string queryInsert = String.Format(querys.InsertUsuario(), usuario.nm_usuario, usuario.vl_idade, usuario.ds_telefone, usuario.nm_nome_usuario, usuario.ds_sexo, usuario.ds_sexo, usuario.ds_pais);

                        string querySelect = String.Format(querys.SelectCountUser(), usuario.nm_nome_usuario);
                        comando = new SqlCommand(querySelect, Connection());

                        var executeSelect = comando.ExecuteScalar();
                        if (executeSelect.Equals(1)) return retorno = EnumStatusRequisicao.NOK.ToString();
                        else
                        {
                            comando = new SqlCommand(queryInsert, Connection());
                            var execute = comando.ExecuteNonQuery();
                            if (execute > 0) return retorno = EnumStatusRequisicao.OK.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (retorno.Equals("NOK")) return retorno + " - " + ex.Message;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }
            return retorno;
        }

        /// <summary>
        /// Realiza insert de um peixe nas bases de dados
        /// </summary>
        /// <param name="peixe"></param>
        /// <returns></returns>
        public string InsertPeixe(Peixe peixe)
        {
            try
            {
                var conn = Connection();
                if (!conn.Equals(null))
                {
                    if (sqlconnection.State == ConnectionState.Open)
                    {
                        string query = String.Format(querys.InsertPeixe(), peixe.id_usuario, peixe.nm_peixe, peixe.ds_especie, peixe.ds_descrisao, peixe.vl_peso, peixe.vl_tamanho, peixe.ds_data_morte, peixe.img, peixe.ds_status_saude, peixe.ds_doenca, peixe.ds_data_aquisicao, peixe.ds_sexo);

                        comando = new SqlCommand(query, Connection());
                        var execute = comando.ExecuteNonQuery();
                        if (execute > 0) return retorno = EnumStatusRequisicao.OK.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                if (retorno.Equals(EnumStatusRequisicao.NOK.ToString())) return retorno + " - " + ex.Message;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }
            return retorno;
        }

        /// <summary>
        /// Realiza select para trazer todos os peixes de um usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<c_RetornoPeixe> ListaPeixesPerUsuario(int idUsuario)
        {
            List<c_RetornoPeixe> lstPeixe = new List<c_RetornoPeixe>();
            var conn = Connection();
            try
            {
                if (conn != null)
                {
                    if (sqlconnection.State == ConnectionState.Open)
                    {
                        comando = new SqlCommand(@"
                                        SELECT 
                                            * 
                                        FROM TB_PEIXES 
                                        WHERE 
                                            id_usuario = @id",
                                    Connection());

                        comando.Parameters.AddWithValue("@id", idUsuario);

                        var execute = comando.ExecuteReader();
                        while (execute.Read())
                        {
                            c_RetornoPeixe retornoPeixe = new c_RetornoPeixe
                            {
                                nm_peixe = execute.GetString("nm_peixe"),
                                ds_especie = execute.GetString("ds_especie"),
                                ds_descrisao = execute.GetString("ds_descricao"),
                                vl_peso = execute.GetDouble("vl_peso"),
                                vl_tamanho = execute.GetInt32("vl_tamanho"),
                                ds_data_morte = execute.GetDateTime("ds_data_morte"),
                                //img = execute.GetByte("img"),
                                ds_status_saude = execute.GetString("ds_status_saude"),
                                ds_doenca = execute.GetString("ds_doenca"),
                                ds_data_aquisicao = execute.GetDateTime("ds_data_aquisicao"),
                                ds_sexo = execute.GetString("ds_sexo")
                            };

                            lstPeixe.Add(retornoPeixe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (retorno.Equals("NOK")) throw ex;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }

            return lstPeixe;
        }

        /// <summary>
        /// Realiza select para trazr informação de um peixe especifico
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idPeixe"></param>
        /// <returns></returns>
        public c_RetornoPeixe InfoPeixe(int idUsuario, int idPeixe)
        {
            c_RetornoPeixe retornoPeixe = null;
            var conn = Connection();
            try
            {
                if (conn != null)
                {
                    if (sqlconnection.State == ConnectionState.Open)
                    {
                        comando = new SqlCommand(@"
                                        SELECT 
                                            * 
                                        FROM TB_PEIXES 
                                        WHERE 
                                            id_usuario = @id_usuario AND
                                            id_peixe = @id_peixe",
                                    Connection());

                        comando.Parameters.AddWithValue("@id_usuario", idUsuario);
                        comando.Parameters.AddWithValue("@id_peixe", idPeixe);

                        var execute = comando.ExecuteReader();
                        if (execute.Read())
                        {
                            retornoPeixe = new c_RetornoPeixe
                            {
                                nm_peixe = execute.GetString("nm_peixe"),
                                ds_especie = execute.GetString("ds_especie"),
                                ds_descrisao = execute.GetString("ds_descricao"),
                                vl_peso = execute.GetDouble("vl_peso"),
                                vl_tamanho = execute.GetInt32("vl_tamanho"),
                                ds_data_morte = execute.GetDateTime("ds_data_morte"),
                                //img = execute.GetByte("img"),
                                ds_status_saude = execute.GetString("ds_status_saude"),
                                ds_doenca = execute.GetString("ds_doenca"),
                                ds_data_aquisicao = execute.GetDateTime("ds_data_aquisicao"),
                                ds_sexo = execute.GetString("ds_sexo")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (retorno.Equals("NOK")) throw ex;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }

            return retornoPeixe;
        }

        /// <summary>
        /// Realiza select para pegar o Id de um usuario
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        public int SelectIdUsuario(string nomeUsuario)
        {
            int retornoIdUsuario = 0;
            try
            {
                var conn = Connection();
                if (!conn.Equals(null))
                {
                    if (sqlconnection.State == ConnectionState.Open)
                    {
                        string query = String.Format("SELECT id_usuario FROM TB_USUARIO WHERE nm_nome_usuario = '{0}'", nomeUsuario);
                        comando = new SqlCommand(query, Connection());
                        var execute = comando.ExecuteReader();
                        if (execute.Read()) return retornoIdUsuario = execute.GetInt32("id_usuario");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlconnection.State == ConnectionState.Open) Close();
            }
            return retornoIdUsuario;
        }

    }
}
