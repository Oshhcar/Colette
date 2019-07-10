using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compilador.parser.Colette.ast.entorno;
using Compilador.parser.Collete;
using Irony;

namespace Compilador.parser.Colette.ast.instruccion
{
    class Import : Instruccion
    {
        public Import(string direccion, int linea, int columna) : base(linea, columna)
        {
            Direccion = direccion;
            Sentencias = null;
        }

        public string Direccion { get; set; }
        public string DirActual { get; set; }
        public LinkedList<Nodo> Sentencias { get; set; }

        public override Result GetC3D(Ent e, bool funcion, bool ciclo, bool isDeclaracion, bool isObjeto, LinkedList<Error> errores)
        {

            string archivo = DirActual + Direccion.Substring(1, Direccion.Length - 2);

            archivo = archivo.Replace("\\\\", "\\");
            archivo = archivo.Replace("/","");

            if (Path.HasExtension(archivo))
            {
                if (Path.GetExtension(archivo).ToLower().Equals(".colette"))
                {
                    StreamReader reader = null;

                    try
                    {
                        reader = new StreamReader(archivo);
                        string contenido = reader.ReadToEnd();

                        if (contenido != null)
                        {
                            if (!contenido.Equals(string.Empty))
                            {
                                AnalizadorColette analizador = new AnalizadorColette();

                                if (analizador.AnalizarEntrada(contenido))
                                {
                                    Arbol arbol = (Arbol)analizador.GenerarArbol(analizador.Raiz.Root);

                                    if (arbol != null)
                                    {
                                        Sentencias = new LinkedList<Nodo>();
                                        LinkedList<Import> imports = new LinkedList<Import>();

                                        foreach (Nodo n in arbol.Sentencias)
                                        {
                                            if (n is Import import)
                                            {
                                                import.DirActual = DirActual;
                                                import.GetC3D(e, false, false, false, false, errores);
                                                imports.AddLast(import);
                                            }
                                            else
                                                Sentencias.AddLast(n);
                                        }

                                        foreach (Import import in imports)
                                        {
                                            if (import.Sentencias != null)
                                            {
                                                foreach (Nodo n in import.Sentencias)
                                                {
                                                    Sentencias.AddLast(n);
                                                }
                                            }
                                        }
                                    }
                                }

                                for (int i = 0; i < analizador.Raiz.ParserMessages.Count(); i++)
                                {
                                    LogMessage m = analizador.Raiz.ParserMessages.ElementAt(i);
                                    if (m.Message.ToString().Contains("character"))
                                        errores.AddLast(new Error("Léxico", m.Message.Replace("Invalid character", "Carácter inválido"), (m.Location.Line + 1), (m.Location.Column + 1)));
                                    else
                                        errores.AddLast(new Error("Sintáctico", m.Message.Replace("Syntax error, expected:", "Error de sintáxis, se esperaba:"), (m.Location.Line + 1), (m.Location.Column + 1)));

                                }

                                return null;
                                
                            }
                        }
                        errores.AddLast(new Error("Semántico", "Ocurrió un error leyendo el archivo " + archivo + ".", Linea, Columna));
                    }
                    catch (Exception)
                    {
                        errores.AddLast(new Error("Semántico", "Ocurrió un error abriendo el archivo " + archivo + ".", Linea, Columna));

                    }
                }
                else
                {
                    errores.AddLast(new Error("Semántico", "La extensión del archivo debe ser .colette " + archivo + ".", Linea, Columna));

                }
            }
            else
            {
                errores.AddLast(new Error("Semántico", "No es un archivo lo que se desea importar" + archivo + ".", Linea, Columna));
            }

            return null;
        }
    }
}
