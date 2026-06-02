# CompileShaders.App

This project have a simple implementation of the `ShaderEffects` for WPF.

To build a shader is necessary to have the `fxc.exe` tool ([Effect-Compiler](https://learn.microsoft.com/en-us/windows/win32/direct3dtools/fxc)), which is part of the Windows SDK. The project contain a copy from the [Windows Software Development Kit (SDK)](https://developer.microsoft.com/pt-br/windows/downloads/windows-10-sdk/) version `10.0.22621.0` / `x64`.

The `.fx` files are compiled using `fxc.exe` with the following command:
```cmd
fxc.exe /T ps_2_0 /E main /Fo "OutputFileName.ps" "InputFileName.fx"
```

The targer `CompileShaders` is used to compile all the `.fx` files in `.ps` files when the project is built.

All `.ps` files is included as `Resource` in the project, and the `ShaderEffect` classes load the shader from the resource stream.

## InvertEffect

Simple inversion color effect implemented as a `ShaderEffect` in WPF. The shader code is written in HLSL and compiled to a pixel shader.

```C#
// InvertEffect.cs
public class InvertEffect : ShaderEffect
{
    public InvertEffect()
    {
        PixelShader = this.CreatePixelShader("Shaders/Invert.ps");
        UpdateShaderValue(InputProperty);
    }

    public static readonly DependencyProperty InputProperty =
        ShaderEffect.RegisterPixelShaderSamplerProperty(
            "Input", typeof(InvertEffect), 0);
}
```

```fx
// Invert.fx
sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);
    color.rgb = 1.0 - color.rgb;
    return color;
}
```

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!