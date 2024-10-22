Shader "Custom/RimLight"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BumpMap("Bumpmap", 2D) = "bump" {}
        _RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
        _RimPower("Rim Power", Range(0,10.0)) = 3.0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
        }
        CGPROGRAM
        #pragma surface surf Lambert
        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
        };

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float4 _RimColor;
        float _RimPower;

        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));

            // Rim Power가 커질수록 RimColor가 검정에 가까워지게
            float rimIntensity = pow(rim, _RimPower);
            float colorScale = 1.0 - saturate(_RimPower / 10.0); // _RimPower가 클수록 0에 가까워짐
            float3 adjustedRimColor = _RimColor.rgb * colorScale;

            o.Emission = adjustedRimColor * rimIntensity;
        }
        ENDCG
    }
    Fallback "Diffuse"
}