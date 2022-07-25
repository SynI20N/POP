Shader "Custom/Building"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _PercentFinished ("Height Finished", Range(-10,10)) = 0.0
        _ConstructionColor ("Construction Color", Color) = (0,0,1,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Cull Back
        CGPROGRAM
        #pragma surface surf Standard alpha:blend

        #pragma target 2.5

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        half _PercentFinished;
        fixed4 _Color;
        fixed4 _ConstructionColor;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = pow(tex2D(_MainTex, IN.uv_MainTex), _ConstructionColor);
            if (IN.worldPos.y > _PercentFinished)
            {
                o.Alpha = 0.5;
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
            }
            else
            {
                discard;
            }
            o.Albedo = c.rgb;
        }

        ENDCG
    }
}
