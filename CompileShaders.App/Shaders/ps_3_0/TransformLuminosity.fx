sampler2D input : register(s0);
float background : register(c0);

float3 RGBToHSL(float3 color)
{
    float maxValue = max(color.r, max(color.g, color.b));
    float minValue = min(color.r, min(color.g, color.b));

    float luminosity = (maxValue + minValue) * 0.5;
    float delta = maxValue - minValue;

    float hue = 0.0;
    float saturation = 0.0;

    if (delta > 0.0)
    {
        saturation = delta / (1.0 - abs(2.0 * luminosity - 1.0));

        if (maxValue == color.r)
            hue = fmod((color.g - color.b) / delta, 6.0);
        else if (maxValue == color.g)
            hue = (color.b - color.r) / delta + 2.0;
        else
            hue = (color.r - color.g) / delta + 4.0;

        hue /= 6.0;

        if (hue < 0.0)
            hue += 1.0;
    }

    return float3(hue, saturation, luminosity);
}

float3 HSLToRGB(float3 hsl)
{
    float hue = hsl.x;
    float saturation = hsl.y;
    float luminosity = hsl.z;

    float chroma = (1.0 - abs(2.0 * luminosity - 1.0)) * saturation;
    float x = chroma * (1.0 - abs(fmod(hue * 6.0, 2.0) - 1.0));
    float m = luminosity - chroma * 0.5;

    float3 rgb;

    if (hue < 1.0 / 6.0)
        rgb = float3(chroma, x, 0.0);
    else if (hue < 2.0 / 6.0)
        rgb = float3(x, chroma, 0.0);
    else if (hue < 3.0 / 6.0)
        rgb = float3(0.0, chroma, x);
    else if (hue < 4.0 / 6.0)
        rgb = float3(0.0, x, chroma);
    else if (hue < 5.0 / 6.0)
        rgb = float3(x, 0.0, chroma);
    else
        rgb = float3(chroma, 0.0, x);

    return rgb + m;
}

float TransformLuminosity(float luminosity, float backgroundLuminosity)
{
    const float HaloLuminosity = 82.0 / 85.0;
    const float LuminosityInversionThreshold = 0.5;

    float haloLuminosity = HaloLuminosity;

    if (backgroundLuminosity < LuminosityInversionThreshold)
    {
        haloLuminosity = 1.0 - haloLuminosity;
        luminosity = 1.0 - luminosity;
    }

    if (luminosity < haloLuminosity)
    {
        return backgroundLuminosity * luminosity / haloLuminosity;
    }

    return (1.0 - backgroundLuminosity)
        * (luminosity - 1.0)
        / (1.0 - haloLuminosity)
        + 1.0;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);

    float3 hsl = RGBToHSL(color.rgb);

    hsl.z = TransformLuminosity(hsl.z, background);

    float3 result = HSLToRGB(hsl);

    return float4(result, color.a);
}