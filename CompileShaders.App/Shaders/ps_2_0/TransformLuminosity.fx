sampler2D input : register(s0);

// background rgb normalized (0-1)
float background : register(c0);

float3 rgb2hsv(float3 c)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);

    float4 p = lerp(
        float4(c.bg, K.wz),
        float4(c.gb, K.xy),
        step(c.b, c.g));

    float4 q = lerp(
        float4(p.xyw, c.r),
        float4(c.r, p.yzx),
        step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1e-10;

    return float3(
        abs(q.z + (q.w - q.y) / (6.0 * d + e)),
        d / (q.x + e),
        q.x);
}

float3 hsv2rgb(float3 c)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);

    float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);

    return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
}

float transformLuminosity(float luminosity, float backgroundLuminosity)
{
    float haloLuminosity = 82.0 / 85.0;

    float luminosityInversionThreshold = 0.5;

    if (backgroundLuminosity < luminosityInversionThreshold)
    {
        haloLuminosity = 1.0 - haloLuminosity;
        luminosity = 1.0 - luminosity;
    }

    if (luminosity < haloLuminosity)
    {
        return backgroundLuminosity * luminosity / haloLuminosity;
    }

    return
        (1.0 - backgroundLuminosity) *
        (luminosity - 1.0) /
        (1.0 - haloLuminosity) +
        1.0;
}

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);

    float3 hsv = rgb2hsv(color.rgb);

    hsv.z = transformLuminosity(hsv.z, background);

    color.rgb = hsv2rgb(hsv);

    return color;
}