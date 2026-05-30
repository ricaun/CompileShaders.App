using System.Windows;
using System.Windows.Media.Effects;

namespace CompileShaders.App.ShaderEffects
{
    public class TransformLuminosityEffect : ShaderEffect
    {
        public TransformLuminosityEffect()
        {
            PixelShader = this.CreatePixelShader("Shaders/TransformLuminosity.ps");
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(BackgroundProperty);
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty(
                "Input", typeof(TransformLuminosityEffect), 0);

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                nameof(Background),
                typeof(double),
                typeof(TransformLuminosityEffect),
                new UIPropertyMetadata(
                    36.0 / 255.0,
                    PixelShaderConstantCallback(0)));

        public double Background
        {
            get => (double)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }
    }

    //public class TransformLuminosityEffect : InputShaderEffect
    //{
    //    public TransformLuminosityEffect() : base("Shaders/TransformLuminosity.ps")
    //    {
    //        UpdateShaderValue(BackgroundProperty);
    //    }

    //    public static readonly DependencyProperty BackgroundProperty =
    //        DependencyProperty.Register(
    //            nameof(Background),
    //            typeof(double),
    //            typeof(TransformLuminosityEffect),
    //            new UIPropertyMetadata(
    //                36.0 / 255.0,
    //                PixelShaderConstantCallback(0)));

    //    public double Background
    //    {
    //        get => (double)GetValue(BackgroundProperty);
    //        set => SetValue(BackgroundProperty, value);
    //    }
    //}
}