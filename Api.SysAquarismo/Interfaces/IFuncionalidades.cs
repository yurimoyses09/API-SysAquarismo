using Api.SysAquarismo.Entities;
using Api.SysAquarismo.RetornoEntities;
using System.Collections.Generic;

namespace Api.SysAquarismo.Interfaces
{
    interface IFuncionalidades
    {
        public string RealizaLogin(string nomeUsuario, string senha);
        public string CadastraUsuario(Usuario usuario);
        public string CadastrarPeixe(Peixe peixe);
        public List<c_RetornoPeixe> VisualizarPeixes(int idUsuario);
        public c_RetornoPeixe VisualizarPeixe(int idUsuario, int idPeixe);
        //public bool RedefinirSenha(string nomeUsuario, string senhaNova, string senhaRepetida);
    }
}
