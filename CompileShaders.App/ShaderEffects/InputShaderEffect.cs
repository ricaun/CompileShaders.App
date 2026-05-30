using System;
using System.Windows;
using System.Windows.Media.Effects;

namespace CompileShaders.App.ShaderEffects
{
    public abstract class InputShaderEffect : ShaderEffect
    {
        private static PixelShader CreatePixelShader(string resourcePath)
        {
            var uriString = $"/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name};component/{resourcePath}";
            return new PixelShader()
            {
                UriSource = new Uri(uriString, UriKind.Relative)
            };
        }

        public InputShaderEffect(string resourcePath)
        {
            PixelShader = CreatePixelShader(resourcePath);
            UpdateShaderValue(InputProperty);
        }

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty(
                "Input", typeof(InvertEffect), 0);
    }
}