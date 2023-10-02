using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SimulacionCarrera
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public class Algoritmos
        {
            public static bool Secuencial(int[] arreglo, int objetivo)
            {
                for (int i = 0; i < arreglo.Length; i++)
                {
                    if (arreglo[i] == objetivo)
                        return true; // Si se encuentra el numero, retorna verdadero
                }
                return false; // Si termina el bucle sin encontrar el objetivo, retorna falso
            }

            public static bool Binaria(int[] arreglo, int objetivo)
            {
                int izquierda = 0;
                int derecha = arreglo.Length - 1;

                while (izquierda <= derecha)
                {
                    int medio = (izquierda + derecha) / 2;

                    if (arreglo[medio] == objetivo)
                        return true; // Si encuentra el objetivo, retorna verdadero

                    if (arreglo[medio] < objetivo)
                        izquierda = medio + 1; // Si el elemento en el medio es menor que el objetivo, actualiza la izquierda
                    else
                        derecha = medio - 1; // Si el elemento en el medio es mayor que el objetivo, actualiza la derecha
                }

                return false; // Si termina el bucle sin encontrar el objetivo, retorna falso
            }

            public static void Burbuja(int[] arreglo)
            {
                int n = arreglo.Length;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (arreglo[j] > arreglo[j + 1])
                        {
                            int temp = arreglo[j];
                            arreglo[j] = arreglo[j + 1];
                            arreglo[j + 1] = temp;
                        }
                    }
                }
            }
            


            public static void QuickSort(int[] arreglo, int izquierda, int derecha)
            {
                if (izquierda < derecha)
                {
                    int particion = Particionar(arreglo, izquierda, derecha);

                    QuickSort(arreglo, izquierda, particion - 1);
                    QuickSort(arreglo, particion + 1, derecha);
                }
            }

            private static int Particionar(int[] arreglo, int izquierda, int derecha)
            {
                int pivote = arreglo[derecha];
                int i = (izquierda - 1);

                for (int j = izquierda; j < derecha; j++)
                {
                    if (arreglo[j] <= pivote)
                    {
                        i++;

                        int temp = arreglo[i];
                        arreglo[i] = arreglo[j];
                        arreglo[j] = temp;
                    }
                }

                int temp1 = arreglo[i + 1];
                arreglo[i + 1] = arreglo[derecha];
                arreglo[derecha] = temp1;

                return i + 1;
            }

            public static void Insercion(int[] arreglo)
            {
                int n = arreglo.Length;
                for (int i = 1; i < n; ++i)
                {
                    int clave = arreglo[i];
                    int j = i - 1;

                    while (j >= 0 && arreglo[j] > clave)
                    {
                        arreglo[j + 1] = arreglo[j];
                        j = j - 1;
                    }

                    arreglo[j + 1] = clave;
                }
            }

        }

        private int[] ObtenerArregloDesdeTextBox()
        {
            string[] valoresTexto = textBoxArreglo.Text.Split(';');

            int[] arreglo = new int[valoresTexto.Length];
            for (int i = 0; i < valoresTexto.Length; i++)
            {
                if (int.TryParse(valoresTexto[i], out int valor))
                {
                    arreglo[i] = valor;
                }
                else
                {
                    MessageBox.Show("Debe ingresar los números separados por punto y comas(;).");
                    return null;
                }
            }

            return arreglo;
        }

        private void EjecutarBusquedaSecuencial(int[] arreglo, int objetivo, Stopwatch stopwatch)
        {
           
            List<long> tiempos = new List<long>();
            bool resultado = false; // Inicializar el resultado
            for (int i = 0; i < 500; i++) // Ejecutar 500 veces

            {
                stopwatch.Restart();
                resultado = Algoritmos.Secuencial(arreglo, objetivo);
                stopwatch.Stop();
                tiempos.Add(stopwatch.ElapsedTicks);
            }

            
            long tiempoPromedio = (long)tiempos.Average();
            MostrarResultado(textBoxSecuencial, resultado);
            MostrarTiempo(label8, tiempoPromedio);
            
        }

        private void EjecutarBusquedaBinaria(int[] arreglo, int objetivo, Stopwatch stopwatch)
        {
            List<long> tiempos = new List<long>();
            bool resultado = false; // Inicializar el resultado
            for (int i = 0; i < 500; i++) // Ejecutar 500 veces
            {
                stopwatch.Restart();
                resultado = Algoritmos.Binaria(arreglo, objetivo);
                stopwatch.Stop();
                tiempos.Add(stopwatch.ElapsedTicks);
            }
            long tiempoPromedio = (long)tiempos.Average();
            MostrarResultado(textBoxBinaria, resultado);
            MostrarTiempo(label9, tiempoPromedio);
            
        }

        private void EjecutarBurbuja(int[] arreglo, Stopwatch stopwatch)
        {
            string ret = "";
            List<long> tiempos = new List<long>();
            for (int i = 0; i < 500; i++) // Ejecutar 500 veces
            {
                int[] copiaBurbuja = (int[])arreglo.Clone(); // Crear una copia del arreglo para no modificar el original
                stopwatch.Restart();
                Algoritmos.Burbuja(copiaBurbuja);
                stopwatch.Stop();
                tiempos.Add(stopwatch.ElapsedTicks);
                
            }
            int t;
            for (int a = 1; a < arreglo.Length; a++)
                for (int b = arreglo.Length - 1; b >= a; b--)
                {
                    if (arreglo[b - 1] > arreglo[b])
                    {
                        t = arreglo[b - 1];
                        arreglo[b - 1] = arreglo[b];
                        arreglo[b] = t;
                    }
                }
            for (int f = 0; f < arreglo.Length; f++)
             {
                ret +=";"+arreglo[f].ToString();
             }
           


            long tiempoPromedio = (long)tiempos.Average();
            MostrarMensaje(textBoxBurbuja, " Arreglo Ordenado en forma ascendente\n" + "\n"+ ret);
            MostrarTiempo(label10, tiempoPromedio);
        }

        private void EjecutarQuickSort(int[] arreglo, Stopwatch stopwatch)
        {
            string rep = "";
            List<long> tiempos = new List<long>();
            for (int i = 0; i < 500; i++) // Ejecutar 500 veces
            {
                int[] copiaQuickSort = (int[])arreglo.Clone();
                stopwatch.Restart();
                Algoritmos.QuickSort(copiaQuickSort, 0, copiaQuickSort.Length - 1);
                stopwatch.Stop();
                tiempos.Add(stopwatch.ElapsedTicks);
            }
           
           Algoritmos.QuickSort(arreglo, 0, arreglo.Length-1);
            for (int f = 0; f < arreglo.Length; f++)
            {
                rep += ";" + arreglo[f].ToString();
            }

            long tiempoPromedio = (long)tiempos.Average();
            MostrarMensaje(textBoxQuickSort, "Arreglo Ordenado  \n" + "\n" + rep);
            MostrarTiempo(label11, tiempoPromedio);
        }

        private void EjecutarInsercion(int[] arreglo, Stopwatch stopwatch)
        {
            string rey= "";
            List<long> tiempo = new List<long>();
            for (int i = 0; i < 500; i++) // Ejecutar 500 veces
            {
                int[] copiaInsercion = (int[])arreglo.Clone();
                stopwatch.Restart();
                Algoritmos.Insercion(copiaInsercion);
                stopwatch.Stop();
                tiempo.Add(stopwatch.ElapsedTicks);
            }
            int auxili;
            int j;
            for (int a = 0; a < arreglo.Length; a++)
            {
                auxili = arreglo[a];
                j = a - 1;
                while (j >= 0 && arreglo[j] > auxili)
                {
                    arreglo[j + 1] = arreglo[j];
                    j--;
                }
                arreglo[j + 1] = auxili;
            }
            for (int f = 0; f < arreglo.Length; f++)
            {
                rey += ";" + arreglo[f].ToString();
            }

            long tiempoPromedio = (long)tiempo.Average();
            MostrarMensaje(textBoxInsercion, "Arreglo Ordenado  \n" + "\n" + rey);
            MostrarTiempo(label12, tiempoPromedio);
        }

        private void MostrarResultado(TextBox textBox, bool resultado )
        {
            textBox.Invoke((MethodInvoker)delegate {
                textBox.Text = resultado ? " Numero Encontrado" : "No se ha encontrado el numero";
            });
        }

        private void MostrarMensaje(TextBox textBox, string mensaje)
        {
            textBox.Invoke((MethodInvoker)delegate {
                textBox.Text = mensaje;
            });
        }

        private void MostrarTiempo(Label label, long tiempoTicks)
        {
            double tiempoSegundos = (double)tiempoTicks / Stopwatch.Frequency;
            label.Invoke((MethodInvoker)delegate {
                label.Text = $"{tiempoSegundos:F10} s"; // Mostrar tiempo en segundos con nueve decimales
            });
        }

       



        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            int[] arreglo = ObtenerArregloDesdeTextBox();

            if (arreglo != null)
            {
                int objetivo;
                if (int.TryParse(textBoxObjetivo.Text, out objetivo))
                {
                    Stopwatch stopwatch = new Stopwatch();

                    Thread busquedaSecuencialThread = new Thread(() => EjecutarBusquedaSecuencial(arreglo, objetivo, stopwatch));
                    Thread busquedaBinariaThread = new Thread(() => EjecutarBusquedaBinaria(arreglo, objetivo, stopwatch));
                    Thread burbujaThread = new Thread(() => EjecutarBurbuja(arreglo, stopwatch));
                    Thread quickSortThread = new Thread(() => EjecutarQuickSort(arreglo, stopwatch));
                    Thread insercionThread = new Thread(() => EjecutarInsercion(arreglo, stopwatch));

                    busquedaSecuencialThread.Start();
                    busquedaBinariaThread.Start();
                    burbujaThread.Start();
                    quickSortThread.Start();
                    insercionThread.Start();
                }
                else
                {
                    MessageBox.Show(" ingresa un número válido como objetivo.");
                }
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Limpiar cuadros de texto
            textBoxArreglo.Text = "";
            textBoxObjetivo.Text = "";

            // Limpiar labels
             textBoxSecuencial.Text = "";
            textBoxBinaria.Text = "";
            textBoxBurbuja.Text = "";
            textBoxInsercion.Text = "";
            textBoxQuickSort.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";

        }

        
    }
}
