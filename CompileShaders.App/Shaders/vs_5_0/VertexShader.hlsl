cbuffer ConstantBuffer : register(b0)
{
    matrix World;
    matrix View;
    matrix Projection;
};

struct VS_INPUT
{
    float3 Position : POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
};

struct VS_OUTPUT
{
    float4 Position : SV_POSITION;
    float3 Normal   : NORMAL;
    float2 TexCoord : TEXCOORD0;
};

VS_OUTPUT main(VS_INPUT input)
{
    VS_OUTPUT output;

    float4 worldPosition = mul(float4(input.Position, 1.0f), World);
    float4 viewPosition  = mul(worldPosition, View);

    output.Position = mul(viewPosition, Projection);

    output.Normal = mul(float4(input.Normal, 0.0f), World).xyz;
    output.TexCoord = input.TexCoord;

    return output;
}