using Aplicacion.Biblioteca_de_clases;
using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Database
{
    public class CancelacionService
    {
        internal static List<Cancelacion> GetData(string dma) {
        
            using (var connection = OracleProvider.GetConnection())
            {

             var p = new OracleDynamicParameters();
             p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);

              var resultado = connection.Query<Cancelacion>(@"SP_SELECT"+dma
                                            , p
                                            , commandType: System.Data.CommandType.StoredProcedure)
                                            .ToList();
               
                return resultado;
                

            }
        
        
        }



    }
}
