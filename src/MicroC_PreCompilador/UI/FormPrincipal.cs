using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MicroC_PreCompilador
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtEditor.Clear();
            txtSalida.Clear();
        }



        private void btnCompilar_Click(object sender, EventArgs e)
        {
            txtSalida.Clear();

            if (String.IsNullOrWhiteSpace(txtEditor.Text))
            {
                txtSalida.Text = "Error: No hay código para compilar";
                return;
            }

            string codigo = txtEditor.Text;
            char[] caracteres = codigo.ToCharArray();

            string actual = "";
            bool enString = false;

            HashSet<string> reservadas = new HashSet<string>()
    {
        "int", "float", "if", "else", "while",
        "for", "return", "main", "void"
    };

            for (int i = 0; i < caracteres.Length; i++)
            {
                char c = caracteres[i];

                // 🔹 Manejo de strings
                if (c == '"')
                {
                    if (enString)
                    {
                        actual += c;
                        txtSalida.AppendText("STRING: " + actual + Environment.NewLine);
                        actual = "";
                        enString = false;
                    }
                    else
                    {
                        if (actual != "")
                        {
                            ClasificarToken(actual, reservadas);
                            actual = "";
                        }

                        enString = true;
                        actual += c;
                    }
                    continue;
                }

                if (enString)
                {
                    actual += c;
                    continue;
                }

                // 🔹 Construcción de tokens
                if (char.IsLetterOrDigit(c))
                {
                    actual += c;
                }
                else
                {
                    if (actual != "")
                    {
                        ClasificarToken(actual, reservadas);
                        actual = "";
                    }

                    if (!char.IsWhiteSpace(c))
                    {
                        // 🔹 Operadores
                        if ("+-*/=".Contains(c))
                        {
                            if (i + 1 < caracteres.Length && caracteres[i + 1] == '=')
                            {
                                txtSalida.AppendText("OPERADOR: " + c + "=" + Environment.NewLine);
                                i++;
                            }
                            else
                            {
                                txtSalida.AppendText("OPERADOR: " + c + Environment.NewLine);
                            }
                        }
                        // 🔹 Delimitadores
                        else if (";(){}".Contains(c))
                        {
                            txtSalida.AppendText("DELIMITADOR: " + c + Environment.NewLine);
                        }
                        // 🔹 Error léxico
                        else
                        {
                            txtSalida.AppendText("ERROR_LEXICO: " + c + Environment.NewLine);
                        }
                    }
                }
            }

            // 🔹 Último token
            if (actual != "")
            {
                ClasificarToken(actual, reservadas);
            }

            // 🔹 Error: string no cerrado
            if (enString)
            {
                txtSalida.AppendText("ERROR_LEXICO: cadena no cerrada" + Environment.NewLine);
            }
        }

        private void ClasificarToken(string token, HashSet<string> reservadas)
        {
            if (token.All(char.IsDigit))
            {
                txtSalida.AppendText("NUMERO: " + token + Environment.NewLine);
            }
            else if (char.IsDigit(token[0]) && token.Any(char.IsLetter))
            {
                txtSalida.AppendText("ERROR_LEXICO: identificador invalido -> " + token + Environment.NewLine);
            }
            else if (reservadas.Contains(token))
            {
                txtSalida.AppendText("PALABRA_RESERVADA: " + token + Environment.NewLine);
            }
            else
            {
                txtSalida.AppendText("IDENTIFICADOR: " + token + Environment.NewLine);
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivos MicroC (*.mc)|*.mc|Archivos de texto (*.txt)|*.txt";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(guardar.FileName, txtEditor.Text);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos MicroC (*.mc)|*.mc|Archivos de texto (*.txt)|*.txt";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtEditor.Text = File.ReadAllText(abrir.FileName);
            }
        }
    }
}