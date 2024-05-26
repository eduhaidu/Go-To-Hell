/**
* Author: Tomás Esconjaureguy (a.k.a selewi)
**/
Shader "Retro/QuakeSkybox"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_MainTexSpeed("Texture Speed", Float) = 2
		_SecondaryTex("Secondary Texture", 2D) = "white" {}
		_SecondaryTexSpeed("Texture Speed", Float) = 5
		_CutOff("Cutoff", Range(0, 1)) = 0
		_SphereSize("Sphere Size", Range(0, 10)) = 5
		_CustomColor("Custom Color", Color) = (1, 1, 1, 1)
		_EmissionColor("Emission Color", Color) = (1, 1, 1, 1)
		_DitherIntensity("Dither Intensity", Range(0, 1)) = 0.5
		_NoiseIntensity("Noise Intensity", Range(0, 1)) = 0.5
		_NoiseScale("Noise Scale", Float) = 1.0
		_NoiseSpeed("Noise Speed", Float) = 1.0
		_GradientColor1("Gradient Color 1", Color) = (1, 0, 0, 1)
		_GradientColor2("Gradient Color 2", Color) = (0, 1, 0, 1)
		_GradientColor3("Gradient Color 3", Color) = (0, 0, 1, 1)
		_GradientIntensity("Gradient Intensity", Range(0, 1)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		ZWrite On ZTest Lequal
		Blend SrcAlpha OneMinusSrcAlpha

		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float3 worldView : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldView = -WorldSpaceViewDir(v.vertex);
				return o;
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _MainTexSpeed;
			sampler2D _SecondaryTex;
			float4 _SecondaryTex_ST;
			float _SecondaryTexSpeed;
			float _CutOff;
			float _SphereSize;
			fixed4 _CustomColor;
			fixed4 _EmissionColor;
			float _DitherIntensity;
			float _NoiseIntensity;
			float _NoiseScale;
			float _NoiseSpeed;
			fixed4 _GradientColor1;
			fixed4 _GradientColor2;
			fixed4 _GradientColor3;
			float _GradientIntensity;

			// Dithering function
			float dither(float2 position, float value)
			{
				int2 gridPos = int2(position) % 4;
				float threshold = frac(sin(dot(float2(gridPos), float2(12.9898, 78.233))) * 43758.5453);
				return step(threshold, value);
			}

			// Simple animated noise function
			float noise(float2 uv, float time)
			{
				float3 uvw = float3(uv, time * _NoiseSpeed);
				return frac(sin(dot(uvw, float3(12.9898, 78.233, 45.164))) * 43758.5453);
			}

			// Function to calculate the gradient color
			fixed4 calculateGradient(float height)
			{
				// Blend between three gradient colors based on height
				if (height < 0.5)
				{
					float t = height * 2.0;
					return lerp(_GradientColor1, _GradientColor2, t);
				}
				else
				{
					float t = (height - 0.5) * 2.0;
					return lerp(_GradientColor2, _GradientColor3, t);
				}
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float3 dir = normalize(float3(i.worldView.x / _SphereSize, i.worldView.y, i.worldView.z / _SphereSize));
				float2 secondaryTexUV = float2(dir.z, dir.x);
					
				secondaryTexUV.x *= _SecondaryTex_ST.x;
				secondaryTexUV.y *= _SecondaryTex_ST.y;
				secondaryTexUV.x += _Time * _SecondaryTexSpeed;

				fixed4 col = tex2D(_SecondaryTex, secondaryTexUV);

				if (col.a < _CutOff) {
					float2 mainTexUV = float2(dir.z, dir.x);

					mainTexUV.x *= _MainTex_ST.x;
					mainTexUV.y *= _MainTex_ST.y;
					mainTexUV.x += _Time * _MainTexSpeed;

					col = tex2D(_MainTex, mainTexUV);
				}

				// Multiply the texture color by the custom color
				col *= _CustomColor;

				// Set the emission color based on the custom color
				col.rgb *= _EmissionColor.rgb;

				// Apply dithering effect
				col.rgb = lerp(col.rgb, dither(i.vertex.xy, col.rgb) * col.rgb, _DitherIntensity);

				// Apply animated noise effect
				float2 noiseUV = float2(i.worldView.x, i.worldView.z) * _NoiseScale;
				float noiseValue = noise(noiseUV, _Time.y);
				col.rgb = lerp(col.rgb, col.rgb * noiseValue, _NoiseIntensity);

				// Apply gradient
				float height = dir.y * 0.5 + 0.5;  // Map y to [0, 1]
				fixed4 gradientCol = calculateGradient(height);
				col.rgb = lerp(col.rgb, gradientCol.rgb, _GradientIntensity);

				return col;
			}
			ENDCG
		}
	}
}
