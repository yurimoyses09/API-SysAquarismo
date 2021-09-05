
namespace Api.SysAquarismo.Querys
{
    public class QuerysSql
    {
        /// <summary>
        /// Query que realiza login e verifica se usuario existe no banco de dados
        /// </summary>
        /// <returns></returns>
        public string SelectUsuarioLogin() 
        {
            return @"SELECT * FROM TB_USUARIO WHERE nm_nome_usuario = @nmUsuario AND ds_senha = @ds_senha";
        }

        public string InsertUsuario() 
        {
            return @"INSERT INTO TB_USUARIO VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', '{6}')";
        }

        public string SelectCountUser() 
        {
            return @"SELECT COUNT(nm_nome_usuario) AS 'USER' FROM TB_USUARIO WHERE nm_nome_usuario = '{0}'";
        }

        public string InsertPeixe() 
        {
            return @"INSERT INTO TB_PEIXES VALUES({0}, '{1}', '{2}', '{3}', {4}, {5}, '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')";
        }
    }
}
