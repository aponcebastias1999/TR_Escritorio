using Aplicacion.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Metodos
{
    public static class htmlCreator
    {

        public static string GetHtml() {
            string cola = "<h5 id='h4'> Reporte de ganancias por reservaciones canceladas Diarias, Mensuales y Anuales (formato de fecha MM-DD-AAAA)</h5></center><br><table>";

            List<DataTable> data = new List<DataTable>();

            data.Add(ConvertList.ToDataTable(CancelacionService.GetData("D")));
            data.Add(ConvertList.ToDataTable(CancelacionService.GetData("M")));
            data.Add(ConvertList.ToDataTable(CancelacionService.GetData("Y")));

            List<string> names = new List<string>();
            names.Add("Diarias: ");
            names.Add("Mensuales: ");
            names.Add("Anuales: ");



            string dt(DataTable a ) {

                int name = 0;
                int c = 0;
                int r = 0;
                string encabezado = "";
                string registro ="";


                while (c < a.Columns.Count)
                {

                    encabezado = encabezado + "<th id='th'>" + a.Columns[c].ColumnName.ToString() + "</th> ";
                    c++;

                }
                c = 0;
                encabezado = "<tr id='tr'>" + encabezado + "</" + "tr>";

                //Retorna cosas 
                //Necesitamos los valores de las columnas.
                while (r < a.Rows.Count)
                {

                    if (c == a.Columns.Count)
                    {
                        registro =  registro + "</" + "tr> </br>  \n <tr id='tr'> ";
                        c = 0;
                        r++;


                        if (r == a.Rows.Count)
                        {
                            break;
                        }
                    }
                    registro = registro + "<td id='td'>" + a.Rows[r][c].ToString() + "</" + "td>";
                    
                    c++;
                }


                string tabla = "<b>" + names[name]+"<b/>" + " <td><table id='table'>" + encabezado + registro + "</table><td/> \n";
                encabezado = "\n";
                registro = "\n";

                name++; 
                
                return tabla;
            }

            string resultado ="\n";

            foreach (var a in data)
            {
                resultado = resultado + dt(a);

            }

            return cola + resultado +"</table></center> ";
        }
        
    }
}
