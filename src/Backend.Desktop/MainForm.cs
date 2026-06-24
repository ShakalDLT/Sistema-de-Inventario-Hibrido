using System;
using System.Drawing;
using System.Windows.Forms;
using Backend.Desktop.Services;

namespace Backend.Desktop
{
    public class MainForm : Form
    {
        private readonly InventarioService _service = new InventarioService();
        
        // Controles con colores legibles para adultos
        private TextBox txtSku = new TextBox { Width = 250, Height = 30, Font = new Font("Segoe UI", 12) };
        private Button btnVender = new Button { Text = "Procesar Venta", Width = 150, Height = 40, BackColor = Color.FromArgb(13, 110, 253), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
        private DataGridView dgvVentas = new DataGridView();
        private Label lblTotal = new Label { Text = "Total: $0", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(33, 37, 41) };

        public MainForm()
        {
            this.Text = "Sistema POS - Gestión de Ventas";
            this.Size = new Size(700, 500);
            this.BackColor = Color.FromArgb(248, 249, 250); // Fondo claro suave

            // Configuración de Tabla Profesional
            dgvVentas.Location = new Point(20, 80);
            dgvVentas.Size = new Size(640, 300);
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.BorderStyle = BorderStyle.FixedSingle;
            
            // Estilo de filas
            dgvVentas.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dgvVentas.RowTemplate.Height = 30;

            // Layout
            txtSku.Location = new Point(20, 25);
            btnVender.Location = new Point(280, 20);
            lblTotal.Location = new Point(20, 400);

            btnVender.Click += BtnVender_Click;

            this.Controls.Add(txtSku);
            this.Controls.Add(btnVender);
            this.Controls.Add(dgvVentas);
            this.Controls.Add(lblTotal);
        }

        private void BtnVender_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSku.Text)) return;
            dgvVentas.Rows.Add(txtSku.Text, "$1.500");
            lblTotal.Text = $"Total: ${dgvVentas.Rows.Count * 1500}";
            txtSku.Clear();
            txtSku.Focus(); // Importante para que el adulto no tenga que hacer clic de nuevo
        }
    }
}