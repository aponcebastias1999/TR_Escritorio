using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Aplicacion.Database;
using System.IO;
using Aplicacion.Metodos;
using System.Windows.Controls;
using System.Windows.Media;

namespace Aplicacion.Biblioteca_de_clases
{
    public static class GenerarReporte
    {

        public static bool ExportToPdf()
        {

            string strFilePath = Path.GetFullPath(@"..\..\..\..\reportes\reporte "+DateTime.Now.ToString()+".pdf");
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(strFilePath, FileMode.Create));

            document.Open();
            iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);


            List<DataTable> dt = new List<DataTable>();

            dt.Add(ConvertList.ToDataTable(ResvLoadService.GetData())) ;
            dt.Add(ConvertList.ToDataTable(CliLoadService.GetData()));
            dt.Add(ConvertList.ToDataTable(LoadDepartamentoService.GetData()));

            Paragraph txt = new Paragraph("REPORTE TURISMO REAL         " + DateTime.Now.ToString());
            document.Add(txt);

            List<string> nombres = new List<string>();
            nombres.Add(ResvLoadService.CLASSNAME);
            nombres.Add(CliLoadService.CLASSNAME);
            nombres.Add(LoadDepartamentoService.CLASSNAME);
            int y = 0;

            foreach (var x in dt) {

                PdfPTable table = new PdfPTable(x.Columns.Count);

                PdfPRow row = null;

                float[] widths = new float[x.Columns.Count];
                for (int i = 0; i < x.Columns.Count; i++)
                    widths[i] = 4f;

                table.SetWidths(widths);

                table.WidthPercentage = 100;
                int iCol = 0;
                string colname = "";
                PdfPCell cell = new PdfPCell(new Phrase("Reporte"));

                cell.Colspan = x.Columns.Count;


                
                foreach (DataColumn c in x.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font5));
                }


                foreach (DataRow r in x.Rows)
                {
                    if (x.Rows.Count > 0)
                    {
                        for (int h = 0; h < x.Columns.Count; h++)
                        {
                            table.AddCell(new Phrase(r[h].ToString(), font5));
                        }
                    }
                }

                
                document.Add(new Paragraph("\n"+ nombres[y] ) );
                document.Add(new Paragraph("\n"));
                y++;
                document.Add(table);


            }
            document.Close();

            bool resp = EnvioReporte.EnviarMail( "a.poncebastias@gmail.com","Reporte TURISMO REAL","Adjunto el reporte: ");

            if (resp == false)
            {
                return false;
            }
            else {
                return true;  
            }

        }


    }
}
