sampler2D input : register(s0);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv);

    float alpha = color.a;
    float3 rgb = alpha > 0.00001 ? color.rgb / alpha : float3(0.0, 0.0, 0.0);

    rgb = 1.0 - rgb;

    color.rgb = rgb * alpha;
    color.a = alpha;

    return color;
}