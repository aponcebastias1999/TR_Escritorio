using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Aplicacion.Database;
using Aplicacion.Biblioteca_de_clases;
using Aplicacion.Metodos;
using System.Threading.Tasks;
using MahApps.Metro;

namespace Aplicacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Departamento dpto = new Departamento();
        Reserva resv = new Reserva();
        Cliente cli = new Cliente();

        public MainWindow()
        {

            InitializeComponent();
            cargarZona();
            cargarCbx();
            //GenerarReporte.ExportToPdf();
            ShowLoginDialog(null, null);
            
        }

        private void cargarZona()
        {

            var zona = LoadZoneService.GetData();

            foreach (Zona i in zona)
            {
                cbxZona.Items.Add(i.NOMBRE);
                cbxZona.SelectedIndex = i.ID_ZONA;
            }
        }
        public void cargarCbx()
        {

            List<ComboBox> cbx = new List<ComboBox>()
            {
                cbxCocina,
                cbxSala,
                cbxBalcon,
                cbxMantencion,
                cbxDisponible,
                cbxVigente,

            };

            for (int i = 0; i < cbx.Count; i++)
            {
                cbx[i].Items.Add("SI");
                cbx[i].Items.Add("NO");
                cbx[i].SelectedIndex = 0;
            }
        }
        public  void Aviso(string Titulo, string Mensaje)
        {
            async void texto()
            {
                
                await this.ShowMessageAsync(Titulo, string.Format(Mensaje));
            }
            texto();
        }

        private async void ShowLoginDialog(object sender, RoutedEventArgs e)
        {
            LoginDialogData result = await this.ShowLoginAsync("Validación", "Ingrese su correo y contraseña", new LoginDialogSettings { InitialUsername = "CORREO1@GMAIL.COM" , InitialPassword = "123"});
            
            if (result == null)
            {
                //User pressed cancel
            }
            else
            {

                var gmail = result.Username;
                var pass  = result.Password;
                
                tbxPass.Password = pass;
                tbxCorreo.Text = gmail;

                btnInicio_Click(null,null);

            }
        }

        //Agregar un input dialog

        //INICIO DE SESIÓN -------------------------------------
        private void btnInicio_Click(object sender, RoutedEventArgs e)
        {
            string hash = Hash.Encode(tbxPass.Password);
            var datos = LoginService.Login(tbxCorreo.Text, hash);

            var algo = datos == null ? 0 : 1;
            if (datos != null)
            { 

                
                Aviso("Bienvenido", "Hola " + datos.nombres + " has iniciado sesión.");
                matc.SelectedItem = mti_datos;

                txtFullname.Text = datos.nombres + " " + datos.apellidos;
                //Cargar datos
                lblNombre.Text = datos.nombres;
                lblapellidos.Text = datos.apellidos;
                lblcelular.Text = datos.celular;
                lblcorreo.Text = datos.correo;
                lblrut.Text = datos.rut;
            }
            else{

                Aviso("Estimado", "Su contraseña o nombre de usuario son incorrectos o no se encuentran registrados.");

            }

        }

       //---> ADMINISTRACIÓN DE EDIFIIOS/DEPARTAMENTOS
        private void btnEdificio_Click(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_addDpto;
           // dgDepartamentos.AutoGenerateColumns = true;
            dgDepartamentos.ItemsSource = LoadDepartamentoService.GetData();
            
            for (int i = 0; i < dgDepartamentos.Columns.Count; i++)
            {
                dgDepartamentos.Columns[i].Width = 40;
            }
            
           }

        private void btnCerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_inicio;
        }

        private void btbAgregarDpto_Click(object sender, RoutedEventArgs e)
        {
            
            string a = (cbxZona.SelectedIndex + 1).ToString();
            string b = (nudDormitorios.Value).ToString();
            string c = (nudBaños.Value).ToString();
            string d = (cbxCocina.SelectedItem).ToString();
            string z = (cbxSala.SelectedItem).ToString();
            string g = (cbxBalcon.SelectedItem).ToString();
            string h = (nudMetros.Value).ToString();
            string i = (cbxMantencion.SelectedItem).ToString();
            string j = (cbxDisponible.SelectedItem).ToString();
            string k = (cbxVigente.SelectedItem).ToString();
            string l = (nudValor.Value).ToString();
            string m = (txtReseña.Text).ToString();

            var dato = AgregarDptoService.Agregar(a,b,c,d,z,g,h,i,j,k,l,m);


            if (dato != null)
            {
                Aviso("Inserción Realizada con exito",
                    " Numero de departamento: " + dato.ID_DEPTO + "\n" +
                    " Zona ID: " + dato.ZONA_ID_ZONA + "\n" +
                    " Dormitorios: " + dato.DORMITORIOS + "\n" +
                    " Baños: " + dato.BAÑOS + "\n" +
                    " Cocina: " + dato.COCINA + "\n" +
                    " Metros Cuadrados: " + dato.METROS_CUADRADOS + "\n" +
                    " Mantencion: " + dato.MANTENCION + "\n" +
                    " Disponibilidad " + dato.DISPONIBLE + "\n" +
                    " Vigente: " + dato.VIGENTE + "\n" +
                    " Valor dia: $" + dato.VALOR_DIA + "\n" +
                    " Reseña: " + dato.RESEÑA);

                dgDepartamentos.ItemsSource = LoadDepartamentoService.GetData();
            }
            else
            {
                Aviso("ERROR", "VERIFIQUE LOS DATOS ENVIADOS");
            }


        }

        private void dgDepartamentos_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var a =(dgDepartamentos.CurrentItem);

            if (a != null && a.ToString() == "Aplicacion.Biblioteca_de_clases.Departamento")
            {
                dpto = (Departamento)a;

                cbxZona.SelectedIndex       = int.Parse(dpto.ZONA_ID_ZONA);
                nudDormitorios.Value        = int.Parse(dpto.DORMITORIOS);
                nudBaños.Value              = int.Parse(dpto.BAÑOS);
                cbxCocina.SelectedItem      = dpto.COCINA;
                cbxSala.SelectedItem        = dpto.SALA_DE_ESTAR;
                cbxBalcon.SelectedItem      = dpto.BALCON;
                nudMetros.Value             = int.Parse(dpto.METROS_CUADRADOS);
                cbxMantencion.SelectedItem  = dpto.MANTENCION;
                cbxDisponible.SelectedItem  = dpto.DISPONIBLE;
                cbxVigente.SelectedItem     = dpto.VIGENTE;
                nudValor.Value              = int.Parse(dpto.VALOR_DIA);
                txtReseña.Text              = dpto.RESEÑA;

            }


        }

        private void btbActualizarDpto_Click(object sender, RoutedEventArgs e)
        {
                      
            dpto.ZONA_ID_ZONA       = (string)cbxZona.SelectedIndex.ToString()      ;
            dpto.DORMITORIOS        = (string)nudDormitorios.Value.ToString()       ;
            dpto.BAÑOS              = (string)nudBaños.Value.ToString()             ;
            dpto.COCINA             = (string)cbxCocina.SelectedItem     ;
            dpto.SALA_DE_ESTAR      = (string)cbxSala.SelectedItem       ;
            dpto.BALCON             = (string)cbxBalcon.SelectedItem     ;
            dpto.METROS_CUADRADOS   = nudMetros.Value.ToString()            ;
            dpto.MANTENCION         = (string)cbxMantencion.SelectedItem ;
            dpto.DISPONIBLE         = (string)cbxDisponible.SelectedItem ;
            dpto.VIGENTE            = (string)cbxVigente.SelectedItem    ;
            dpto.VALOR_DIA          = (string)nudValor.Value.ToString()             ;
            dpto.RESEÑA             = (string)txtReseña.Text;

            var dato = ActualizarDptoService.Actualizar(dpto);

            if (dato != null)
            {
                Aviso("ACTUALIZCIÓN Realizada con exito",
                    " Numero de departamento: " + dato.ID_DEPTO + "\n" +
                    " Zona ID: " + dato.ZONA_ID_ZONA + "\n" +
                    " Dormitorios: " + dato.DORMITORIOS + "\n" +
                    " Baños: " + dato.BAÑOS + "\n" +
                    " Cocina: " + dato.COCINA + "\n" +
                    " Metros Cuadrados: " + dato.METROS_CUADRADOS + "\n" +
                    " Mantencion: " + dato.MANTENCION + "\n" +
                    " Disponibilidad " + dato.DISPONIBLE + "\n" +
                    " Vigente: " + dato.VIGENTE + "\n" +
                    " Valor dia: $" + dato.VALOR_DIA + "\n" +
                    " Reseña: " + dato.RESEÑA + "\n"+
                    " Creación: " + dato.CREACION);
                    

                dgDepartamentos.ItemsSource = LoadDepartamentoService.GetData();
            }
            else
            {
                Aviso("ERROR", "VERIFIQUE LOS DATOS ENVIADOS");
            }
        }

        private void btbEliminarDpto_Click(object sender, RoutedEventArgs e)
        {
            var dato  = EliminarDptoService.Eliminar(dpto);


            if (dato != null)
            {
                Aviso("ELIMINACIÓN Realizada con exito",
                    " Numero de departamento: " + dato.ID_DEPTO + "\n" +
                    " Zona ID: " + dato.ZONA_ID_ZONA + "\n" +
                    " Dormitorios: " + dato.DORMITORIOS + "\n" +
                    " Baños: " + dato.BAÑOS + "\n" +
                    " Cocina: " + dato.COCINA + "\n" +
                    " Metros Cuadrados: " + dato.METROS_CUADRADOS + "\n" +
                    " Mantencion: " + dato.MANTENCION + "\n" +
                    " Disponibilidad " + dato.DISPONIBLE + "\n" +
                    " Vigente: " + dato.VIGENTE + "\n" +
                    " Valor dia: $" + dato.VALOR_DIA + "\n" +
                    " Reseña: " + dato.RESEÑA + "\n" +
                    " Creación: " + dato.CREACION);



                dgDepartamentos.ItemsSource = LoadDepartamentoService.GetData();
            }
            else
            {
                Aviso("ERROR", "El departamento que intenta eliminar contiene reservaciones y pagos asociados");
            }

        }
        private void btnRegresarClick(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_datos;
        }
        //<---- ADMINISTRACIÓN DE DEPARTAMENTOS 


        //---> Reserva
        private void btnReservaciones_Click(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_addReservación;
            dgReserva.ItemsSource = ResvLoadService.GetData();

        }
        private void btnRegresarResvClick(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_datos;
        }

        private void btbAgregarResv_Click(object sender, RoutedEventArgs e)
        {
            resv.DEPARTAMENTO_ID_DEPTO  = nudDEPARTAMENTO_ID_DEPTO.Value.ToString();
            resv.CLIENTE_ID_CLIENTE     = nudCLIENTE_ID_CLIENTE.Value.ToString();
            resv.FECHA_INICIO           = dtpFECHA_INICIO.SelectedDate.ToString();
            resv.FECHA_TERMINO          = dtpFECHA_TERMINO.SelectedDate.ToString();
            resv.MONTO_INICIAL          = nudMONTO_INICIAL.Value.ToString();
            resv.POR_PAGAR              = nudPOR_PAGAR.Value.ToString();
            resv.TOTAL_ACTIVIDADES      = nudTOTAL_ACTIVIDADES.Value.ToString();
            resv.TOTAL_PAGAR            = nudTOTAL_PAGARCANCELADA.Value.ToString();
            resv.CANCELADA              = nudCANCELADO.Value.ToString();

            var dato = ResvAgregarService.Agregar(resv);

            if (dato != null)
            {
                string msj = " Numero de reservación: " + dato.ID_RESER + "\n" +
                    " Numero de departamento: " + dato.DEPARTAMENTO_ID_DEPTO + "\n" +
                    " Numero del cliente: " + dato.CLIENTE_ID_CLIENTE + "\n" +
                    " Fecha inicio: " + dato.FECHA_INICIO + "\n" +
                    " Fecha termino: " + dato.FECHA_TERMINO + "\n" +
                    " Monto inicial: $" + dato.MONTO_INICIAL + "\n" +
                    " Por pagar: $" + dato.POR_PAGAR + "\n" +
                    " Total actividades: $" + dato.TOTAL_ACTIVIDADES + "\n" +
                    " Total pagar: $" + dato.TOTAL_PAGAR + "\n" +
                    " Cancelado(1=SI;0=NO):  " + dato.CANCELADA;


                Aviso("Inserción Realizada con exito", msj);

                bool resp = EnvioReporte.EnviarCHECKIN(dato.CORREO,"Acta Checkin ",msj);

                if (resp == true)
                {
                    Aviso("Informacion", "Acta Checkin se envio al destinatario" + dato.CORREO);
                }
                else
                {
                    Aviso("Informacion", "Error al enviar al destinatario" + dato.CORREO);
                }
              
                dgReserva.ItemsSource = ResvLoadService.GetData();


            }
            else
            {
                Aviso("VERIFIQUE LOS DATOS", " ◉ Verifique que el id cliente exista \n ◉ Verifique que el id departamento exista \n ◉ Compruebe que no hayan campos en blanco \n  si el problema persiste comuniquese con el desarrollador.");
            }

        }

        private void btbActualizarResv_Click(object sender, RoutedEventArgs e)
        {
            
            resv.DEPARTAMENTO_ID_DEPTO = nudDEPARTAMENTO_ID_DEPTO.Value.ToString();
            resv.CLIENTE_ID_CLIENTE = nudCLIENTE_ID_CLIENTE.Value.ToString();
            resv.FECHA_INICIO = dtpFECHA_INICIO.SelectedDate.ToString();
            resv.FECHA_TERMINO = dtpFECHA_TERMINO.SelectedDate.ToString();
            resv.MONTO_INICIAL = nudMONTO_INICIAL.Value.ToString();
            resv.POR_PAGAR = nudPOR_PAGAR.Value.ToString();
            resv.TOTAL_ACTIVIDADES = nudTOTAL_ACTIVIDADES.Value.ToString();
            resv.TOTAL_PAGAR = nudTOTAL_PAGARCANCELADA.Value.ToString();
            resv.CANCELADA = nudCANCELADO.Value.ToString();
            

            var dato = ResvActualizarService.Actualizar(resv);

            if (dato != null)
            {
                Aviso("Actualización Realizada con exito",
                    " Numero de reserva actualizada: " + dato.ID_RESER + "\n" +
                    " Numero de reservación: " + dato.ID_RESER + "\n" +
                    " Numero de departamento: " + dato.DEPARTAMENTO_ID_DEPTO + "\n" +
                    " Numero del cliente: " + dato.CLIENTE_ID_CLIENTE + "\n" +
                    " Fecha inicio: " + dato.FECHA_INICIO + "\n" +
                    " Fecha termino: " + dato.FECHA_TERMINO + "\n" +
                    " Monto inicial: $" + dato.MONTO_INICIAL + "\n" +
                    " Por pagar: $" + dato.POR_PAGAR + "\n" +
                    " Total actividades: $" + dato.TOTAL_ACTIVIDADES + "\n" +
                    " Total pagar: $" + dato.TOTAL_PAGAR + "\n" +
                    " Cancelado(1=SI;0=NO):  " + dato.CANCELADA + "\n");

                dgReserva.ItemsSource = ResvLoadService.GetData();


            }
            else
            {
                Aviso("VERIFIQUE LOS DATOS", " ◉ Verifique que el id cliente exista \n ◉ Verifique que el id departamento exista \n ◉ Compruebe que no hayan campos en blanco \n  si el problema persiste comuniquese con el desarrollador.");
            }

        }

        private void btbEliminarResv_Click(object sender, RoutedEventArgs e)
        {
            var dato = ResvEliminarService.Eliminar(resv);

            if (dato != null)
            {
                Aviso("ELIMINACIÓN Realizada con exito",
                    " Numero de reserva eliminada: " + dato.ID_RESER + "\n" +
                    " Numero de reservación: " + dato.ID_RESER + "\n" +
                    " Numero de departamento: " + dato.DEPARTAMENTO_ID_DEPTO + "\n" +
                    " Numero del cliente: " + dato.CLIENTE_ID_CLIENTE + "\n" +
                    " Fecha inicio: " + dato.FECHA_INICIO + "\n" +
                    " Fecha termino: " + dato.FECHA_TERMINO + "\n" +
                    " Monto inicial: $" + dato.MONTO_INICIAL + "\n" +
                    " Por pagar: $" + dato.POR_PAGAR + "\n" +
                    " Total actividades: $" + dato.TOTAL_ACTIVIDADES + "\n" +
                    " Total pagar: $" + dato.TOTAL_PAGAR + "\n" +
                    " Cancelado(1=SI;0=NO):  " + dato.CANCELADA + "\n");

                dgReserva.ItemsSource = ResvLoadService.GetData();
            }
            else
            {
                Aviso("ERROR", "La reserva que intenta eliminar contiene reservaciones y pagos asociados, finalice el proceso.");
            }

        }

        private void dgReserva_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var a = (dgReserva.CurrentItem);

            if (a != null && a.ToString() == "Aplicacion.Biblioteca_de_clases.Reserva")
            {
                resv = (Reserva)a;

                int  dia = int.Parse(resv.FECHA_INICIO.ToString().Substring(0,2));
                int año = int.Parse(resv.FECHA_INICIO.ToString().Substring(6, 4));
                int mes = int.Parse(resv.FECHA_INICIO.ToString().Substring(3,2));

                int tdia = int.Parse(resv.FECHA_TERMINO.ToString().Substring(0, 2));
                int taño = int.Parse(resv.FECHA_TERMINO.ToString().Substring(6, 4));
                int tmes = int.Parse(resv.FECHA_TERMINO.ToString().Substring(3, 2));

                
                nudDEPARTAMENTO_ID_DEPTO.Value       = int.Parse(resv.DEPARTAMENTO_ID_DEPTO); 
                nudCLIENTE_ID_CLIENTE.Value          = int.Parse(resv.CLIENTE_ID_CLIENTE); 
                dtpFECHA_INICIO.SelectedDate         = new DateTime(año,mes,dia);
                dtpFECHA_TERMINO.SelectedDate        = new DateTime(taño, tmes, tdia);
                nudMONTO_INICIAL.Value               = int.Parse(resv.MONTO_INICIAL);
                nudPOR_PAGAR.Value                   = int.Parse(resv.POR_PAGAR);
                nudTOTAL_ACTIVIDADES.Value           = int.Parse(resv.TOTAL_ACTIVIDADES);
                nudTOTAL_PAGARCANCELADA.Value        = int.Parse(resv.TOTAL_PAGAR);
                nudCANCELADO.Value                   = int.Parse(resv.CANCELADA);

            }

        }
        //<----

        //---> Cliente
        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            matc.SelectedItem = mti_addCliente;
            dgClientes.ItemsSource = CliLoadService.GetData();
        }

        private void dgClientes_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var a = (dgClientes.CurrentItem);

            if (a != null && a.ToString() == "Aplicacion.Biblioteca_de_clases.Cliente")
            {
                cli = (Cliente)a;


                tbxNOMBRES.Text     = cli.NOMBRES;
                tbxAPELLIDOS.Text   = cli.APELLIDOS;
                tbxRUT.Text         = cli.RUT;
                nudCELULAR.Value    = int.Parse(cli.CELULAR);
                tbxCORREO.Text =    cli.CORREO;
                tbxDIRECCION.Text   = cli.DIRECCION;
                passCLAVE.Password  = "";

            }
        }
        private void btbAgregarCliente_Click(object sender, RoutedEventArgs e)
        {

            cli.NOMBRES     = tbxNOMBRES.Text.ToString();
            cli.APELLIDOS   = tbxAPELLIDOS.Text;
            cli.RUT         = tbxRUT.Text;
            cli.CELULAR     = nudCELULAR.Value.ToString();
            cli.CORREO      = tbxCORREO.Text;
            cli.DIRECCION   = tbxDIRECCION.Text;
            cli.CLAVE       = passCLAVE.Password.ToString();

            var dato = CliAgregarService.Agregar(cli);

            if (dato != null)
            {
                Aviso("Inserción Realizada con exito",
                    " Numero de cliente: " + dato.ID_CLIENTE + "\n" +
                    " Nombres: " + dato.NOMBRES + "\n" +
                    " Apellidos: " + dato.APELLIDOS + "\n" +
                    " Rut: " + dato.RUT + "\n" +
                    " Celular: " + dato.CELULAR + "\n" +
                    " Correo: " + dato.CORREO + "\n" +
                    " Dirección" + dato.DIRECCION + "\n");

                dgClientes.ItemsSource = CliLoadService.GetData();
            }
            else
            {
                Aviso("Error al ingresar los datos", " ◉ Verifique que el id cliente exista \n ◉ Verifique que el id departamento exista \n ◉ Compruebe que no hayan campos en blanco \n  si el problema persiste comuniquese con el desarrollador.");
            }
        }
        private void btbActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            cli.NOMBRES     = tbxNOMBRES.Text.ToString();
            cli.APELLIDOS   = tbxAPELLIDOS.Text;
            cli.RUT         = tbxRUT.Text;
            cli.CELULAR     = nudCELULAR.Value.ToString();
            cli.CORREO      = tbxCORREO.Text;
            cli.DIRECCION   = tbxDIRECCION.Text;
            cli.CLAVE       = passCLAVE.Password.ToString();

            var dato =CliActualizarService.Actualizar(cli);

            if (dato != null)
            {
                Aviso("Actualización Realizada con exito",
                        " Numero de cliente: " + dato.ID_CLIENTE + "\n" +
                        " Nombres: " + dato.NOMBRES + "\n" +
                        " Apellidos: " + dato.APELLIDOS + "\n" +
                        " Rut: " + dato.RUT + "\n" +
                        " Celular: " + dato.CELULAR + "\n" +
                        " Correo: " + dato.CORREO + "\n" +
                        " Dirección" + dato.DIRECCION + "\n");

                dgClientes.ItemsSource = CliLoadService.GetData();


            }
            else
            {
                Aviso("VERIFIQUE LOS DATOS", " ◉ Verifique que el id cliente exista \n ◉ Verifique que el id departamento exista \n ◉ Compruebe que no hayan campos en blanco \n  si el problema persiste comuniquese con el desarrollador.");
            }

        }
        private void btbEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            var dato = CliEliminarService.Eliminar(cli);


            if (dato != null)
            {
                Aviso("Actualización Realizada con exito",
                         " Numero de cliente: " + dato.ID_CLIENTE + "\n" +
                         " Nombres: " + dato.NOMBRES + "\n" +
                         " Apellidos: " + dato.APELLIDOS + "\n" +
                         " Rut: " + dato.RUT + "\n" +
                         " Celular: " + dato.CELULAR + "\n" +
                         " Correo: " + dato.CORREO + "\n" +
                         " Dirección" + dato.DIRECCION + "\n");


                dgClientes.ItemsSource = CliLoadService.GetData();
            }
            else
            {
                Aviso("ERROR", "El departamento que intenta eliminar contiene reservaciones y pagos asociados");
            }
        }

        //-reporte
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool resp = GenerarReporte.ExportToPdf();

            if (resp == true)
            {
                Aviso("Informacion","Envio de reporte completado");
            }
            else
            {
                Aviso("Informacion", "Error al enviar el reporte ");
            }
        }



    }
}
