using System;
using System.Windows;

namespace CompileShaders.App
{
    public partial class CanvaView
    {
        public CanvaView()
        {
            InitializeComponent();
            InitializeWindow();
            this.KeyDown += (s, e) => { 
                if (e.Key == System.Windows.Input.Key.D1)
                {
                    MainCanvas.Effect = new ShaderEffects.InvertEffect();
                }
                if (e.Key == System.Windows.Input.Key.D2)
                {
                    MainCanvas.Effect = new ShaderEffects.InvertHueEffect();
                }
                if (e.Key == System.Windows.Input.Key.D3)
                {
                    MainCanvas.Effect = new ShaderEffects.TransformLuminosityEffect();
                }
                if (e.Key == System.Windows.Input.Key.D0)
                {
                    MainCanvas.Effect = null;
                }
            };
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        #endregion
    }
}