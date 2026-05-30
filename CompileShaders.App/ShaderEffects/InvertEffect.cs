using System;

namespace CompileShaders.App.ShaderEffects
{
    public class InvertEffect : InputShaderEffect
    {
        public InvertEffect() : base("Shaders/Invert.ps")
        {
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