using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            }
            else
            {
                txtSalida.Text = "Compilación exitosa (simulación)";
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

        private void panelLineas_Paint(object sender, PaintEventArgs e)
        {
            int primeraLinea = txtEditor.GetLineFromCharIndex(txtEditor.GetCharIndexFromPosition(new Point(0, 0)));
            int ultimaLinea = txtEditor.GetLineFromCharIndex(txtEditor.GetCharIndexFromPosition(new Point(0, txtEditor.Height)));

            for (int i = primeraLinea; i <= ultimaLinea + 1; i++)
            {
                int y = txtEditor.GetPositionFromCharIndex(txtEditor.GetFirstCharIndexFromLine(i)).Y;
                e.Graphics.DrawString(
                    (i + 1).ToString(),
                    new Font("Consolas", 10),
                    Brushes.Gray,
                    new PointF(5, y)
                );
            }
        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {
            panelLineas.Invalidate();
        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_VSCROLL = 0x115;
            const int WM_MOUSEWHEEL = 0x20A;

            if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
            {
                panelLineas.Invalidate();
            }
        }
    }
 }
