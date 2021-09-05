using Api.SysAquarismo.Funcao;

namespace Api.SysAquarismo.Entities
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nm_usuario { get; set; }
        public int vl_idade { get; set; }
        public string? ds_telefone { get; set; }
        public string nm_nome_usuario { get; set; }
        public string ds_senha { get; set; }
        public string ds_sexo { get; set; }
        public string ds_pais { get; set; }
    }

    /// <summary>
    /// Classe UsuarioAPI
    /// </summary>
    public class UsuarioAPI : Interfaces.IUsuario
    {
        /// <summary>
        /// Metodoque pega o id so usuario
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <returns>Retorna o id do usuario(int)</returns>
        public int GetIdUsuarioAPI(string nomeUsuario)
        {
            ComandosGetId comandosGet = new ComandosGetId();

            int retorno = comandosGet.GetIdUsuarioAPI(nomeUsuario);

            return retorno;
        }
    }
}
