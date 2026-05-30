using System.Windows;
using System.Windows.Media.Effects;

namespace CompileShaders.App.ShaderEffects
{
    public class InvertHueEffect : ShaderEffect
    {
        public InvertHueEffect()
        {
            PixelShader = this.CreatePixelShader("Shaders/InvertHue.ps");
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(AmountProperty);
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty(
                "Input", typeof(InvertHueEffect), 0);

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(double),
                typeof(InvertHueEffect),
                new UIPropertyMetadata(0.93, PixelShaderConstantCallback(0)));

        public double Amount
        {
            get => (double)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

    }
}