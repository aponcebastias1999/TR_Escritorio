using Aplicacion.Biblioteca_de_clases;
using Dapper;
using Dapper.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Database
{
    public class ResvLoadService
    {
        public const string CLASSNAME = "Lista de Reservaciones";
        internal static List<Reserva> GetData()
        {

            using (var connection = OracleProvider.GetConnection())
            {

                var p = new OracleDynamicParameters();
                p.Add(name: "outREFCURSOR", direction: System.Data.ParameterDirection.Output, dbType: OracleMappingType.RefCursor);



                var resultado = connection.Query<Reserva>(@"SP_SELECTRESV"
                                            , p
                                            , commandType: System.Data.CommandType.StoredProcedure)
                                            .ToList();
                

               
                return resultado;
                

            }

        }


    }
}
