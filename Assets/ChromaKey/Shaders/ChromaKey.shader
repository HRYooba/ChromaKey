﻿Shader "Custom/ChromaKey"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_InputTex("InputTexture", 2D) = "white"{}
		_Color ("DetectionColor", Color) = (0, 0, 0, 1)
		_Threshold ("Threshold", float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _InputTex;
			fixed4 _Color;
			float _Threshold;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 input = tex2D(_InputTex, i.uv);
				
				float R = col.r - _Color.r;
				float G = col.g - _Color.g;
				float B = col.b - _Color.b;
				float dist = sqrt(R * R + G * G + B * B);

				if (dist <= _Threshold) {
					col = input;
				}

				return col;
			}
			ENDCG
		}
	}
}
