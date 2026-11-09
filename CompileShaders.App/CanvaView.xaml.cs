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
                if (e.Key == System.Windows.Input.Key.Up)
                {
                    if (MainCanvas.Effect is ShaderEffects.InvertHueEffect invertHueEffect)
                    {
                        invertHueEffect.Amount = Math.Min(1.0, invertHueEffect.Amount + 0.01);
                    }
                    if (MainCanvas.Effect is ShaderEffects.TransformLuminosityEffect transformLuminosityEffect)
                    {
                        transformLuminosityEffect.Background = Math.Min(1.0, transformLuminosityEffect.Background + 0.01);
                    }
                }
                if (e.Key == System.Windows.Input.Key.Down)
                {
                    if (MainCanvas.Effect is ShaderEffects.InvertHueEffect invertHueEffect)
                    {
                        invertHueEffect.Amount = Math.Max(0.0, invertHueEffect.Amount - 0.01);
                    }
                    if (MainCanvas.Effect is ShaderEffects.TransformLuminosityEffect transformLuminosityEffect)
                    {
                        transformLuminosityEffect.Background = Math.Max(0.0, transformLuminosityEffect.Background - 0.01);
                    }
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