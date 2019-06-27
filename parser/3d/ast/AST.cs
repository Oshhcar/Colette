using Compilador.parser._3d.ast.entorno;
using Compilador.parser._3d.ast.instrucion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.parser._3d.ast
{
    class AST
    {

        public AST(LinkedList<Instruccion> instrucciones)
        {
            Instrucciones = instrucciones;
        }

        public LinkedList<Instruccion> Instrucciones { get; set; }

        public void ejecutar()
        {
            Entorno global = new Entorno();

            for (int i = 0; i < Instrucciones.Count(); i++)
            {
                Instruccion instruccion = Instrucciones.ElementAt(i);

                if (instruccion is Label)
                {
                    Label label = (Label)instruccion;
                    label.I = i;
                    label.Ejecutar(global);
                }
                else if (instruccion is Metodo)
                {
                    Metodo metodo = (Metodo)instruccion;
                    LinkedList<Instruccion> bloques = metodo.Ejecutar(global) as LinkedList<Instruccion>;
                    /*comprobar null*/
                    for (int j = 0; j < bloques.Count(); j++)
                    {
                        Instruccion bloque = bloques.ElementAt(j);
                        if (bloque is Label)
                        {
                            Label labelJ = (Label)bloque;
                            labelJ.I = j;
                            labelJ.Ejecutar(global);
                        }
                    }
                }
            }

            
            
            for (int i = 0; i < Instrucciones.Count(); i++)
            {
                Instruccion instruccion = Instrucciones.ElementAt(i);

                if (!(instruccion is Label) && !(instruccion is Metodo))
                {
                    if (!(instruccion is Salto) && !(instruccion is SaltoCond))
                    {
                        instruccion.Ejecutar(global);
                    }
                    else
                    {
                        Object o = instruccion.Ejecutar(global);
                        if (o != null)
                        {
                            i = Convert.ToInt32(o.ToString());
                        }
                    }
                }
            }
            Console.WriteLine("\n");
            //global.Recorrer();
            //Console.WriteLine("\n");
        }
    }
}
