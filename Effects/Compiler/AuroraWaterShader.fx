float time;
float2 screenSize;
float2 offset;

texture sampleTexture;
sampler2D samplerTex = sampler_state { texture = <sampleTexture>; magfilter = LINEAR; minfilter = LINEAR; mipfilter = LINEAR; AddressU = wrap; AddressV = wrap; };

texture sampleTexture2;
sampler2D samplerTex2 = sampler_state { texture = <sampleTexture2>; magfilter = LINEAR; minfilter = LINEAR; mipfilter = LINEAR; AddressU = wrap; AddressV = wrap; };

float4 PixelShaderFunction(float4 screenSpace : TEXCOORD0) : COLOR0
{
    float2 st = screenSpace.xy;
    float2 off = float2(sin(time + st.y * 100.0 + offset.y * 100.0), sin(time + st.x * 100.0 + offset.x * 100.0)) / screenSize;

    float map = tex2D(samplerTex2, st + off).r;
    float4 color = tex2D(samplerTex, st + off);

    float progress = (st.x + st.y) * 2.0 * map;

    float r = 10.0 * (1.0 + sin(time + progress * 0.2));
    float g = 14.0 * (1.0 + cos(time + progress));
    float b = 18.0;

    float3 colorB = float3(r, g, b);
    float3 color2 = colorB * 0.02 * (color.r + color.b * 2.0);

    return float4(color2, color.a) * map;
}

technique Technique1
{
    pass PrimitivesPass
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
};