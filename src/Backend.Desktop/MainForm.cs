using System;
using System.Drawing;
using System.Windows.Forms;
using Backend.Desktop.Services;

namespace Backend.Desktop
{
    public class MainForm : Form
    {
        // Paleta de colores "Shakal Studio"
        private readonly Color BackColorDark = ColorTranslator.FromHtml("#161b22");
        private readonly Color SidebarColor = ColorTranslator.FromHtml("#0d1117");
        private readonly Color AccentColor = ColorTranslator.FromHtml("#bc8cff");

        private readonly InventarioService _service = new InventarioService();
        private TextBox txtSku = new TextBox { Width = 200, BackColor = Color.FromArgb(30, 30, 30), ForeColor = Color.White };
        private Button btnVender = new Button { Text = "Procesar Venta", BackColor = Color.FromArgb(60, 60, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
        private DataGridView dgvVentas = new DataGridView();
        private Label lblTotal = new Label { Text = "Total: $0", ForeColor = Color.FromArgb(188, 140, 255), Font = new Font("Segoe UI", 14, FontStyle.Bold) };

        public MainForm()
        {
            this.Text = "Sistema POS - Shakal Studio";
            this.Size = new Size(700, 450);
            this.BackColor = BackColorDark;

            // Configuración del DataGridView
            dgvVentas.Location = new Point(20, 80);
            dgvVentas.Size = new Size(640, 250);
            dgvVentas.BackgroundColor = SidebarColor;
            dgvVentas.DefaultCellStyle.BackColor = BackColorDark;
            dgvVentas.DefaultCellStyle.ForeColor = Color.White;
            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = SidebarColor;
            dgvVentas.EnableHeadersVisualStyles = false;
            
            dgvVentas.Columns.Add("SKU", "SKU Producto");
            dgvVentas.Columns.Add("Precio", "Precio");

            txtSku.Location = new Point(20, 30);
            btnVender.Location = new Point(230, 28);
            btnVender.FlatAppearance.BorderColor = AccentColor;
            
            lblTotal.Location = new Point(20, 350);

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
            txtSku.Clear();
        }
    }
}