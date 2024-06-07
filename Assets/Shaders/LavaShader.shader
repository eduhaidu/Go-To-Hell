Shader "Custom/LavaShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _LavaColor ("Lava Color", Color) = (1, 0.5, 0, 1)
        _Speed ("Flow Speed", Range(0.1, 10)) = 1
        _DistortionStrength ("Distortion Strength", Range(0, 1)) = 0.1
        _TextureScale ("Texture Scale", Float) = 1
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
            float _Speed;
            float _DistortionStrength;
            float _TextureScale;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                // Calculate world space UVs to keep texture size consistent
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                float3 objectScale = float3(length(unity_ObjectToWorld[0].xyz), length(unity_ObjectToWorld[1].xyz), length(unity_ObjectToWorld[2].xyz));
                o.uv = worldPos.xz / (objectScale.xz * _TextureScale);

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

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
