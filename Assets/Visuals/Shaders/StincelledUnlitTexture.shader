Shader "Custom/StencilledUnlitTexture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_StencilMask("Stencil Mask", Range(0, 255)) = 255
	}
	SubShader
	{
		LOD 100
		ZTest On
		Cull Off
		Stencil{
			Ref [_StencilMask]
			Comp Equal
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
		  CGPROGRAM
		  #pragma surface surf Lambert

		  struct Input {
			  float2 uv_MainTex;
		  };

		  sampler2D _MainTex;

		  void surf(Input IN, inout SurfaceOutput o) {
			  o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		  }
		  ENDCG
	}
}
