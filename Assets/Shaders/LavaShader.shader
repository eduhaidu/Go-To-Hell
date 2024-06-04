Shader "Custom/LavaShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _LavaColor ("Lava Color", Color) = (1, 0.5, 0, 1)
        _EmissiveColor ("Emissive Color", Color) = (1, 0.2, 0, 1)
        _Speed ("Flow Speed", Range(0.1, 10)) = 1
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.1
        _EmissiveIntensity ("Emissive Intensity", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LavaColor;
            float4 _EmissiveColor;
            float _Speed;
            float _DistortionStrength;
            float _EmissiveIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float2 FlowUV(float2 uv, float time, float speed, float strength)
            {
                float2 panningUV = uv + float2(time * speed, time * speed);
                float2 distortion = float2(sin(panningUV.x * 10.0) * strength, cos(panningUV.y * 10.0) * strength);
                return uv + distortion;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float time = _Time.y; // Get the current time
                float2 flowUV = FlowUV(i.uv, time, _Speed, _DistortionStrength);
                fixed4 col = tex2D(_MainTex, flowUV) * _LavaColor;

                // Calculate luminance to create a mask
                float luminance = dot(col.rgb, float3(0.299, 0.587, 0.114));
                float emissiveMask = smoothstep(0.3, 1.0, luminance); // Adjust threshold as needed

                // Apply emissive color based on the luminance mask
                fixed4 emissive = _EmissiveColor * _EmissiveIntensity * emissiveMask;
                col.rgb += emissive.rgb;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
