sampler2D input : register(s0);

float amount : register(c0);

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

float4 main(float2 uv : TEXCOORD) : COLOR
{
   float4 color = tex2D(input, uv);

    // Preserve original transparency
    float alpha = color.a;

    // Convert from premultiplied to straight alpha
    float3 rgb = alpha > 0.00001
        ? color.rgb / alpha
        : float3(0.0, 0.0, 0.0);

    // Invert RGB
    float3 inverted = 1.0 - rgb;

    // Blend with original
    float3 blended = lerp(rgb, inverted, amount);

    // RGB -> HSV
    float3 hsv = rgb2hsv(blended);

    // Rotate hue by 180 degrees
    hsv.x = frac(hsv.x + 0.5);

    // HSV -> RGB
    float3 result = hsv2rgb(hsv);

    // Convert back to premultiplied alpha
    color.rgb = result * alpha;
    color.a = alpha;

    return color;
}