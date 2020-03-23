using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Encodings;
using System.Text;

namespace Laboratorio4.Models
{
    public class Archivo
    {
        private Dictionary<string, int> TablaCaracteres { get; set; }
        private List<string> ListaTexto { get; set; } 
        private string TextoComprimir;

        public int TotalCaracteres;

        public Archivo()
        {
            TablaCaracteres = new Dictionary<string, int>();
            ListaTexto = new List<string>();
            TextoComprimir = "";
            TotalCaracteres = 0;
        }

        public void LeerArchivoTexto(string Name)
        { 
            var LineaLeida = "";
            var i = 0;
            using(var sr = new StreamReader(Name))
            {
                //Mandar bloques de texto
                LineaLeida = sr.ReadLine();
                TotalCaracteres += LineaLeida.Length;
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
            var TextoCompartir = "";
            var i = 0;
            var numero = TablaCaracteres.Count;
            bool FinLinea = false;

            foreach (var item in ListaTexto)
            {
                while (i < item.Length)
                {
                    if (TablaCaracteres.ContainsKey(item[i].ToString()) == true)
                    {
                        Palabra = item[i].ToString();
                        while (TablaCaracteres.ContainsKey(Palabra) == true && (FinLinea==false))
                        {
               
                            i++;
                            if (i < item.Length) { Palabra += item[i]; }
                            else {
                                FinLinea = true;
                                //Almacena la posicion del ultimo caracter de la linea leida
                                TextoCompartir += TablaCaracteres[item[i - 1].ToString()];
                            }
  
                        }
                        if(TablaCaracteres.ContainsKey(Palabra)==false){
                            numero++;
                            TablaCaracteres.Add(Palabra, numero);

                            //Almacena la posicion del caracter o cadena, que si se encuentra en el diccionario, para comprimir 
                            TextoCompartir += TablaCaracteres[Palabra.TrimEnd(item[i])].ToString()  + ",";
                            
                        }
                       
                    }
                    

                }
                FinLinea = false;
                i = 0;
            }

            TextoComprimir = TextoCompartir;
        }

        public void Comprimir()
        {
            //2^n
            var TamañoBite = n();
            var i = 0;
            var j = 0;
            var v = 0;
            string[] CaracteresSeparados = new string[1];
            string[] CaracteresBinario = new string[TextoComprimir.Length];
            char[] CaractersAscii = new char[TextoComprimir.Length];
            CaracteresSeparados = TextoComprimir.Split(',');
            

            System.Text.Encoding enEncondig = System.Text.ASCIIEncoding.ASCII;

            //Convertir a binario
            while(i < CaracteresSeparados.Length)
            {
                //FORMATO
                string p = Convert.ToString(Convert.ToInt32(CaracteresSeparados[i]), 2);
                CaracteresBinario[i] = p.ToString();
                while (CaracteresBinario[i].Length < TamañoBite)
                {

                    CaracteresBinario[i] = CaracteresBinario[i].Insert(0, "0");
                    
                }
                i++;
            }


            //Convertir a Decimal un numero de un Byte
            while (CaracteresBinario[j] != " ")
            {

                if(CaracteresBinario[j].Length == 7)
                {
                    string p1 = Convert.ToString(Convert.ToInt32(CaracteresBinario[j], 10));
                    char result = Convert.ToChar(Convert.ToInt32(p1));
                    CaractersAscii[j] = result;
                    j++;
                }
                else
                {
                    string p12 = "";
                    string p2 = "";
                    string p3 = CaracteresBinario[j].ToString();
                    j++;
                    string p4 = CaracteresBinario[j].ToString();

                    p2 = p3.Substring(v, 5-v);

                    if(p2.Length < 8)
                    {
                        v = 8 - p2.Length;
                        if(v < 5)
                        {
                            p2 += p4.Substring(0, v);
                        }
                        else
                        {
                            j++;
                            p2 += p4;
                            p4 = CaracteresBinario[j].ToString();
                            v = 8 - p2.Length;
                            p2 += p4.Substring(0, 8 - p2.Length);

                        }
                       
                        
                    }
                  
                    p12 = Convert.ToString(Convert.ToInt32(p2, 2));
                    UTF8Encoding utf8 = new UTF8Encoding();
                    Byte[] encodedBytes = utf8.GetBytes(p12);
                    String decodedString = utf8.GetString(encodedBytes);


                    CaractersAscii[j] = Convert.ToChar(decodedString);
                    
                }

              
            } 
            
            
            



        }



        public int  n()
        {
            bool nEncontrado = false;
            int Potencia = 0;
            int Valor = 2;
            while(nEncontrado == false)
            {
                if(Valor > TotalCaracteres)
                {
                    nEncontrado = true;
                }
                else
                {
                    Potencia++;
                    Valor = (int)(Math.Pow(2, Potencia));
                    
                }
            }

            return Potencia;
        }
    }
}
