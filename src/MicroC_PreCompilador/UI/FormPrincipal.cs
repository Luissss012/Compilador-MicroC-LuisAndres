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

            AnalizadorLexico analizador = new AnalizadorLexico();

            List<Token> resultado = analizador.Analizar(txtEditor.Text);

            foreach (Token token in resultado)
            {
                txtSalida.AppendText(
                    "Línea: " + token.Linea +
                    " | Token: " + token.Codigo +
                    " | Lexema: " + token.Lexema +
                    " | Tipo: " + token.Tipo +
                    Environment.NewLine
                );
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