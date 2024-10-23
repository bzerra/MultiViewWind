using System.Windows;
using System.Windows.Forms;

namespace ScreenViewer
{
    public partial class MainWindow : Window
    {
        private ScreenCaptureService _captureService;

        public MainWindow()
        {
            InitializeComponent();
            _captureService = new ScreenCaptureService();
            LoadScreens();
        }

        private void LoadScreens()
        {
            foreach (var screen in Screen.AllScreens)
            {
                var imageBytes = _captureService.CaptureScreen(screen.Bounds);
                var imageSource = _captureService.ByteArrayToImageSource(imageBytes);
                var imageControl = new System.Windows.Controls.Image { Source = imageSource, Width = 400, Height = 300 };
                ScreensPanel.Children.Add(imageControl); // Adiciona a imagem ao painel
            }
        }
    }
}
