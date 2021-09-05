using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.SysAquarismo.RetornoEntities
{
    public class c_RetornoPeixe
    {
        public string nm_peixe { get; set; }
        public string ds_especie { get; set; }
        public string ds_descrisao { get; set; }
        public double vl_peso { get; set; }
        public int vl_tamanho { get; set; }
        public DateTime ds_data_morte { get; set; }
        public byte img { get; set; }
        public string ds_status_saude { get; set; }
        public string ds_doenca { get; set; }
        public DateTime ds_data_aquisicao { get; set; }
        public string ds_sexo { get; set; }
        public DateTime DataExecucao { get; set; }
    }
}
