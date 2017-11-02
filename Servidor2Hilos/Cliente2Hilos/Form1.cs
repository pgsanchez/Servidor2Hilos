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

        Socket socket1;
        IPEndPoint direccion1;

        ObjIntercambio1 obj = new ObjIntercambio1();
        NetworkStream nw;
        BinaryFormatter bf;

        ThreadStart delegado1;
        Thread hilo1;

        public event EventHandler DataReceived;

        private ReaderWriterLock rwl = new ReaderWriterLock();
        

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
        /************ FIN Funciones para el hilo 1 de conexión con el Servidor *********/

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
        /************ FIN Funciones para el hilo de conexión con el Servidor *********/


    }
}
