using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compilador.parser._3d
{
    class Optimizador
    {
        public Optimizador(string contenido)
        {
            Contenido = contenido;
            Optimizado = new LinkedList<Optimizado>();
            OptimizarTodo = false;
        }

        public string Contenido { get; set; }
        public LinkedList<Optimizado> Optimizado { get; set; }
        public bool OptimizarTodo { get; set; }

        public string Optimizar()
        {
            string codigo = PrimeraPasada(Contenido);
            if (OptimizarTodo)
            {
                codigo = TerceraPasada(codigo);
                codigo = SegundaPasada(codigo);
            }
            return codigo;
        }

        private string PrimeraPasada(string entrada)
        {
            string codigo = "";
            Regex num = new Regex("^[0-9]+$");

            string[] lineas = entrada.Split('\n');
            int i = 2;
            int j = 1;

            string anterior = "";
            int tipAnt = 0;

            foreach (string l in lineas)
            {
                string linea = l;

                if (l.Contains("var"))
                {
                    /*Declaracion*/
                    tipAnt = 1;
                }
                else if (l.Contains("=")) //Asignación
                {
                    if (l.Contains("+"))
                    {
                        string[] op = l.Split('=');
                        string opIz = op[0].Replace(" ", "");

                        string[] opDer = op[1].Split('+');
                        string op1 = opDer[0].Replace(" ", "");
                        string op2 = opDer[1].Split(';')[0].Replace(" ", "");

                        if (op1.Equals("0"))
                        {
                            if (opIz.Equals(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 8));
                            }
                            else
                            {
                                linea = opIz + " = " + op2 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 12));
                            }
                        }
                        else if (op2.Equals("0"))
                        {
                            if (opIz.Equals(op1))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 8));
                            }
                            else
                            {
                                linea = opIz + " = " + op1 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 12));
                            }
                        }

                        tipAnt = 2;
                    }
                    else if (l.Contains("-"))
                    {
                        string[] op = l.Split('=');
                        string opIz = op[0].Replace(" ", "");

                        string[] opDer = op[1].Split('-');
                        string op1 = opDer[0].Replace(" ", "");
                        string op2 = opDer[1].Split(';')[0].Replace(" ", "");

                        /*if (op1.Equals("0"))
                        {
                            if (opIz.Equals(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 9));
                            }
                            else
                            {
                                linea = opIz + " = " + op2 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 13));
                            }
                        }
                        else */
                        if (op2.Equals("0"))
                        {
                            if (opIz.Equals(op1))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 9));
                            }
                            else
                            {
                                linea = opIz + " = " + op1 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 13));
                            }
                        }
                        tipAnt = 2;
                    }
                    else if (l.Contains("*"))
                    {
                        string[] op = l.Split('=');
                        string opIz = op[0].Replace(" ", "");

                        string[] opDer = op[1].Split('*');
                        string op1 = opDer[0].Replace(" ", "");
                        string op2 = opDer[1].Split(';')[0].Replace(" ", "");

                        if (op1.Equals("1"))
                        {
                            if (opIz.Equals(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 10));
                            }
                            else
                            {
                                linea = opIz + " = " + op2 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 14));
                            }
                        }
                        else if (op2.Equals("1"))
                        {
                            if (opIz.Equals(op1))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 10));
                            }
                            else
                            {
                                linea = opIz + " = " + op1 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 14));
                            }
                        }
                        else if (op1.Equals("2"))
                        {
                            linea = opIz + " = " + op2 + " + " + op2 + ";";
                            Optimizado.AddLast(new Optimizado(j, i, 16));
                        }
                        else if (op2.Equals("2"))
                        {
                            linea = opIz + " = " + op1 + " + " + op1 + ";";
                            Optimizado.AddLast(new Optimizado(j, i, 16));
                        }
                        else if (op1.Equals("0"))
                        {
                            linea = opIz + " = 0;";
                            Optimizado.AddLast(new Optimizado(j, i, 17));
                        }
                        else if (op2.Equals("0"))
                        {
                            linea = opIz + " = 0;";
                            Optimizado.AddLast(new Optimizado(j, i, 17));
                        }

                        tipAnt = 2;
                    }
                    else if (l.Contains("/"))
                    {
                        string[] op = l.Split('=');
                        string opIz = op[0].Replace(" ", "");

                        string[] opDer = op[1].Split('/');
                        string op1 = opDer[0].Replace(" ", "");
                        string op2 = opDer[1].Split(';')[0].Replace(" ", "");

                        /*
                        if (op1.Equals("1"))
                        {
                            if (opIz.Equals(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 11));
                            }
                            else
                            {
                                linea = opIz + " = " + op2 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 15));
                            }
                        }
                        else */
                        if (op2.Equals("1"))
                        {
                            if (opIz.Equals(op1))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, i, 11));
                            }
                            else
                            {
                                linea = opIz + " = " + op1 + ";";
                                Optimizado.AddLast(new Optimizado(j, i, 15));
                            }
                        }
                        else if (op1.Equals("0"))
                        {
                            linea = opIz + " = 0;";
                            Optimizado.AddLast(new Optimizado(j, i, 18));
                        }

                        tipAnt = 2;
                    }
                    else if (l.Contains("%"))
                    {
                        tipAnt = 2;
                    }
                    else
                    {
                        if (!anterior.Equals(""))
                        {
                            if (tipAnt == 3)
                            {
                                string[] op = l.Split('=');
                                string op1 = op[0].Replace(" ", "");
                                string op2 = op[1].Split(';')[0].Replace(" ", "");

                                string[] opAnt = anterior.Split('=');
                                string op1Ant = opAnt[0].Replace(" ", "");
                                string op2Ant = opAnt[1].Split(';')[0].Replace(" ", "");

                                if (op1.Equals(op2Ant))
                                {
                                    if (op2.Equals(op1Ant))
                                    {
                                        if (!num.IsMatch(op1) && !num.IsMatch(op2))
                                        {
                                            anterior = l;
                                            linea = "";
                                            Optimizado.AddLast(new Optimizado(j, i, 1));
                                        }
                                    }
                                }

                            }
                        }
                        tipAnt = 3;
                    }
                }
                else
                {
                    //Otar sentencia;
                    tipAnt = 4;
                }

                if (anterior != "")
                {
                    codigo += anterior + "\n";
                    i++;
                }
                j++;
                anterior = linea;
            }

            if (anterior != "")
                codigo += anterior + "\n";

            return codigo;
        }
        private string SegundaPasada(string entrada)
        {
            string codigo = "";

            string[] lineas = entrada.Split('\n');
            int i = 1;
            int j = 1;
            int lineasMenos = 0;
            int contador = 0;

            string etqSalto = "";
            string codEntreEtq = "";

            foreach (string l in lineas)
            {
                if (l.Contains("if"))
                {

                    /*If*/
                }
                else if (l.Contains("ifFalse"))
                {

                }
                else if (l.Contains("goto"))
                {
                    string etq = l.Replace("goto", "");
                    etq = etq.Replace(";", "");
                    etq = etq.Trim();

                    if (etqSalto == "")
                    {
                        contador = 0;
                        etqSalto = etq;
                        i = j;
                    }

                }
                else if (l.Contains(":"))
                {
                    string etq = l.Replace(":", "");
                    etq = etq.Trim();

                    if (etqSalto != "")
                    {
                        if (etq.Equals(etqSalto))
                        {
                            etqSalto = "";
                            codEntreEtq = "";
                            Optimizado.AddLast(new Optimizado(j, i-lineasMenos, 2));
                            lineasMenos += contador;
                            contador = 0;
                        }
                        else
                        {
                            etqSalto = "";
                            codigo += codEntreEtq;
                            codEntreEtq = "";
                            contador = 0;
                        }
                    }
                }
                else
                {
                    /*Otra cosa*/
                }

                if (etqSalto == "")
                {
                    codigo += l + "\n";
                }
                else
                {
                    codEntreEtq += l + "\n";
                    contador++;
                }

                j++;
            }

            return codigo;
        }
        private string TerceraPasada(string entrada)
        {
            string codigo = "";
            Regex num = new Regex("^[0-9]+$");

            string[] lineas = entrada.Split('\n');
            int i = 0;
            int j = 1;

            bool delSalto = false;
            string lineaAnt = "";
            bool antSalto = false;

            foreach (string l in lineas)
            {
                string linea = l;

                if (l.Contains("ifFalse"))
                {
                    delSalto = false;
                    antSalto = false;

                    string cond = l.Replace("ifFalse", "");
                    string salto = l.Substring(l.LastIndexOf("goto"));
                    salto = salto.Replace(";", "");
                    salto = salto.Trim();

                    cond = cond.Split(')')[0].Replace("(", "");
                    cond = cond.Replace(";", "");
                    cond = cond.Trim();

                    string op1;
                    string op2;

                    if (cond.Contains("=="))
                    {
                        op1 = cond.Remove(cond.IndexOf("=="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("=="));
                        op2 = op2.Replace("==", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) == Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                    else if (cond.Contains("!="))
                    {
                        op1 = cond.Remove(cond.IndexOf("!="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("!="));
                        op2 = op2.Replace("!=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) != Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                    else if (cond.Contains(">"))
                    {
                        op1 = cond.Remove(cond.IndexOf(">"));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf(">"));
                        op2 = op2.Replace(">", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) > Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                    else if (cond.Contains("<"))
                    {
                        op1 = cond.Remove(cond.IndexOf("<"));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("<"));
                        op2 = op2.Replace("<", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) < Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                    else if (cond.Contains(">="))
                    {
                        op1 = cond.Remove(cond.IndexOf(">="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf(">="));
                        op2 = op2.Replace(">=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) >= Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                    else if (cond.Contains("<="))
                    {
                        op1 = cond.Remove(cond.IndexOf("<="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("<="));
                        op2 = op2.Replace("<=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) <= Convert.ToInt32(op2))
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                            else
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                        }
                    }
                }
                else if (l.Contains("if"))
                {
                    delSalto = false;
                    antSalto = false;

                    string cond = l.Replace("if", "");
                    string salto = l.Substring(l.LastIndexOf("goto"));
                    salto = salto.Replace(";", "");
                    salto = salto.Trim();

                    cond = cond.Split(')')[0].Replace("(", "");
                    cond = cond.Replace(";", "");
                    cond = cond.Trim();

                    string op1;
                    string op2;

                    if (cond.Contains("=="))
                    {
                        op1 = cond.Remove(cond.IndexOf("=="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("=="));
                        op2 = op2.Replace("==", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) == Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                    else if (cond.Contains("!="))
                    {
                        op1 = cond.Remove(cond.IndexOf("!="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("!="));
                        op2 = op2.Replace("!=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) != Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                    else if (cond.Contains(">"))
                    {
                        op1 = cond.Remove(cond.IndexOf(">"));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf(">"));
                        op2 = op2.Replace(">", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) > Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                    else if (cond.Contains("<"))
                    {
                        op1 = cond.Remove(cond.IndexOf("<"));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("<"));
                        op2 = op2.Replace("<", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) < Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                    else if (cond.Contains(">="))
                    {
                        op1 = cond.Remove(cond.IndexOf(">="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf(">="));
                        op2 = op2.Replace(">=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) >= Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                    else if (cond.Contains("<="))
                    {
                        op1 = cond.Remove(cond.IndexOf("<="));
                        op1 = op1.Trim();

                        op2 = cond.Substring(cond.IndexOf("<="));
                        op2 = op2.Replace("<=", "");
                        op2 = op2.Trim();

                        if (num.IsMatch(op1) && num.IsMatch(op2))
                        {
                            if (Convert.ToInt32(op1) <= Convert.ToInt32(op2))
                            {
                                linea = salto + ";";
                                delSalto = true;
                                Optimizado.AddLast(new Optimizado(j, j - i, 4));
                            }
                            else
                            {
                                linea = "";
                                Optimizado.AddLast(new Optimizado(j, j - i, 5));
                                i++;
                            }
                        }
                    }
                }
                else if (l.Contains("goto"))
                {
                    if (delSalto)
                    {
                        i++;
                        linea = "";
                        antSalto = false;
                    }
                    else //Eliminar saltos innecesarios
                    {
                        if (antSalto)
                        {
                            Optimizado.AddLast(new Optimizado(j, j - i, 6));
                            i++;
                            linea = "";
                            antSalto = false;
                        }
                        else
                        {
                            antSalto = true;
                        }
                    }

                }
                else if (l.Contains(":"))
                {
                    delSalto = false;
                    antSalto = false;
                }
                else
                {
                    delSalto = false;
                    antSalto = false;
                }

                if (lineaAnt != "")
                    codigo += lineaAnt + "\n";
                j++;
                lineaAnt = linea;
            }

            if (lineaAnt != "")
                codigo += lineaAnt + "\n";

            return codigo;
        }

    }
}
