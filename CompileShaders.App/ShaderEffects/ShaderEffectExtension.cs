using System;
using System.Windows.Media.Effects;

namespace CompileShaders.App.ShaderEffects
{
    internal static class ShaderEffectExtension
    {
        public static PixelShader CreatePixelShader(this ShaderEffect shaderEffect, string resourcePath)
        {
            var uriString = $"/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name};component/{resourcePath}";
            var pixelShader = new PixelShader()
            {
                UriSource = new Uri(uriString, UriKind.Relative)
            };
            return pixelShader;
        }
    }
}