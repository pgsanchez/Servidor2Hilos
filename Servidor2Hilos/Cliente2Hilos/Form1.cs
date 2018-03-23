using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization;
using LibreriaIntercambio;


namespace Cliente2Hilos
{
    public partial class Form1 : Form
    {
        int variablePrueba = 0;
        
        // Conexión 1 para RECIBIR datos del coche
        Socket socket1;
        IPEndPoint direccion1;

        ObjIntercambio1 obj = new ObjIntercambio1();
        NetworkStream nw;
        BinaryFormatter bf;

        ThreadStart delegado1;
        Thread hilo1;

        // Conexión 2 para ENVIAR datos al coche
        Socket socket3;
        IPEndPoint direccion3;

        ObjIntercambio2 obj2 = new ObjIntercambio2();
        NetworkStream nw2;
        BinaryFormatter bf2;
        ThreadStart delegado2;
        Thread hilo2;

        public event EventHandler DataReceived;

        private ReaderWriterLock rwl = new ReaderWriterLock();
        private ReaderWriterLock rw2 = new ReaderWriterLock();
        

        public Form1()
        {
            InitializeComponent();

            DataReceived += btnPrueba_Click;
        }

        /************ Funciones para el hilo 1 de conexión con el Servidor *********/
        private void FuncionHilo1()
        {

            configurarConexion();
            while (true)
            {
                conectarConServidor();

                rwl.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    obj = (ObjIntercambio1)(bf.Deserialize(nw));
                }
                catch
                {
                    //nw.Close();
                    //socket1.Close();
                }
                finally
                {
                    //Release the lock.
                    rwl.ReleaseWriterLock();
                }

                OnDataReceived(EventArgs.Empty);
            }
        }

        private void configurarConexion()
        {
            socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            direccion1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        }

        private int conectarConServidor()
        {
            try
            {
                socket1.Connect(direccion1);

                nw = new NetworkStream(socket1);
                bf = new BinaryFormatter();
            }
            catch (Exception error)
            {
                return -1;
            }
            return 1;
        }

        protected virtual void OnDataReceived(EventArgs e)
        {
            EventHandler handler = DataReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        /************ FIN Funciones para el hilo 2 de conexión con el Servidor *********/

        private int recibirDatos(int x)
        {
            int longitud = 0;

            obj = (ObjIntercambio1)(bf.Deserialize(nw));

            //tbSensor1.Text = x.ToString();
            //tbSensorVal1.Text = obj.numInt.ToString();
            //tbSensor1.Update();
            //tbSensorVal1.Update();
            return 0;

        }



        private int desconectarDelServidor()
        {
            try
            {
                socket1.Close();
            }
            catch (Exception error)
            {
                return -1;
            }
            return 1;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //configurarConexion();
            //conectarConServidor();
            /*for (int i = 1; i < 101; i++)
            {
                recibirDatos(0);
                Thread.Sleep(100);
            }*/
            //Creamos el delegado 
            delegado1 = new ThreadStart(FuncionHilo1);
            //Creamos la instancia del hilo 
            hilo1 = new Thread(delegado1);
            //Iniciamos el hilo 
            hilo1.Start();

        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            desconectarDelServidor();
            hilo1.Abort();
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            variablePrueba++;
            textBoxPrueba.Text = variablePrueba.ToString();

                rwl.AcquireReaderLock(Timeout.Infinite);
                tbSensor1.Text = obj.cadena;
                tbSensorVal1.Text = obj.numInt.ToString();
                rwl.ReleaseReaderLock();
                tbSensor1.Update();
                tbSensorVal1.Update();
            
        }

        private void btnAdelante_Click(object sender, EventArgs e)
        {
            rw2.AcquireReaderLock(Timeout.Infinite);
            obj2.numInt2++;
            rw2.ReleaseReaderLock();

        }

        //protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        protected override bool ProcessKeyPreview(ref Message msg) 
        {
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            int msgVal = msg.WParam.ToInt32();

            if (msg.Msg == WM_KEYDOWN)
            {
                if ((Keys)msgVal == Keys.Up)
                    textBoxPrueba.Text = "Arriba";
                else if ((Keys)msgVal == Keys.Down)
                    textBoxPrueba.Text = "Abajo";
                else if ((Keys)msgVal == Keys.Left)
                    textBoxPrueba.Text = "Izquierda";
                else if ((Keys)msgVal == Keys.Right)
                    textBoxPrueba.Text = "Derecha";
            }
            else if (msg.Msg == WM_KEYUP)
                textBoxPrueba.Text = "Tecla suelta";
            return true;
        }
        /************ FIN Funciones para el hilo de conexión con el Servidor *********/


        /*****************************************************************************/
        /**************** MANDAR ÓRDENES AL COCHE (Hilo 2) ***************************/
        // Avanzar + velocidad: Avanzar será un número positivo. Cuanto mayor sea el número, más velocidad.
        // Retroceder + velocidad: será un número negativo. Igual que Avanzar, pero hacia atrás.
        // Girar a derecha + ángulo: será un número positivo que indicará cuanto de giradas están las ruedas a la derecha (*)
        // Girar a izquierda + ángulo: será un número negativo que indicará cuanto de giradas están las ruedas a la izquierda (*)
        // Parar: Se enviará un 0
        // Sin órdenes (orden void)
        // (*) el valor que se enviará para el giro será un ángulo entre 0 y el MAX que pueden girar las ruedas. Habrá que amoldar
        //  el valor que devuelve el joystick a ese ángulo que hay que mandar.

        private void CrearHilo2()
        {
            //Creamos el delegado 
            delegado2 = new ThreadStart(FuncionHilo2);
            //Creamos la instancia del hilo 
            hilo2 = new Thread(delegado2);
            //Iniciamos el hilo 
            hilo2.Start();
        }

        private void FuncionHilo2()
        {
            configurarConexion2();
            while (true)
            {
                conectarConServidor2();

                //rwl.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    //obj = (ObjIntercambio1)(bf.Deserialize(nw));
                }
                catch
                {

                }
                finally
                {
                    //Release the lock.
                    //rwl.ReleaseWriterLock();
                }

                //OnDataReceived(EventArgs.Empty);
            }
        }
        private void configurarConexion2()
        {
            socket3 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            direccion3 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1236);
        }

        private int conectarConServidor2()
        {
            byte[] byARecibir = new byte[255];
            try
            {
                socket3.Bind(direccion1);
                socket3.Listen(1);
                //Servidor escuchando.Esperando conexión...
                Socket socket4 = socket3.Accept();
                //Servidor conectado...

                NetworkStream nw = new NetworkStream(socket4);
                BinaryFormatter bf = new BinaryFormatter();

                bool salir = false;
                while (salir == false)
                {
                    Thread.Sleep(100);
                    rw2.AcquireReaderLock(Timeout.Infinite);
                    try
                    {
                        bf2.Serialize(nw2, obj2);
                    }
                    catch
                    {
                        nw2.Close();
                        socket3.Close();
                        salir = true;
                    }
                    finally
                    {
                        //Release the lock.
                        rw2.ReleaseReaderLock();
                    }
                }
            }
            catch (Exception error)
            {
                //Console.WriteLine("Error: {0}", error.ToString());
            }
            return 0;
        }



    }
}
