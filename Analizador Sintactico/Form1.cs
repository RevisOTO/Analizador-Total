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
            PalabrasReservadas.Add(new Tuple<string, string>("readline", "PR01"));
            PalabrasReservadas.Add(new Tuple<string, string>("println", "PR02"));
            PalabrasReservadas.Add(new Tuple<string, string>("string", "PR03"));
            PalabrasReservadas.Add(new Tuple<string, string>("int", "PR04"));
            PalabrasReservadas.Add(new Tuple<string, string>("bool", "PR05"));
            PalabrasReservadas.Add(new Tuple<string, string>("double", "PR06"));
            PalabrasReservadas.Add(new Tuple<string, string>("continue", "PR07"));
            PalabrasReservadas.Add(new Tuple<string, string>("char", "PR15"));

            //If y Loops
            PalabrasReservadas.Add(new Tuple<string, string>("for", "PR08"));
            PalabrasReservadas.Add(new Tuple<string, string>("while", "PR09"));
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
            PalabrasReservadas.Add(new Tuple<string, string>(">", "COMP01"));
            PalabrasReservadas.Add(new Tuple<string, string>("<", "COMP02"));
            PalabrasReservadas.Add(new Tuple<string, string>(">=", "COMP03"));
            PalabrasReservadas.Add(new Tuple<string, string>("<=", "COMP04"));
            PalabrasReservadas.Add(new Tuple<string, string>("<>", "COMP05"));
            PalabrasReservadas.Add(new Tuple<string, string>("==", "COMP06"));

            //Operadores Logicos
            PalabrasReservadas.Add(new Tuple<string, string>("&&", "LOG01"));
            PalabrasReservadas.Add(new Tuple<string, string>("||", "LOG02"));
            PalabrasReservadas.Add(new Tuple<string, string>("!", "LOG03"));

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
        bool OPA = false;
        String[] type = { "int", "string", "bool", "char", "double" };
        String[] CE = { "+", "-", "/", "*", "^", "(", "=" };
        String[] COMP = { "==", ">", "<", "<>", "<=", ">=" };
        String[] LOG = { "!", "||", "&&" };
        String[] ANI = { "if", "for", "do", "while", "else" };

        List<Tuple<string, string>> IDES = new List<Tuple<string, string>>();
        private void Lexico()
        {
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
                                continue;
                            }
                        }
                        if (lastWord == "=")
                        {
                            IDES.Add(new Tuple<string, string>(line.Split()[countWord - 2], "IDEN" + ide));
                        }
                        if (Array.Exists(CE, element => element == lastWord) || Array.Exists(COMP, element => element == lastWord))
                        {

                            if (word != line.Split()[line.Split().Length - 1])
                            {
                                if (Semantica("int", word, false) == "int")
                                {
                                    rtxtLexico.AppendText("CNE ");
                                }
                                else if (Semantica("double", word, false) == "double")
                                {
                                    rtxtLexico.AppendText("CNR ");
                                }
                                else if (Semantica("char", word, false) == "char")
                                {
                                    rtxtLexico.AppendText("CHAR ");
                                }
                                else if (Semantica("bool", word, false) == "bool")
                                {
                                    rtxtLexico.AppendText("BOOL ");
                                }
                                else
                                {
                                    rtxtLexico.AppendText("STR ");
                                }

                            }
                            else
                            {
                                if (Semantica("int", word, false) == "int")
                                {
                                    rtxtLexico.AppendText("CNE");
                                }
                                else if (Semantica("double", word, false) == "double")
                                {
                                    rtxtLexico.AppendText("CNR");
                                }
                                else if (Semantica("char", word, false) == "char")
                                {
                                    rtxtLexico.AppendText("CHAR");
                                }
                                else if (Semantica("bool", word, false) == "bool")
                                {
                                    rtxtLexico.AppendText("BOOL");
                                }
                                else
                                {
                                    rtxtLexico.AppendText("STR");
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
                    if (valor == Convert.ToString(row.Cells["ID"].Value))
                    {
                        switch (prop)
                        {
                            case "Tipo":
                                return Convert.ToString(row.Cells["Tipo"].Value);
                            case "ID":
                                return Convert.ToString(row.Cells["ID"].Value);
                            case "CONT":
                                return Convert.ToString(row.Cells["CONT"].Value);
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
            string lastLine = "";
            dtgVariables.Rows.Clear();
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
                        continue;
                    } 
                }
                else if (input[0] == '}') {
                    lastLine = input;
                    terPas--;
                    if (input.Length == 1)
                    {
                        continue;
                    }
                }
                lastLine = input;
                OPA = false;
                string[] tokens = input.Split(' ');

                string oa = "";
                string valor2 = "";
                string valor = "";
                int resultado = 0;
                string variable = "";
                string operador = "";

                // Verificar si la asignacion esta bien
                if (tokens.Length != 4 && tokens.Length != 6 && tokens.Length != 3 && tokens.Length != 5 && !Array.Exists(ANI, element => element == tokens[0]))
                {
                    MessageBox.Show("Error linea " + linea + ": instrucción mal formada");
                    linea = 0;
                    continue;
                }
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
                        if (tokens[1] == "(" && tokens[5] == ")")
                        {
                            valor = tokens[2];
                            valor2 = tokens[4];

                            if (Array.Exists(LOG, element => element == tokens[3]))
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
                            else if (Array.Exists(COMP, element => element == tokens[3]))
                            {
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
                                MessageBox.Show("Condicional mal formada");
                                continue;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Condicional mal formada");
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
                                if (Semantica("bool",valor,false) == "false" || Semantica("bool", valor2, false) == "false")
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
                    else if("while" == tokens[0] || ("}while" == tokens[0]))
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
                        if (tokens[1] == "(" && tokens[5] == ")")
                        {
                            valor = tokens[2];
                            valor2 = tokens[4];
                            if (Array.Exists(LOG, m => m == tokens[3]))
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
                            else if (Array.Exists(COMP, m => m == tokens[3]))
                            {
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
                                MessageBox.Show("Ciclo While mal formado");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ciclo While mal formado");
                        }
                        continue;
                    }
                    else if("do" == tokens[0])
                    {
                        isDo = true;
                        continue;
                    }
                    else if("else" == tokens[0] ) 
                    {
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
                }
                //Operacion de asignacion
                if (tokens.Length == 6)
                {
                    OPA = true;
                    valor = tokens[3];
                    oa = tokens[4];
                    valor2 = tokens[5];
                }
                //Operacion de reasignacion
                else if (tokens.Length == 5)
                {
                    valor = tokens[2];
                    oa = tokens[3];
                    valor2 = tokens[4];
                    if (doOPA(valor, valor2, oa, CheckVar(tipo, "Tipo")) == "false")
                    {
                        continue;
                    }
                    resultado = int.Parse(doOPA(valor, valor2, oa, CheckVar(tipo,"Tipo")));
                    foreach (DataGridViewRow Row in dtgVariables.Rows)
                    {
                        if (Convert.ToString(Row.Cells["ID"].Value) == CheckVar(tipo,"ID"))
                        {
                            Row.Cells["CONT"].Value = resultado;
                            break;
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
                //Reasigna la variable a otro valor
                if (tokens.Length == 3)
                {
                    if (CheckVar(tokens[0], "CONT") == "false")
                    {
                        MessageBox.Show("Error linea " + linea + ": variable contenedora no asignada");
                        continue;
                    }
                    else if (tokens[1] != "=")
                    {
                        MessageBox.Show("Error linea " + linea + ": caracter especial = no encontrado");
                        continue;
                    }
                    else if (CheckVar(tokens[2], "CONT") == "false")
                    {
                        if (Semantica(CheckVar(tokens[0], "Tipo"), tokens[2],true) == "false")
                        {
                            continue;
                        }
                    }
                    else if (Semantica(CheckVar(tokens[0], "Tipo"), CheckVar(tokens[2], "CONT"),true) == "false")
                    {
                        continue;
                    }
                    if (CheckVar(tokens[2], "CONT") == "false")
                    {
                        foreach (DataGridViewRow row in dtgVariables.Rows)
                        {
                            if (tokens[0] == Convert.ToString(row.Cells["ID"].Value))
                            {
                                row.Cells["CONT"].Value = tokens[2];
                                break;
                            }
                        }
                        continue;
                    }
                    foreach (DataGridViewRow row in dtgVariables.Rows)
                    {

                        if (tokens[0] == Convert.ToString(row.Cells["ID"].Value))
                        {
                            row.Cells["CONT"].Value = CheckVar(tokens[2], "CONT");
                            break;
                        }
                    }
                    continue;
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
                        if (valor == Convert.ToString(row.Cells["ID"].Value))
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
                if (OPA)
                {
                    if (doOPA(valor, valor2, oa, tipo) != "false")
                    {
                        resultado = int.Parse(doOPA(valor, valor2, oa, tipo));
                    }
                    else
                    {
                        OPA = false;
                        continue;
                    }
                    
                }
                bool isValid = true;
                //Revisa si hay identificadores iguales
                if (dtgVariables.Rows.Count > 1)
                {
                    foreach (DataGridViewRow row in dtgVariables.Rows)
                    {
                        if (variable == Convert.ToString(row.Cells["ID"].Value))
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
                    if (OPA)
                    {
                        dtgVariables.Rows.Add(tipo, variable, resultado);
                    }
                    else
                    {
                        dtgVariables.Rows.Add(tipo, variable, valor);
                    }
                    isValid = false;
                    OPA = false;
                }
            }
            if (terPas != 0)
            {
                MessageBox.Show("Error de tercera pasada");
            }
            terPas = 0;
            MessageBox.Show("Se completo el analisis semantico");
            linea = 0;
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
        private string doOPA(string valor, string valor2, string oa, string tipo)
        {
            var resultado = 0;
            if (CheckVar(valor, "CONT") != "false")
            {
                valor = CheckVar(valor, "CONT");
            }
            if (CheckVar(valor2, "CONT") != "false")
            {
                valor2 = CheckVar(valor2, "CONT");
            }
            //Verifica si el operador es correcto
            if (!Array.Exists(CE, element => element == oa))
            {
                MessageBox.Show("Error linea " + linea + " operador no valido");
                return "false";
            }
            if (Semantica(tipo, valor2, true) == "false" || tipo == "string" || tipo == "bool" || tipo == "char")
            {
                MessageBox.Show("Error linea " + linea + " operacion aritmetica no permitida");
                return "false";
            }
            switch (oa)
            {
                case "+":
                    resultado = int.Parse(valor) + int.Parse(valor2);
                    return resultado.ToString();
                case "-":
                    resultado = int.Parse(valor) - int.Parse(valor2);
                    return resultado.ToString();
                case "*":
                    resultado = int.Parse(valor) * int.Parse(valor2);
                    return resultado.ToString();
                case "/":
                    resultado = int.Parse(valor) / int.Parse(valor2);
                    return resultado.ToString();
                case "^":
                    resultado = (int)Math.Pow(int.Parse(valor), int.Parse(valor2));
                    return resultado.ToString();
                default:
                    MessageBox.Show("Error linea " + linea + " operador no valido");
                    return "false";
            }
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
    }
}
