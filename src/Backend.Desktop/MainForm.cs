using System.Windows.Forms;
using System.Drawing;
using Backend.Desktop.Services;

namespace Backend.Desktop
{
    public class MainForm : Form
    {
        private readonly InventarioService _service = new InventarioService();
        private TextBox txtSku = new TextBox { Width = 200, PlaceholderText = "Ingrese SKU..." };
        private Button btnVender = new Button { Text = "Procesar Venta", Width = 120 };
        private DataGridView dgvVentas = new DataGridView();
        private Label lblTotal = new Label { Text = "Total: $0", Font = new Font("Arial", 12, FontStyle.Bold) };

        public MainForm()
        {
            this.Text = "Sistema POS - Control de Inventario";
            this.Size = new Size(650, 450);

            // Configuración del DataGridView (La tabla que da el aspecto profesional)
            dgvVentas.Location = new Point(20, 100);
            dgvVentas.Size = new Size(590, 200);
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.Columns.Add("SKU", "SKU Producto");
            dgvVentas.Columns.Add("Precio", "Precio");

            // Posicionamiento de controles
            txtSku.Location = new Point(20, 20);
            btnVender.Location = new Point(230, 18);
            lblTotal.Location = new Point(20, 320);

            btnVender.Click += BtnVender_Click;

            this.Controls.Add(txtSku);
            this.Controls.Add(btnVender);
            this.Controls.Add(dgvVentas);
            this.Controls.Add(lblTotal);
        }

        private void BtnVender_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSku.Text)) return;
            
            // Simulamos agregar a la tabla
            dgvVentas.Rows.Add(txtSku.Text, "$1.500");
            lblTotal.Text = "Última venta procesada: " + txtSku.Text;
            txtSku.Clear();
        }
    }
}