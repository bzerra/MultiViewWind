using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MultiView.App3
{
    public partial class Form1 : Form
    {
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _timer = new Timer
            {
                Interval = 1000 // Captura a cada segundo
            };
            _timer.Tick += (s, args) => CaptureScreens();
            _timer.Start();
        }

        private void CaptureScreens()
        {
            var screens = Screen.AllScreens;
            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            foreach (var screen in screens)
            {
                Bitmap bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(screen.Bounds.Location, Point.Empty, screen.Bounds.Size);
                }

                PictureBox pictureBox = new PictureBox
                {
                    Image = bitmap,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Width = 250,
                    Height = 150,
                    Margin = new Padding(10)                    
                };

                // Cria o Label para a legenda
                Label label = new Label
                {
                    Text = $"Tela {Array.IndexOf(screens, screen) + 1}",
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black, // Cor do texto
                    Margin = new Padding(10)
                };

                // Cria um painel para agrupar o PictureBox e o Label
                FlowLayoutPanel itemPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.TopDown,
                    Width = 250,
                    Margin = new Padding(10),
                    AutoSize = true // Permite que o painel ajuste seu tamanho
                };

                // Adiciona o PictureBox e o Label ao painel
                itemPanel.Controls.Add(pictureBox);
                itemPanel.Controls.Add(label);

                // Adiciona o painel item ao painel principal
                panel.Controls.Add(itemPanel);
            }

            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    Controls.Clear();
                    Controls.Add(panel);
                }));
            }
            else
            {
                Controls.Clear();
                Controls.Add(panel);
            }
        }
    }
}
