using System;
using System.IO;
using System.Collections.Generic;

namespace Laboratorio4.Models
{
    public class Archivo
    {
        private Dictionary<string, int> TablaCaracteres { get; set; }
        private List<string> ListaTexto { get; set; } 
        public const int TamañoBuffer = 1040;
        

        public Archivo()
        {
            TablaCaracteres = new Dictionary<string, int>();
            ListaTexto = new List<string>();
        }


        public void LeerArchivoTexto(string Name)
        { 
            var LineaLeida = "";
            var i = 0;
            using(var sr = new StreamReader(Name))
            {
                //Mandar bloques de texto
                LineaLeida = sr.ReadLine();
                while( LineaLeida != null)
                {
                    ListaTexto.Add(LineaLeida);
                    LineaLeida = sr.ReadLine();
                    i++;
                }
            }
        }
        public void ObtenerCaracteresBase()
        {   var i = 0;
            var num = 1;
            foreach (var item in ListaTexto)
            {
                while(i < item.Length)
                {
                    if (TablaCaracteres.ContainsKey(item[i].ToString()) == false)
                    {
                        TablaCaracteres.Add(item[i].ToString(), num);
                        num++;
                    }
                    i++;
                }
                i = 0;
            }
        }

        public void AgregandoNuevasCombinaciones()
        {
            var Palabra = "";
            var i = 0;
            var numero = TablaCaracteres.Count;

            foreach (var item in ListaTexto)
            {
                while (i < item.Length)
                {
                    if (TablaCaracteres.ContainsKey(item[i].ToString()) == true)
                    {
                        Palabra = item[i].ToString();
                        while (TablaCaracteres.ContainsKey(Palabra) == true)
                        {
                           
                            numero++;
                            i++;
                            Palabra += item[i];
                        }
                        TablaCaracteres.Add(Palabra, numero);
                    }
                }
                i = 0;
            }

        }
    }
}
