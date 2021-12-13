using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Metodos
{
   public class  CorreoHtml
    {
        public static string GetString()
        {
            // string fullPath = Path.Combine(Application.StartupPath, "imagenes\image1.jpg");
            string path = Path.GetFullPath(@"..\..\Resources\correo.html");
            //C:\\Users\\Administrador\\Desktop\\4.SW TR\\TR Aplicacion\\Aplicacion\\Aplicacion\\bin\\Resources\\correo.html"
            string text = System.IO.File.ReadAllText(@path);
            //C:\Users\Administrador\Desktop\4.SW TR\TR Aplicacion\Aplicacion\Aplicacion\Resources\correo.html
            System.Console.WriteLine("Contents of WriteText.txt = {0}", text);

            string[] lines = System.IO.File.ReadAllLines(@path);

            string htmlstring = ""; 
            foreach (string line in lines)
            {
                htmlstring = htmlstring + "\t" + line;
            }

            return htmlstring;
        }







    }
}
