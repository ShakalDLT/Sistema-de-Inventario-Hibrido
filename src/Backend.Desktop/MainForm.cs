using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Backend.Desktop
{
    // ==========================================
    // CLASE AUXILIAR PARA DISEÑO PROFESIONAL (Bordes Redondeados)
    // ==========================================
    public static class UIHelper
    {
        public static GraphicsPath GetRoundedRectanglePath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (diameter > bounds.Width) diameter = bounds.Width;
            if (diameter > bounds.Height) diameter = bounds.Height;

            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    // ==========================================
    // COMPONENTE PERSONALIZADO: TextBox Redondeado Moderno
    // ==========================================
    public class RoundedTextBox : Panel
    {
        private readonly TextBox txtInner;
        public string? PlaceholderText { get => txtInner.PlaceholderText; set => txtInner.PlaceholderText = value; }
        public bool UseSystemPasswordChar { get => txtInner.UseSystemPasswordChar; set => txtInner.UseSystemPasswordChar = value; }
        
        // Solucionado CS0114: Se agrega 'override' ya que Panel hereda de Control.Text (propiedad virtual)
        public override string Text { get => txtInner.Text; set => txtInner.Text = value; }

        public RoundedTextBox(int width, int height, string placeholder, bool isPassword)
        {
            this.Size = new Size(width, height);
            this.BackColor = ColorTranslator.FromHtml("#1a1d30"); // Color de tarjeta para modo oscuro integrado
            
            int topPadding = (height - 20) / 2;
            this.Padding = new Padding(18, topPadding, 18, 5);

            txtInner = new TextBox
            {
                BorderStyle = BorderStyle.None,
                BackColor = ColorTranslator.FromHtml("#1a1d30"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                Dock = DockStyle.Fill,
                PlaceholderText = placeholder,
                UseSystemPasswordChar = isPassword
            };

            this.Controls.Add(txtInner);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            using (GraphicsPath path = UIHelper.GetRoundedRectanglePath(rect, 14))
            {
                this.Region = new Region(path);
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#2c3150"), 1f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }

    // ==========================================
    // COMPONENTE PERSONALIZADO: Botón Redondeado Estilo Web
    // ==========================================
    public class RoundedButton : Button
    {
        public int Radius { get; set; } = 22;

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            
            using (GraphicsPath path = UIHelper.GetRoundedRectanglePath(rect, Radius))
            {
                this.Region = new Region(path);
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rect, this.ForeColor, 
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    // ==========================================
    // 1. FORMULARIO DE LOGIN
    // ==========================================
    public class LoginForm : Form
    {
        public LoginForm()
        {
            this.Text = "Sistema Inventario - Login";
            this.Size = new Size(440, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#161826"); 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Panel pnlHeader = new Panel { Dock = DockStyle.Top, Height = 130 };
            
            // Contenedor horizontal dinámico para centrar perfectamente el título de dos colores
            FlowLayoutPanel pnlBrand = new FlowLayoutPanel
            {
                Width = 440,
                Height = 40,
                Location = new Point(0, 35),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(65, 0, 0, 0) // Centrado visual calculado para el ancho del formulario
            };

            Label lblBrandLeft = new Label { Text = "SISTEMA ", ForeColor = Color.White, Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Margin = new Padding(0) };
            Label lblBrandRight = new Label { Text = "INVENTARIO", ForeColor = ColorTranslator.FromHtml("#a78bfa"), Font = new Font("Segoe UI", 18, FontStyle.Bold), AutoSize = true, Margin = new Padding(0) };
            
            pnlBrand.Controls.Add(lblBrandLeft);
            pnlBrand.Controls.Add(lblBrandRight);

            Label lblTitle = new Label { Text = "Iniciar Sesión", ForeColor = Color.White, Font = new Font("Segoe UI", 13, FontStyle.Regular), Width = 440, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(0, 80) };
            
            pnlHeader.Controls.Add(pnlBrand);
            pnlHeader.Controls.Add(lblTitle);

            RoundedTextBox txtUser = new RoundedTextBox(300, 46, "Usuario (admin)", false) { Location = new Point(65, 150) };
            RoundedTextBox txtPass = new RoundedTextBox(300, 46, "Contraseña (1234)", true) { Location = new Point(65, 220) };

            RoundedButton btnLogin = new RoundedButton 
            { 
                Text = "Entrar", 
                Location = new Point(65, 310), 
                Width = 300, 
                Height = 46, 
                BackColor = ColorTranslator.FromHtml("#a78bfa"), 
                ForeColor = Color.White, 
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };

            btnLogin.Click += (s, e) =>
            {
                if (txtUser.Text == "admin" && txtPass.Text == "1234")
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Usa admin / 1234", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            this.Controls.Add(pnlHeader);
            this.Controls.Add(txtUser);
            this.Controls.Add(txtPass);
            this.Controls.Add(btnLogin);
        }
    }

    // ==========================================
    // 2. FORMULARIO PRINCIPAL (POS)
    // ==========================================
    public class MainForm : Form
    {
        private readonly Color BgPrimary = ColorTranslator.FromHtml("#161826");
        private readonly Color CardColor = ColorTranslator.FromHtml("#1a1d30");
        private readonly Color Accent = ColorTranslator.FromHtml("#a78bfa");
        private readonly Color HeaderColor = ColorTranslator.FromHtml("#2c3150");

        private readonly Color GridBgColor = Color.White;
        private readonly Color GridLineColor = Color.Blue;
        private readonly Color GridTextColor = Color.Blue;

        // Solucionado CS8618: Se inicializan los campos con null! para satisfacer el chequeo de nulabilidad en el constructor
        private DataGridView dgv = null!;
        private Panel pnlContenido = null!; 
        private Label lblTotal = null!;
        private decimal totalVenta = 0;

        public MainForm()
        {
            this.Text = "Sistema Inventario - Terminal de Ventas";
            this.Size = new Size(1280, 800);
            this.BackColor = BgPrimary;
            this.StartPosition = FormStartPosition.CenterScreen;

            TableLayoutPanel master = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(10) };
            master.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            master.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            master.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            master.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            master.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));

            FlowLayoutPanel nav = new FlowLayoutPanel { Dock = DockStyle.Fill };
            string[] items = { "Venta", "Inventario", "Historial", "Config", "Salir" };
            foreach (var item in items)
            {
                Button btn = new Button { Text = item, Size = new Size(100, 40), BackColor = CardColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) => CambiarVista(item);
                nav.Controls.Add(btn);
            }

            pnlContenido = new Panel { Dock = DockStyle.Fill };
            CrearGridVenta();

            Panel pnlResumen = new Panel { Dock = DockStyle.Fill, BackColor = CardColor };
            lblTotal = new Label { Text = "TOTAL: $0.00", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Accent, Location = new Point(20, 20), AutoSize = true };
            
            Button btnAgregarPrueba = new Button { Text = "+ Agregar Producto", Location = new Point(20, 80), Size = new Size(150, 40), BackColor = HeaderColor, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnAgregarPrueba.Click += (s, e) => AgregarProductoPOS("Producto Ejemplo", 1, 150.50m);

            pnlResumen.Controls.Add(lblTotal);
            pnlResumen.Controls.Add(btnAgregarPrueba);

            FlowLayoutPanel footer = new FlowLayoutPanel { Dock = DockStyle.Fill };
            Button btnCerrar = new Button { Text = "Cobrar / Cerrar", Size = new Size(180, 50), BackColor = Accent, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += (s, e) => { MessageBox.Show($"Cobro realizado por ${totalVenta}", "Éxito"); dgv.Rows.Clear(); ActualizarTotal(); };
            footer.Controls.Add(btnCerrar);

            this.Controls.Add(master);
            master.Controls.Add(nav, 0, 0); master.SetColumnSpan(nav, 2);
            master.Controls.Add(pnlContenido, 0, 1);
            master.Controls.Add(pnlResumen, 1, 1);
            master.Controls.Add(footer, 0, 2); master.SetColumnSpan(footer, 2);
        }

        private void CambiarVista(string vista)
        {
            if (vista == "Salir")
            {
                Application.Exit();
                return;
            }

            pnlContenido.Controls.Clear();

            if (vista == "Venta")
            {
                pnlContenido.Controls.Add(dgv);
            }
            else
            {
                Label lbl = new Label 
                { 
                    Text = $"Pantalla de {vista} en construcción...", 
                    ForeColor = Color.White, 
                    Font = new Font("Segoe UI", 20), 
                    Dock = DockStyle.Fill, 
                    TextAlign = ContentAlignment.MiddleCenter 
                };
                pnlContenido.Controls.Add(lbl);
            }
        }

        private void AgregarProductoPOS(string producto, int cantidad, decimal precioUnitario)
        {
            decimal subtotal = cantidad * precioUnitario;
            dgv.Rows.Add(producto, Math.Max(1, cantidad), $"${precioUnitario}", $"${subtotal}");
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            totalVenta = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                // Solucionado CS8602 y CS8600: Comprobación segura de nulos usando operadores condicionales (?.) y coalescencia (??)
                string valorCelda = row.Cells[3].Value?.ToString()?.Replace("$", "") ?? "0";
                if (decimal.TryParse(valorCelda, out decimal subtotal))
                {
                    totalVenta += subtotal;
                }
            }
            lblTotal.Text = $"TOTAL: ${totalVenta:0.00}";
        }

        private void CrearGridVenta()
        {
            dgv = new DataGridView 
            { 
                Dock = DockStyle.Fill, 
                BackgroundColor = GridBgColor,
                BorderStyle = BorderStyle.None, 
                ReadOnly = true, 
                SelectionMode = DataGridViewSelectionMode.FullRowSelect, 
                MultiSelect = false,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToOrderColumns = false,
                AllowUserToResizeColumns = false, 
                AllowUserToResizeRows = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                GridColor = GridLineColor 
            };

            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = GridLineColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridLineColor;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgv.DefaultCellStyle.BackColor = GridBgColor; 
            dgv.DefaultCellStyle.ForeColor = GridTextColor; 
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.DarkBlue;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgv.Columns.Add("Prod", "Producto");
            dgv.Columns.Add("Cant", "Cantidad");
            dgv.Columns.Add("Precio", "P. Unitario");
            dgv.Columns.Add("Subtotal", "Subtotal");
            
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Solucionado CS8602: Uso de acceso condicional para prevenir excepciones en caso de que la columna retorne null
            if (dgv.Columns["Cant"] != null)
            {
                dgv.Columns["Cant"]!.FillWeight = 50; 
            }

            pnlContenido.Controls.Add(dgv);
        }
    }
}