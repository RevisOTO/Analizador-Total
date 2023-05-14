using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analizador_Sintactico
{
    public partial class Form1 : Form
    {
        List<Tuple<string, string>> PalabrasReservadas = new List<Tuple<string, string>>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Palabras reservadas
            PalabrasReservadas.Add(new Tuple<string, string>("readline", "PR1"));
            PalabrasReservadas.Add(new Tuple<string, string>("println", "PR2"));
            PalabrasReservadas.Add(new Tuple<string, string>("string", "PR3"));
            PalabrasReservadas.Add(new Tuple<string, string>("int", "PR4"));
            PalabrasReservadas.Add(new Tuple<string, string>("bool", "PR5"));
            PalabrasReservadas.Add(new Tuple<string, string>("double", "PR6"));
            PalabrasReservadas.Add(new Tuple<string, string>("continue", "PR7"));
            PalabrasReservadas.Add(new Tuple<string, string>("char", "PR15"));

            //If y Loops
            PalabrasReservadas.Add(new Tuple<string, string>("for", "PR8"));
            PalabrasReservadas.Add(new Tuple<string, string>("while", "PR9"));
            PalabrasReservadas.Add(new Tuple<string, string>("do", "PR10"));
            PalabrasReservadas.Add(new Tuple<string, string>("if", "PR11"));
            PalabrasReservadas.Add(new Tuple<string, string>("else", "PR12"));

            //Otros
            PalabrasReservadas.Add(new Tuple<string, string>("begin", "PR13"));
            PalabrasReservadas.Add(new Tuple<string, string>("end", "PR14"));

            //Identificadores
            PalabrasReservadas.Add(new Tuple<string, string>("iden", "IDEN"));

            //Operadores
            PalabrasReservadas.Add(new Tuple<string, string>("+", "OPER1"));
            PalabrasReservadas.Add(new Tuple<string, string>("-", "OPER2"));
            PalabrasReservadas.Add(new Tuple<string, string>("/", "OPER3"));
            PalabrasReservadas.Add(new Tuple<string, string>("*", "OPER4"));
            PalabrasReservadas.Add(new Tuple<string, string>("^", "OPER5"));

            //Operadores Relacionales
            PalabrasReservadas.Add(new Tuple<string, string>(">", "COMP1"));
            PalabrasReservadas.Add(new Tuple<string, string>("<", "COMP2"));
            PalabrasReservadas.Add(new Tuple<string, string>(">=", "COMP3"));
            PalabrasReservadas.Add(new Tuple<string, string>("<=", "COMP4"));
            PalabrasReservadas.Add(new Tuple<string, string>("<>", "COMP5"));
            PalabrasReservadas.Add(new Tuple<string, string>("==", "COMP6"));

            //Operadores Logicos
            PalabrasReservadas.Add(new Tuple<string, string>("&&", "LOG1"));
            PalabrasReservadas.Add(new Tuple<string, string>("||", "LOG2"));
            PalabrasReservadas.Add(new Tuple<string, string>("!", "LOG3"));

            //Caracteres especiales
            PalabrasReservadas.Add(new Tuple<string, string>("@", "CE01"));
            PalabrasReservadas.Add(new Tuple<string, string>("$", "CE02"));
            PalabrasReservadas.Add(new Tuple<string, string>("#", "CE03"));
            PalabrasReservadas.Add(new Tuple<string, string>("(", "CE04"));
            PalabrasReservadas.Add(new Tuple<string, string>(")", "CE05"));
            PalabrasReservadas.Add(new Tuple<string, string>("{", "CE06"));
            PalabrasReservadas.Add(new Tuple<string, string>("}", "CE07"));

            PalabrasReservadas.Add(new Tuple<string, string>("=", "ASIGN01"));

            //Cometario
            PalabrasReservadas.Add(new Tuple<string, string>("Comentario", "COM"));
        }
        int linea;
        String[] type = { "int", "string", "bool", "char", "double" };
        String[] CE = { "+", "-", "/", "*", "^", "(", "=" };
        String[] COMP = { "==", ">", "<", "<>", "<=", ">=" };
        String[] LOG = { "||", "&&" };
        String[] ANI = { "if", "for", "do", "while", "else" };


        List<Tuple<string, string>> IDES = new List<Tuple<string, string>>();
        List<Tuple<string, string>> ContTuple = new List<Tuple<string, string>>();
        private void Lexico()
        {
            ContTuple = new List<Tuple<string, string>>();
            int ContE = 0;
            int ContR = 0;
            int ContC = 0;
            int ContS = 0;
            //Limpia identificadores
            IDES.Clear();
            //Limpia el texto del lexico
            rtxtLexico.Clear();
            string lastWord = "";
            int ide = 0;
            string PR = "";
            int countWord = -1;
            foreach (var line in rtxtCodigo.Lines)
            {
                if (line == "")
                {
                    continue;
                }
                else if (line[0] == '/')
                {
                    rtxtLexico.AppendText("COM \n");
                    continue;
                }
                foreach (var word in line.Split())
                {
                    countWord++;
                    //Reasignacion
                    if (IDES.Any(m => m.Item1 == word))
                    {
                        string find = IDES.FirstOrDefault(m => m.Item1 == word).Item2;
                        if (word != line.Split()[line.Split().Length - 1])
                        {
                            rtxtLexico.AppendText(find + " ");
                        }
                        else
                        {
                            rtxtLexico.AppendText(find);
                        }
                    }
                    //Asignacion
                    else if (PalabrasReservadas.Any(m => m.Item1 == word))
                    {
                        PR = PalabrasReservadas.FirstOrDefault(m => m.Item1 == word).Item2;
                        if (PR != line.Split()[line.Split().Length - 1])
                        {
                            rtxtLexico.AppendText(PR + " ");
                        }
                        else
                        {
                            rtxtLexico.AppendText(PR);
                        }
                    }
                    else
                    {
                        //Identificadores
                        if (Array.Exists(type, element => element == lastWord))
                        {
                            //Revisa
                            if (!char.IsLetter(word[0]))
                            {
                                if (word != line.Split()[line.Split().Length - 1])
                                {
                                    rtxtLexico.AppendText("ErrorIDE ");
                                }
                                else
                                {
                                    rtxtLexico.AppendText("ErrorIDE");
                                }
                            }
                            else
                            {
                                ide++;
                                rtxtLexico.AppendText("IDEN" + ide + " ");
                                IDES.Add(new Tuple<string, string>(line.Split()[1], "IDEN" + ide));
                                lastWord = word;
                                continue;
                            }
                        }
                        if (Array.Exists(CE, element => element == lastWord) || Array.Exists(COMP, element => element == lastWord))
                        {
                            if (word != line.Split()[line.Split().Length - 1])
                            {
                                if (ContTuple.Any(m => m.Item2.ToString() == word))
                                {
                                    rtxtLexico.AppendText(ContTuple.FirstOrDefault(m => m.Item2.ToString() == word).Item1+" ");
                                }
                                else if (Semantica("int", word, false) == "int")
                                {
                                    ContE++;
                                    ContTuple.Add(new Tuple<string, string>($"CNE{ContE}",word));
                                    rtxtLexico.AppendText($"CNE{ContE} ");
                                }
                                else if (Semantica("double", word, false) == "double")
                                {
                                    ContR++;
                                    ContTuple.Add(new Tuple<string, string>($"CNR{ContR}", word));
                                    rtxtLexico.AppendText($"CNR{ContR} ");
                                }
                                else if (Semantica("char", word, false) == "char")
                                {
                                    ContC++;
                                    ContTuple.Add(new Tuple<string, string>($"CHAR{ContC}", word));
                                    rtxtLexico.AppendText($"CHAR{ContC} ");
                                }
                                else if (Semantica("bool", word, false) == "bool")
                                {
                                    rtxtLexico.AppendText("BOOL ");
                                }
                                else
                                {
                                    ContS++;
                                    ContTuple.Add(new Tuple<string, string>($"STR{ContS}", word));
                                    rtxtLexico.AppendText($"STR{ContS} ");
                                }
                            }
                            else
                            {
                                if (ContTuple.Any(m => m.Item2.ToString() == word))
                                {
                                    rtxtLexico.AppendText(ContTuple.FirstOrDefault(m => m.Item2.ToString() == word).Item1);
                                }
                                else if (Semantica("int", word, false) == "int")
                                {
                                    ContE++;
                                    ContTuple.Add(new Tuple<string, string>($"CNE{ContE}", word));
                                    rtxtLexico.AppendText($"CNE{ContE}");
                                }
                                else if (Semantica("double", word, false) == "double")
                                {
                                    ContR++;
                                    ContTuple.Add(new Tuple<string, string>($"CNR{ContR}", word));
                                    rtxtLexico.AppendText($"CNR{ContR}");
                                }
                                else if (Semantica("char", word, false) == "char")
                                {
                                    ContC++;
                                    ContTuple.Add(new Tuple<string, string>($"CHAR{ContC}", word));
                                    rtxtLexico.AppendText($"CHAR{ContC}");
                                }
                                else if (Semantica("bool", word, false) == "bool")
                                {
                                    rtxtLexico.AppendText("BOOL");
                                }
                                else
                                {
                                    ContS++;
                                    ContTuple.Add(new Tuple<string, string>($"STR{ContS}", word));
                                    rtxtLexico.AppendText($"STR{ContS}");
                                }
                            }
                        }
                        else if(word == "}while")
                        {
                            rtxtLexico.AppendText("PR09 CE07 ");
                        }
                        else
                        {
                            if (word != line.Split()[line.Split().Length - 1])
                            {
                                rtxtLexico.AppendText("Error ");
                            }
                            else
                            {
                                rtxtLexico.AppendText("Error");
                            }
                        }
                    }
                    lastWord = word;
                }
                countWord = -1;
                if (line != rtxtCodigo.Lines[rtxtCodigo.Lines.Length - 1] || line.Length == 1)
                {
                    rtxtLexico.AppendText("\n");
                }
            }
        }

        private void Sintactico()
        {
            foreach (var line in rtxtLexico.Lines)
            {
                String[] sintacticCodes = { "PICC", "ICC", "PCICIC", "PCCCCC", "PCCCIC", "PCICCC", "PCCC", "PCIC", "PCPI" };

                string codigoCurrent = "";
                foreach (var word in line.Split())
                {
                    if (word != "")
                    {
                        codigoCurrent += word[0];
                    }
                }
                //Asignacion
                if (codigoCurrent == "PIAC" || codigoCurrent == "PIAS")
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Asignacion";
                }
                //Reasignacion
                else if (codigoCurrent == "IAC")
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Reasignacion";
                }
                //Condicional
                else if (codigoCurrent == "PCICIC" || codigoCurrent == "PCCCCC" || codigoCurrent == "PCICCC" || codigoCurrent == "PCCCIC")
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Condicion";
                }
                //Ciclo For
                else if (codigoCurrent == "PCPIACICCIOOC")
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Ciclo For";
                }
                //WriteLine y ReadLine
                else if (codigoCurrent == "PCCC" || codigoCurrent == "PCIC")
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Impresion";
                }
            }
        }

        private string Semantica(string tipo, string valor, bool showError)
        {
            // Verificar tipo de dato
            switch (tipo)
            {
                case "int":
                    int numero;
                    if (!int.TryParse(valor, out numero))
                    {
                        if (showError)
                            MessageBox.Show("Error linea " + linea + ": el valor asignado no es un numérico entero");

                        return "false";
                    }
                    return "int";
                case "double":
                    double numerod;
                    if (!double.TryParse(valor, out numerod))
                    {
                        if (showError)
                            MessageBox.Show("Error linea " + linea + "el valor asignado no es un numérico real");

                        return "false";
                    }
                    return "double";
                case "bool":
                    if (!(valor.ToLower() == "true" || valor.ToLower() == "false"))
                    {
                        if (showError)
                            MessageBox.Show("Error linea " + linea + " el valor asignado no es booleano");
                        return "false";
                    }
                    return "bool";
                case "char":
                    if (valor.Length != 3 ||  valor[0] != '\'')
                    {
                        if (showError)
                            MessageBox.Show("Error linea " + linea + " el valor asignado no es un caracter");
                        return "false";
                    }
                    return "char";
                case "string":
                    if (!(valor[0] == '\"' && valor[valor.Length - 1] == '\"'))
                    {
                        if (showError)
                            MessageBox.Show("Error linea " + linea + " el valor asignado no es una cadena");
                        return "false";
                    }
                    return "string";
                default:
                    break;
            }
            return "false";
        }
        private string CheckVar(string valor, string prop)
        {
            if (dtgVariables.Rows.Count >= 1)
            {
                foreach (DataGridViewRow row in dtgVariables.Rows)
                {
                    if (valor == Convert.ToString(row.Cells["Nombre"].Value))
                    {
                        switch (prop)
                        {
                            case "Tipo":
                                return Convert.ToString(row.Cells["Tipo"].Value);
                            case "Nombre":
                                return Convert.ToString(row.Cells["Nombre"].Value);
                            case "CONT":
                                return Convert.ToString(row.Cells["CONT"].Value);
                            case "ID":
                                return Convert.ToString(row.Cells["ID"].Value);
                            default:
                                break;
                        }
                    }
                }
            }
            return "false";
        }


        int terPas = 0;
        bool isIf = false;
        bool isDo = false;
        private void button1_Click(object sender, EventArgs e)
        {
            autoincrementValue = 0;
            string lastLine = "";
            dtgVariables.Rows.Clear();
            rtxtPostfijo.Clear();
            if (rtxtCodigo.Lines.Length == 0)
            {
                MessageBox.Show("Inserta alguna instruccion de asignacion");
            }

            //Analizador Lexico
            Lexico();
            //Analizador Sintactico
            Sintactico();

            foreach (string input in rtxtCodigo.Lines)
            {
                linea++;
                if (input == "" || input[0] == '/')
                {
                    continue;
                }
                //Tercera pasada
                if (input[0] == '{') {
                    if (!Array.Exists(ANI, m => m == lastLine.Split()[0]))
                    {
                        MessageBox.Show("Apertura de instruccion anidada innecesaria");
                        continue;
                    }
                    else
                    {
                        lastLine = input;
                        terPas++;
                        rtxtPostfijo.AppendText("{\n");
                        continue;
                    } 
                }
                else if (input[0] == '}') {
                    lastLine = input;
                    terPas--;
                    rtxtPostfijo.AppendText("}\n");
                    if (input.Length == 1)
                    {
                        continue;
                    }
                }
                lastLine = input;
                string[] tokens = input.Split(' ');

                string valor2 = "";
                string valor = "";
                string variable = "";
                string operador = "";

                // Verificar si la asignacion esta bien
                //if (tokens.Length != 4 && tokens.Length != 6 && tokens.Length != 3 && tokens.Length != 5 && !Array.Exists(ANI, element => element == tokens[0]))
                //{
                //    MessageBox.Show("Error linea " + linea + ": instrucción mal formada");
                //    linea = 0;
                //    continue;
                //}
                string tipo = tokens[0].ToLower();

                if (tokens.Length >= 2)
                {
                    variable = tokens[1];
                    operador = tokens[2];
                }
                //Si es una instruccion aninada
                if (Array.Exists(ANI, element => element == tokens[0]) || tokens[0] == "}while")
                {
                    if ("if" == tokens[0])
                    {
                        isIf = true;
                        if (tokens[1] == "(" && tokens[tokens.Length - 1] == ")")
                        {
                            rtxtPostfijo.AppendText("if\n");
                            ArraySegment<string> segmento = new ArraySegment<string>(tokens, 2, tokens.Length - 3);

                            // Convertir el subconjunto en un nuevo array y unir sus elementos en una cadena
                            string operacionLogica = string.Join(" ", segmento.ToArray());

                            operacionLogica = AnalizarOperacionLogica(operacionLogica);
                            rtxtPostfijo.AppendText(InfijoAPostfijo(operacionLogica) + "\n");
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("Condicional mal formada");
                            continue;
                        }
                    }
                    else if ("while" == tokens[0] || ("}while" == tokens[0]))
                    {
                        if ("}while" == tokens[0])
                        {
                            if (!isDo)
                            {
                                MessageBox.Show("Error linea: " + linea + " \"do\" no encontrado");
                            }
                            isDo = false;
                            continue;
                        }
                        if (tokens[1] == "(" && tokens[tokens.Length - 1] == ")")
                        {
                            rtxtPostfijo.AppendText("while\n");
                            ArraySegment<string> segmento = new ArraySegment<string>(tokens, 2, tokens.Length - 3);

                            // Convertir el subconjunto en un nuevo array y unir sus elementos en una cadena
                            string operacionLogica = string.Join(" ", segmento.ToArray());

                            operacionLogica = AnalizarOperacionLogica(operacionLogica);
                            rtxtPostfijo.AppendText(InfijoAPostfijo(operacionLogica) + "\n");
                            continue;
                        }
                        else
                        {
                            MessageBox.Show("Ciclo mal formado");
                            continue;
                        }
                    }
                    else if ("do" == tokens[0])
                    {
                        isDo = true;
                        continue;
                    }
                    else if ("else" == tokens[0])
                    {
                        rtxtPostfijo.AppendText("else\n");
                        if (isIf)
                        {
                            isIf = false;
                            continue;
                        }
                        else
                        {
                            isIf = false;
                            MessageBox.Show("Error linea " + linea + ": else sin if");
                            continue;
                        }
                    }
                    else if ("for" == tokens[0])
                    {
                        if (!char.IsLetter(tokens[3][0]))
                        {
                            MessageBox.Show("Variable de Ciclo For mal formada");
                        }
                        else if (tokens[1] == "(" && tokens[2] == "int" && tokens[4] == "=" && Semantica("int", tokens[5], true) == "int" && tokens[9] == tokens[3] && tokens[10] == "+" && tokens[11] == "+" && tokens[12] == ")")
                        {
                            valor = tokens[6];
                            valor2 = tokens[8];
                            if (Array.Exists(LOG, m => m == tokens[7]))
                            {
                                if (Semantica("bool", valor, false) == "false" || Semantica("bool", valor2, false) == "false")
                                {
                                    MessageBox.Show("Los operadores logicos solo funcionan con booleanos");
                                    continue;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (Array.Exists(COMP, m => m == tokens[7]))
                            {
                                dtgVariables.Rows.Add("int", tokens[3], tokens[5]);
                                if (CheckVar(valor, "CONT") != "false")
                                {
                                    valor = CheckVar(valor, "CONT");
                                }
                                if (CheckVar(valor2, "CONT") != "false")
                                {
                                    valor2 = CheckVar(valor2, "CONT");
                                }
                                if (valor[0] == '\"' && Semantica("string", valor2, true) == "false")
                                {
                                    continue;
                                }
                                else if (valor[0] == '\'' && Semantica("char", valor2, true) == "false")
                                {
                                    continue;
                                }
                                else if (Semantica("int", valor, true) == "false" || Semantica("int", valor2, true) == "false")
                                {
                                    continue;
                                }
                                else if (Semantica("double", valor, true) == "false" || Semantica("double", valor2, true) == "false")
                                {
                                    continue;
                                }
                                continue;
                            }
                            else
                            {
                                MessageBox.Show("Ciclo For mal formado");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ciclo For mal formado");
                        }
                        continue;
                    }
                }

                //Para hacer operaciones
                if (tokens[0] == "int" || tokens[0] == "double" || (CheckVar(tokens[0],"Tipo") == "int"))
                {
                    //try
                    //{
                    //    string aux = IDES.FirstOrDefault(m => m.Item1 == tokens[1]).Item2;
                    //}
                    //catch (Exception)
                    //{
                    //    MessageBox.Show("Asignacion mal hecho en la linea " + linea);
                    //    continue;
                    //}
                    int indiceInicio = input.IndexOf("=");
                    string subcadena = input.Substring(indiceInicio + "=".Length);
                    string subcadenaInt = subcadena;

                    foreach (string elemento in subcadena.Split())
                    {
                        if (elemento == "") continue;
                        if (Char.IsLetter(elemento[0]) && CheckVar(elemento,"Nombre") == elemento)
                        {
                            subcadenaInt = subcadenaInt.Replace(elemento, CheckVar(elemento, "CONT"));
                        }
                    }
                    if (ResolverOperacionAritmetica(subcadenaInt) == .0001)
                    {
                        MessageBox.Show("Tipo de dato no coincidiente en la linea "+linea);
                        continue;
                    }

                    if (CheckVar(tokens[1], "Nombre") == tokens[1])
                    {
                        MessageBox.Show("Variable \"" + tokens[1] + "\" ya asignada");
                        continue;
                    }

                    if (CheckVar(tokens[0], "Nombre") == tokens[0]) { rtxtPostfijo.AppendText(tokens[0] + " " ); }
                    else { rtxtPostfijo.AppendText(tokens[1] + " "); }

                    rtxtPostfijo.AppendText(InfijoAPostfijo(subcadena) + "=\n");

                    if ((tokens[0] == "int" || tokens[0] == "double") && CheckVar(tokens[1], "Nombre") == "false")
                    {
                        if (tokens[0] == "int") dtgVariables.Rows.Add(tipo, tokens[1], Math.Round(ResolverOperacionAritmetica(subcadenaInt)), IDES.FirstOrDefault(m => m.Item1 == tokens[1]).Item2);
                        else dtgVariables.Rows.Add(tipo, tokens[1], ResolverOperacionAritmetica(subcadenaInt), IDES.FirstOrDefault(m => m.Item1 == tokens[1]).Item2);

                    }
                    //REASIGNACION
                    else if (CheckVar(tokens[0], "Nombre") == tokens[0])
                    {
                        foreach (DataGridViewRow row in dtgVariables.Rows)
                        {
                            if (tokens[0] == Convert.ToString(row.Cells["Nombre"].Value))
                            {
                                row.Cells["CONT"].Value = ResolverOperacionAritmetica(subcadenaInt);
                                break;
                            }
                        }
                    }
                    continue;
                }

                if (tokens[0] == "println")
                {
                    continue;
                }
                else if (tokens[0] == "readline")
                {
                    string var = CheckVar(tokens[2], "CONT");
                    if (var == "false")
                    {
                        MessageBox.Show("Error linea: " + linea + " instruccion \"readline\" solo acepta variables como argumento");
                    }
                    continue;
                }
                if (tokens.Length >= 4)
                {
                    valor = tokens[3];
                }
                //Si no es una reasignacion es una declaracion
                // Verificar si el tipo de dato es corecto
                if (!Array.Exists(type, element => element == tipo))
                {
                    MessageBox.Show("Error linea " + linea + ": tipo de dato no valido");
                    linea = 0;
                    continue;
                }
                // Verificar si la variable es válida
                if (!char.IsLetter(variable[0]))
                {
                    MessageBox.Show("Error linea " + linea + ": el identificador de la variable es inválido");
                    linea = 0;
                    continue;
                }

                // Verificar si el operador es "="
                if (operador != "=")
                {
                    MessageBox.Show("Error linea " + linea + ": operador inválido");
                    linea = 0;
                    continue;
                }
                //Revisa si el valor es una variable
                if (dtgVariables.Rows.Count > 1)
                {
                    foreach (DataGridViewRow row in dtgVariables.Rows)
                    {
                        if (valor == Convert.ToString(row.Cells["Nombre"].Value))
                        {
                            valor = Convert.ToString(row.Cells["CONT"].Value);
                            break;
                        }
                    }
                }
                // Verificar si el tipo de valor
                if (Semantica(tipo, valor,true) == "false")
                {
                    MessageBox.Show("Error linea: " + linea + " los tipos de datos no coinciden");
                    continue;
                }
                bool isValid = true;
                //Revisa si hay identificadores iguales
                if (dtgVariables.Rows.Count > 1)
                {
                    foreach (DataGridViewRow row in dtgVariables.Rows)
                    {
                        if (variable == Convert.ToString(row.Cells["Nombre"].Value))
                        {
                            MessageBox.Show("El identificador " + variable + " ya esta asignada");
                            isValid = false;
                            break;
                        }
                    }
                }
                // La instrucción es válida, se puede asignar el valor a la variable
                if (isValid)
                {
                    dtgVariables.Rows.Add(tipo, variable, valor, IDES.FirstOrDefault(m => m.Item1 == tokens[1]).Item2);
                    isValid = false;
                }
            }
            if (terPas != 0)
            {
                MessageBox.Show("Error de tercera pasada");
            }
            else
            {
                Cuadruplos();
            }
            //string TXTPostfijoTokens = rtxtPostfijo.Text;
            foreach (string line in rtxtPostfijo.Lines)
            {
                foreach (string palabra in line.Split())
                {
                    if (palabra == "" || palabra == " ") continue;
                    string remplazo = PalabrasReservadas.FirstOrDefault(m => m.Item1 == palabra)?.Item2 ?? palabra;

                    if (remplazo == palabra)
                    {
                        if (Char.IsDigit(palabra[0]))
                        { 
                            remplazo = ContTuple.FirstOrDefault(m => m.Item2 == palabra).Item1; 
                        }
                        else { remplazo = CheckVar(palabra, "ID"); }
                    }
                    rtxtPostfijo.AppendText(remplazo + " ");
                }
                rtxtPostfijo.AppendText("\n");
            }


            terPas = 0;
            MessageBox.Show("Se completo el analisis semantico");
            linea = 0;

        }
        private void Cuadruplos()
        {
            dtgvCuadruplos.Rows.Clear();
            string lastWord = "";
            bool alredyCuad = false;
            string lastlastWord = "";
            int CuadId = 1;
            bool isPathFalse = false;
            int RoutID = 0;
            foreach (string line in rtxtPostfijo.Lines)
            {
                if (line == "") { continue; }
                if (line == "else") { isPathFalse = true; continue; }
                string currentLine = line;
                int TempID = 1;
                if (isPathFalse)
                {
                    if (line[0] == '{')
                    {
                        dtgvCuadruplos.Rows.Add("", $"FalseCuad{RoutID}", "", "");
                        continue;
                    }
                    if (line[0] == '}')
                    {
                        dtgvCuadruplos.Rows.Add("", $"FalseFin{RoutID}", "", "");
                        isPathFalse = false;
                        continue;
                    }
                }
                else
                {
                    if (line[0] == '{')
                    {
                        RoutID++;
                        dtgvCuadruplos.Rows.Add("", $"TrueCuad{RoutID}", "", "");
                        continue;
                    }
                    if (line[0] == '}')
                    {
                        dtgvCuadruplos.Rows.Add("", $"TrueFin{RoutID}", "", "");
                        continue;
                    }
                }
                
                string newPost = "";
                string[] word = line.Split(' ');
                
                if (word[0] == "if" || word[0] == "while")
                {
                    dtgvCuadruplos.Rows.Add(word[0], "Cuadruplo " + CuadId, "", "");
                    alredyCuad = true;
                    CuadId++;
                    continue;
                }
                else if (!alredyCuad)
                {
                    CuadId++;
                    dtgvCuadruplos.Rows.Add("", "Cuadruplo " + CuadId, "", "");
                }
                alredyCuad = false;
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i].Contains("IDEN") || word[i].Contains("CNR") || word[i].Contains("CNE"))
                    {
                        return;
                    }
                    if (word[i] == "=")
                    {
                        dtgvCuadruplos.Rows.Add(lastlastWord, lastWord,"", word[i]);
                        continue;
                    }
                    else if ((CE.Any(m => m == word[i]) || LOG.Any(m => m == word[i]) || COMP.Any(m => m == word[i])) && (CheckVar(lastWord, "ID") != "false" || Char.IsDigit(lastWord[0]) || lastWord[0] == 'T') && (CheckVar(lastlastWord, "ID") != "false" || Char.IsDigit(lastlastWord[0]) || lastlastWord[0] == 'T'))
                    {
                        if (word.Contains("||") || word.Contains("&&"))
                        {
                            if (LOG.Any(m => m == word[i]))
                            {
                                dtgvCuadruplos.Rows.Add("T" + TempID, lastlastWord, lastWord, word[i]);
                                //Se pasa al TRUE
                                dtgvCuadruplos.Rows.Add("RouteT", "", "", "");
                                //Se pasa a la otra operacion
                                dtgvCuadruplos.Rows.Add("RouteF", "", "", "");
                                continue;
                            }
                        }
                        else if (COMP.Any(m => m == word[i]))
                        {
                            dtgvCuadruplos.Rows.Add("T" + TempID, lastlastWord, lastWord, word[i]);
                            //Se pasa al TRUE
                            dtgvCuadruplos.Rows.Add("RouteT", "", "", "");
                            //Se pasa a la otra operacion
                            dtgvCuadruplos.Rows.Add("RouteF", "", "", "");
                            continue;
                        }

                        dtgvCuadruplos.Rows.Add("T" + TempID, lastlastWord, lastWord, word[i]);
                        newPost = string.Join(" ", newPost.Split().Take(newPost.Split().Length - 2));
                        newPost += " T" + TempID;
                        while (i < word.Length - 1)
                        {
                            newPost += " " + word[i + 1];
                            i++;
                        }

                        TempID++;
                        lastlastWord = "";
                        lastWord = "";
                        i = 0;
                        word = newPost.Split();
                        newPost = "";
                        continue;
                    }
                    newPost += " "+ word[i];
                    lastlastWord = lastWord;
                    lastWord = word[i];
                }
            }
            PasosAnidados();
        }
        private void PasosAnidados()
        {
            int auxRowRoute = 0;
            int auxRowFin = 0;
            int auxWhile = 0;
            //Busca Rutas
            for (int i = 0; i < dtgvCuadruplos.RowCount-1; i++)
            {
                if (dtgvCuadruplos.Rows[i].Cells[0].Value.ToString().Contains("while"))
                {
                    auxWhile = int.Parse(dtgvCuadruplos.Rows[i].Cells[4].Value.ToString())+1;
                }
                //Ruta Verdadera
                if (dtgvCuadruplos.Rows[i].Cells[0].Value.ToString().Contains("RouteT"))
                {
                    //Busca el true target
                    for (int j = i; j < dtgvCuadruplos.RowCount - 1; j++)
                    {
                        if (dtgvCuadruplos.Rows[j].Cells[1].Value.ToString().Contains("TrueCuad"))
                        {
                            dtgvCuadruplos.Rows[i].Cells[3].Value = dtgvCuadruplos.Rows[j].Cells[4].Value;
                            break;
                        }
                    }
                }
                //Ruta Falsa
                else if (dtgvCuadruplos.Rows[i].Cells[0].Value.ToString().Contains("RouteF"))
                {
                    auxRowRoute = i;
                    //Busca el true target
                    for (int j = i; j < dtgvCuadruplos.RowCount - 1; j++)
                    {
                        if (dtgvCuadruplos.Rows[j].Cells[1].Value.ToString().Contains("TrueFin"))
                        {
                            auxRowFin = j;
                            dtgvCuadruplos.Rows[i].Cells[3].Value = dtgvCuadruplos.Rows[j].Cells[4].Value;
                            break;
                        }
                    }
                }

                //Ruta else
                if (dtgvCuadruplos.Rows[i].Cells[1].Value.ToString().Contains("FalseCuad"))
                {
                    dtgvCuadruplos.Rows[auxRowRoute].Cells[3].Value = dtgvCuadruplos.Rows[i].Cells[4].Value;

                    for (int j = i; j < dtgvCuadruplos.RowCount - 1; j++)
                    {
                        if (dtgvCuadruplos.Rows[j].Cells[1].Value.ToString().Contains("FalseFin"))
                        {
                            dtgvCuadruplos.Rows[auxRowFin].Cells[3].Value = dtgvCuadruplos.Rows[j].Cells[4].Value;
                            break;
                        }
                    }
                }
                //Ruta ciclo
                if (dtgvCuadruplos.Rows[i].Cells[1].Value.ToString().Contains("TrueFin") && auxWhile != 0)
                {
                    dtgvCuadruplos.Rows[i].Cells[3].Value = auxWhile;
                    auxWhile = 0;
                }
            }
        }
        private void onEnter(object sender, KeyEventArgs e)
        {
            // Verificar si se presionó la tecla Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Agregar el número de línea para la nueva línea
                int line = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
                richTextBox1.AppendText((line + 1).ToString() + Environment.NewLine); // +2 porque los índices empiezan en cero
            }
        }

        public static double ResolverOperacionAritmetica(string operacion)
        {
            try
            {
                DataTable dt = new DataTable();
                var resultado = dt.Compute(operacion, "");
                return Convert.ToDouble(resultado);
            }
            catch (Exception)
            {
                return .0001;
            }
        }

        public static bool EvaluarOperacionLogica(string operacion)
        {
            DataTable dt = new DataTable();
            return (bool)dt.Compute(operacion, "");
        }

        public static string InfijoAPostfijo(string infijo)
        {
            Stack<string> pila = new Stack<string>();
            Stack<char> pilaParentesisIzquierdos = new Stack<char>();
            string postfijo = "";
            Dictionary<string, int> precedencia = new Dictionary<string, int>() { 
                { "=", 0 },
                { "==", 1 },
                { "<>", 1 },
                { "<", 1 },
                { ">", 1 },
                { "<=", 1 },
                { ">=", 1 },
                { "+", 2 },
                { "-", 2 },
                { "*", 3 },
                { "/", 3 },
                { "^", 4 },
                { "&&", 5 },
                { "||", 5 },
                { "(", 0 } };
            int i = 0;
            int skips = 0;
            foreach (char caracter in infijo)
            {
                if (skips != 0) { skips--; i++; continue; }
                if (Char.IsLetterOrDigit(caracter))
                {
                    postfijo += caracter;

                    if (infijo.Count() == i + 1) { postfijo += " "; break; }

                    if (Char.IsLetterOrDigit(infijo[i + 1]))
                    {
                        int j = i + 1;
                        while (Char.IsLetterOrDigit(infijo[j]))
                        {
                            postfijo += infijo[j];
                            skips++;
                            j++;
                            if (infijo.Count() == j) break;
                        }
                    }
                    postfijo += " ";
                }
                else if (caracter == '(')
                {
                    pila.Push(caracter.ToString());
                    pilaParentesisIzquierdos.Push(caracter);
                }
                else if (caracter == ')')
                {
                    if (pilaParentesisIzquierdos.Count > 1)
                    {
                        while (pila.Peek() != pilaParentesisIzquierdos.Peek().ToString())
                        {
                            postfijo += pila.Pop() + " ";
                        }
                        pila.Pop();
                        pilaParentesisIzquierdos.Pop();
                    }
                    else if (pilaParentesisIzquierdos.Count == 1)
                    {
                        while (pila.Peek() != "(")
                        {
                            postfijo += pila.Pop() + " ";
                        }
                        pila.Pop();
                        pilaParentesisIzquierdos.Pop();
                    }
                }
                else if (precedencia.ContainsKey(caracter.ToString()) && !(infijo[i + 1] == '=' || infijo[i + 1] == '>'))
                {
                    while (pila.Count > 0 && precedencia[caracter.ToString()] <= precedencia[pila.Peek().ToString()])
                    {
                        postfijo += pila.Pop() + " ";
                    }
                    pila.Push(caracter.ToString());
                }
                switch (caracter)
                {
                    case '&':
                        if (infijo[i + 1] == '&') {
                            while (pila.Count > 0 && precedencia["&&"] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push("&&"); 
                            skips++; 
                        }
                        break;
                    case '|':
                        if (infijo[i + 1] == '|') {
                            while (pila.Count > 0 && precedencia["||"] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push("||"); skips++; 
                        }
                        break;
                    case '>':
                        if (infijo[i + 1] == '=') {
                            while (pila.Count > 0 && precedencia[">="] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push(">="); 
                            skips++; 
                        }
                        break;
                    case '<':
                        if (infijo[i + 1] == '=') {
                            while (pila.Count > 0 && precedencia["<="] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push("<="); 
                            skips++;
                        }
                        else if (infijo[i + 1] == '>')
                        {
                            while (pila.Count > 0 && precedencia["<>"] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push("<>");
                            skips++;
                        }
                        break;
                    case '=':
                        if (infijo[i + 1] == '=') {
                            while (pila.Count > 0 && precedencia["=="] <= precedencia[pila.Peek().ToString()])
                            {
                                postfijo += pila.Pop() + " ";
                            }
                            pila.Push("=="); 
                            skips++; 
                        }
                        break;
                    default:
                        break;
                }
                i++;
            }
            //if ( ( 5 < 10 ) && ( 3 + 4 >= 7 ) || ( 2 * 5 <> 10 ) )
            while (pila.Count > 0)
            {
                postfijo += pila.Pop() + " ";
            }
            char[] postfijoArray = postfijo.ToCharArray();
            string postfijoRes = new string(postfijoArray);
            return postfijoRes;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            // Mostrar el diálogo de selección de archivo
            OpenFileDialog dialogo = new OpenFileDialog();
            dialogo.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            dialogo.Title = "Seleccionar archivo de texto";
            if (dialogo.ShowDialog() != DialogResult.OK)
                return;

            // Leer el contenido del archivo seleccionado
            string contenido = File.ReadAllText(dialogo.FileName);

            // Escribir el contenido en el RichTextBox
            rtxtCodigo.Text = contenido;
        }

        //Esto convierte las variables en valores
        private string AnalizarOperacionLogica(string operacionLogica)
        {
            string operacionLogicaFinal = operacionLogica;
            foreach (string elemento in operacionLogica.Split())
            {
                if (elemento == "") continue;
                if (Char.IsLetter(elemento[0]) && CheckVar(elemento, "Nombre") == elemento)
                {
                    operacionLogicaFinal = operacionLogica.Replace(elemento, CheckVar(elemento, "CONT"));
                }
            }
            return operacionLogicaFinal;
        }

        private int autoincrementValue = 0;
        private void dtgvCuadruplos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int rowIndex = e.RowIndex;
            // Si hay filas previas, obtener el valor autoincrementable de la última fila
            if (rowIndex > 0)
            {
                int lastValue = Convert.ToInt32(dtgvCuadruplos.Rows[rowIndex - 1].Cells[dtgvCuadruplos.Columns.Count - 1].Value);
                autoincrementValue = lastValue;
            }

            // Asignar el valor autoincrementable a la última columna de la nueva fila agregada
            dtgvCuadruplos.Rows[rowIndex].Cells[dtgvCuadruplos.Columns.Count - 1].Value = ++autoincrementValue;
        }
    }
}
