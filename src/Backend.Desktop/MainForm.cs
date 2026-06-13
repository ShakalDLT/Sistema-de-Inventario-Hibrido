using System.Windows.Forms;
using System.Drawing;
using Backend.Desktop.Services;

namespace Backend.Desktop
{
    public class MainForm : Form
    {
        private readonly InventarioService _service = new InventarioService();
        private TextBox txtSku = new TextBox();
        private Button btnVender = new Button();
        private Label lblTotal = new Label();

        public MainForm()
        {
            this.Text = "Sistema POS - Inventario";
            this.Size = new Size(600, 400);

            // Configuración del campo de texto SKU
            txtSku.Location = new Point(20, 20);
            txtSku.PlaceholderText = "Ingrese SKU...";
            
            // Botón de venta
            btnVender.Text = "Procesar Venta";
            btnVender.Location = new Point(20, 60);
            btnVender.Click += BtnVender_Click;

            // Etiqueta de resultado
            lblTotal.Location = new Point(20, 100);
            lblTotal.Text = "Esperando venta...";

            // Agregar al formulario
            this.Controls.Add(txtSku);
            this.Controls.Add(btnVender);
            this.Controls.Add(lblTotal);
        }

        private void BtnVender_Click(object? sender, EventArgs e)
        {
            lblTotal.Text = $"Procesando venta para: {txtSku.Text}";
            // Aquí llamarás a _service.RegistrarVenta(...)
        }
    }
}