using System;
using System.IO;
using System.Collections.Generic;

namespace Laboratorio4.Models
{
    public class Archivo
    {
        private Dictionary<char, int> TablaCaracteres { get; set; }
        private List<string> ListaTexto { get; set; } 
        public const int TamañoBuffer = 1040;
        

        public Archivo()
        {
            TablaCaracteres = new Dictionary<char, int>();
            ListaTexto = new List<string>();
        }


        public void LeerArchivoTexto(FileStream fileStream)
        {
            char[] Buffer = new char[TamañoBuffer];
            var LineaLeida = "";
            var i = 0;
            using(var sr = new StreamReader("//Users//eber.g//Downloads//PRUEBAS//Prue.txt"))
            {
                //Mandar bloques de texto
                LineaLeida = sr.ReadLine();
                while( LineaLeida != null)
                {
                    LineaLeida=(sr.ReadBlock(Buffer, i, TamañoBuffer)).ToString();
                    i += TamañoBuffer;
                    LineaLeida = sr.ReadLine();
                }
               
            }
        }
        public void ObtenerCaracteresBase(string Characteres)
        {

        }
    }
}
