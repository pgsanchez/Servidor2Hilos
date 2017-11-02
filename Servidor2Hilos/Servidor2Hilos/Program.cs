using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using LibreriaIntercambio;

namespace Servidor2Hilos
{
    class Program
    {
        private ReaderWriterLock rwl = new ReaderWriterLock();

        // "ObjIntercambio1 obj" es un objeto que pretenece a la clase y, por lo tanto, será
        // accesible desde todos sus métodos, aunque se ejecuten en distinto hilo. Para controlar
        // este acceso, se utiliza siempre el "ReaderWriterLock rwl", que se llama antes de hacer 
        // cualquier cosa con "obj".
        ObjIntercambio1 obj = new ObjIntercambio1(); 

        static void Main(string[] args)
        {
            Program programaPrincipal = new Program();
            //***** HILO 1: el que enviará datos al cliente *****//
            //** Ejecutará la función: programaPrincipal.conectarConCliente **//
            //Creamos el delegado 
            ThreadStart delegado1 = new ThreadStart(programaPrincipal.conectarConCliente); 
            //Creamos la instancia del hilo 
            Thread hilo1 = new Thread(delegado1); 
            //Iniciamos el hilo 
            Console.WriteLine("Hilo 1 Start...");
            hilo1.Start();
 
            //***** HILO 2 ********//
            //** Ejecutará la función: programaPrincipal.procesarSensores **//
            //Creamos el delegado 
            ThreadStart delegado2 = new ThreadStart(programaPrincipal.procesarSensores);
            //Creamos la instancia del hilo 
            Thread hilo2 = new Thread(delegado2); 
            //programaPrincipal.procesarSensores();
            //Iniciamos el hilo 
            Console.WriteLine("Hilo 2 Start...");
            hilo2.Start();
            
            //Se espera a que el Hilo1 termine.
            hilo1.Join();
            hilo2.Abort();

            // Fin del programa. Se cerrará la consola.
            Console.WriteLine("Fin del programa...");
            Console.ReadKey();
            
        }

        // Función para el HILO 1 de conectar con el cliente y mandarle datos.
        private void conectarConCliente()
        {
            Socket socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint direccion1 = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);

            int i = 1; // contador de test. Quitar al final.
            bool salir = false;
            byte[] byARecibir = new byte[255];
            try
            {
                socket1.Bind(direccion1);
                socket1.Listen(1);
                Console.WriteLine("Servidor escuchando.Esperando conexión...");
                Socket socket2 = socket1.Accept();
                Console.WriteLine("Servidor conectado...");
                //int longitud = socket2.Receive(byARecibir);
                //Array.Resize(ref byARecibir, longitud);
                //Console.WriteLine(Encoding.Default.GetString(byARecibir));
                NetworkStream nw = new NetworkStream(socket2);
                BinaryFormatter bf = new BinaryFormatter();

                while (salir == false)
                {
                    Thread.Sleep(100);
                    rwl.AcquireReaderLock(Timeout.Infinite);
                    try
                    {
                        bf.Serialize(nw, obj);
                        //Console.WriteLine("Datos enviados {0}: cadena = {1}, número = {2}", i, obj.cadena, obj.numInt);
                        i++;
                        //Thread.Sleep(50);
                    }
                    catch
                    {
                        nw.Close();
                        socket1.Close();
                        Console.WriteLine("Ha ocurrido un error. Se cierra la conexión.");
                        salir = true;
                    }
                    finally
                    {
                        //Release the lock.
                        rwl.ReleaseReaderLock(); 
                    }
                }
                
                /*nw.Close();
                socket1.Close();
                Console.WriteLine("Texto de prueba enviado al cliente");*/
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: {0}", error.ToString());
            }
        }

        //******* HILO 2: el que va a chequear los sensores *****//
        private void procesarSensores()
        {
            Thread.Sleep(50);
            Random r1 = new Random();
            Random r2 = new Random();
            int x = 0;
            String[] sensores = {"Sensor A", "Sensor B", "Sensor C", "Sensor D", "Sensor E", "Sensor F", "Sensor G", "Sensor H", "Sensor I", "Sensor J"};
            
            while(true)
            {
                x = r1.Next(100);
                Thread.Sleep(500);
                //Acquire a write lock on the resource.
                rwl.AcquireWriterLock(Timeout.Infinite);
                try
                {
                    obj.numInt = x;
                    obj.cadena = sensores[r2.Next(9)];
                    Console.WriteLine("HILO 2: " + obj.cadena + " " + x);
                }
                finally
                {
                    //Release the lock.
                    rwl.ReleaseWriterLock();
                }

                
            }
            //return 0;
        }
    }
}
