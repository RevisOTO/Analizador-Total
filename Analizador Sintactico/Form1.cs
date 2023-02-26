using System;
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
            //Tipos de datos
            PalabrasReservadas.Add(new Tuple<string, string>("string", "PR03"));
            PalabrasReservadas.Add(new Tuple<string, string>("int", "PR04"));
            PalabrasReservadas.Add(new Tuple<string, string>("bool", "PR05"));
            PalabrasReservadas.Add(new Tuple<string, string>("double", "PR06"));
            PalabrasReservadas.Add(new Tuple<string, string>("const", "PR07"));
            PalabrasReservadas.Add(new Tuple<string, string>("char", "PR15"));

            //If y Loops
            PalabrasReservadas.Add(new Tuple<string, string>("for", "PR08"));
            PalabrasReservadas.Add(new Tuple<string, string>("while", "PR09"));
            PalabrasReservadas.Add(new Tuple<string, string>("do", "PR10"));
            PalabrasReservadas.Add(new Tuple<string, string>("if", "PR11"));
            PalabrasReservadas.Add(new Tuple<string, string>("else", "PR12"));

            //Otros
            PalabrasReservadas.Add(new Tuple<string, string>("inicio", "PR13"));
            PalabrasReservadas.Add(new Tuple<string, string>("final", "PR14"));

            //Identificadores
            PalabrasReservadas.Add(new Tuple<string, string>("iden", "IDEN"));

            //Operadores
            PalabrasReservadas.Add(new Tuple<string, string>("+", "OPER01"));
            PalabrasReservadas.Add(new Tuple<string, string>("-", "OPER02"));
            PalabrasReservadas.Add(new Tuple<string, string>("/", "OPER03"));
            PalabrasReservadas.Add(new Tuple<string, string>("*", "OPER04"));
            PalabrasReservadas.Add(new Tuple<string, string>("^", "OPER05"));

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

            //Asignacion
            PalabrasReservadas.Add(new Tuple<string, string>("=", "ASIGN01"));

            //Cometario y Letrero
            PalabrasReservadas.Add(new Tuple<string, string>("Comentario", "COM"));
            PalabrasReservadas.Add(new Tuple<string, string>("Letrero", "LET"));
        }
        int linea;
        bool OPA = false;
        String[] type = { "int", "string", "bool", "char", "double" };
        String[] CE = { "+", "-", "/", "*" };
        private void Lexico()
        {
            rtxtLexico.Clear();
            string lastWord = "";
            int ide = 0;
            int cont = 0;
            foreach (var line in rtxtCodigo.Lines)
            {
                foreach (var word in line.Split())
                {

                    if (PalabrasReservadas.Any(m => m.Item1 == word))
                    {
                        string PR = PalabrasReservadas.FirstOrDefault(m => m.Item1 == word).Item2;
                        rtxtLexico.AppendText(PR + " ");
                    }
                    else
                    {
                        //Identificadores
                        if (Array.Exists(type, element => element == lastWord))
                        {
                            ide++;
                            rtxtLexico.AppendText("IDEN"+ide+" ");
                        }
                        else if(lastWord == "=")
                        {

                            cont++;
                            rtxtLexico.AppendText("CONT" + cont);
                        }
                        else
                        {
                            if (word != line.Split()[line.Split().Length-1])
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
                if (line != rtxtCodigo.Lines[rtxtCodigo.Lines.Length-1])
                {
                    rtxtLexico.AppendText("\n");
                }
            }
        }

        private void Sintactico()
        {
            foreach (var line in rtxtLexico.Lines)
            {
                String[] sintacticCodes = { "PIAC", "IAC", "bool", "char", "double" };

                string codigoCurrent = "";
                foreach (var word in line.Split())
                {
                    codigoCurrent+= word[0];
                }
                //Asignacion
                if (Array.Exists(sintacticCodes, element => element == codigoCurrent && codigoCurrent == "PIAC"))
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor= Color.Green;
                    btnValidate.Text = "Asignacion";
                }
                //Reasignacion
                else if (Array.Exists(sintacticCodes, element => element == codigoCurrent && codigoCurrent == "IAC"))
                {
                    txtLinea.Text = line;
                    btnValidate.BackColor = Color.Green;
                    btnValidate.Text = "Reasignacion";
                }
            }
        }

        private string Semantica(string tipo, string valor)
        {
            // Verificar tipo de dato
            switch (tipo)
            {
                case "int":
                    int numero;
                    if (!int.TryParse(valor, out numero))
                    {
                        MessageBox.Show("Error linea " + linea + ": el valor asignado no es un numérico entero");
                        
                        return "false";
                    }
                    return "int";
                case "double":
                    double numerod;
                    if (!double.TryParse(valor, out numerod))
                    {
                        MessageBox.Show("Error linea " + linea + "el valor asignado no es un numérico real");
                        
                        return "false";
                    }
                    return "double";
                case "bool":
                    if (!(valor.ToLower() == "true" || valor.ToLower() == "false"))
                    {
                        MessageBox.Show("Error linea " + linea + " el valor asignado no es booleano");
                        return "false";
                    }
                    return "bool";
                case "char":
                    char caracter;
                    if ( valor.Length < 3 || (!char.TryParse(valor[1].ToString(), out caracter) && !(valor[0] == '\'' && valor[valor.Length - 1] == '\'')))
                    {
                        MessageBox.Show("Error linea " + linea + " el valor asignado no es un caracter");
                        return "false";
                    }
                    return "char";
                case "string":
                    if (!(valor[0] == '\"' && valor[valor.Length - 1] == '\"'))
                    {
                        MessageBox.Show("Error linea " + linea + " el valor asignado no es una cadena");
                        return "false";
                    }
                    return "string";
                default:
                    break;
            }
            return "false";
        }
        private string CheckVar(string valor,string prop)
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

 

        private void button1_Click(object sender, EventArgs e)
        {
            dtgVariables.Rows.Clear();
            if (rtxtCodigo.Lines.Length == 0)
            {
                MessageBox.Show("Inserta alguna instruccion de asignacion");
            }

            //Analizador Lexico
            Lexico();
            //Analizador
            Sintactico();
            foreach (string input in rtxtCodigo.Lines)
            {
                linea++;
                string[] tokens = input.Split(' ');
                string oa = "";
                string valor2;
                // Verificar si la asignacion contiene cuatro elementos
                if (tokens.Length != 4 && tokens.Length != 6 && tokens.Length != 3)
                {
                    MessageBox.Show("Error linea " + linea + ": instrucción mal formada");
                    linea = 0;
                    return;
                }
                else if (tokens.Length == 6)
                {
                    OPA = true;
                    oa = tokens[4];
                    valor2 = tokens[5];
                }
                string tipo = tokens[0].ToLower();
                string variable = tokens[1];
                string operador = tokens[2];
                string valor = "";
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
                        if (Semantica(CheckVar(tokens[0], "Tipo"), tokens[2]) == "false")
                        {
                            continue;
                        }
                    }
                    else if (Semantica(CheckVar(tokens[0], "Tipo"), CheckVar(tokens[2], "CONT")) == "false")
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
                if (Semantica(tipo, valor) == "false")
                {
                    continue;
                }
                if (OPA)
                {
                    //Verifica si el operador es correcto
                    if (!Array.Exists(CE, element => element == oa))
                    {
                        MessageBox.Show("Error linea " + linea + " operador no valido");
                        continue;
                    }
                    if (!Array.Exists(CE, element => element == oa))
                    {
                        MessageBox.Show("Error linea " + linea + " caracter especial no valido");
                        continue;
                    }
                    OPA = false;
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
                    dtgVariables.Rows.Add(tipo, variable, valor);
                    isValid = true;
                }
            }
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

    }
}
