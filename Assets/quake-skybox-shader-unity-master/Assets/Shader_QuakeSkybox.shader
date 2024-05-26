/**
* Author: Tomás Esconjaureguy (a.k.a selewi)
**/
Shader "Retro/QuakeSkybox" // Defines a shader named "Retro/QuakeSkybox"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // Main texture, default is white
        _MainTexSpeed("Texture Speed", Float) = 2 // Speed of the main texture's animation
        _SecondaryTex("Secondary Texture", 2D) = "white" {} // Secondary texture, default is white
        _SecondaryTexSpeed("Texture Speed", Float) = 5 // Speed of the secondary texture's animation
        _CutOff("Cutoff", Range(0, 1)) = 0 // Alpha cutoff for texture blending
        _SphereSize("Sphere Size", Range(0, 10)) = 5 // Size of the sphere for skybox mapping
        _CustomColor("Custom Color", Color) = (1, 1, 1, 1) // Custom color applied to the texture
        _EmissionColor("Emission Color", Color) = (1, 1, 1, 1) // Emission color applied to the texture
        _DitherIntensity("Dither Intensity", Range(0, 1)) = 0.5 // Intensity of the dithering effect
        _NoiseIntensity("Noise Intensity", Range(0, 1)) = 0.5 // Intensity of the noise effect
        _NoiseScale("Noise Scale", Float) = 1.0 // Scale of the noise effect
        _NoiseSpeed("Noise Speed", Float) = 1.0 // Speed of the noise animation
        _GradientColor1("Gradient Color 1", Color) = (1, 0, 0, 1) // First gradient color
        _GradientColor2("Gradient Color 2", Color) = (0, 1, 0, 1) // Second gradient color
        _GradientColor3("Gradient Color 3", Color) = (0, 0, 1, 1) // Third gradient color
        _GradientIntensity("Gradient Intensity", Range(0, 1)) = 0.5 // Intensity of the gradient effect
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" } // Render type tag for the shader
        ZWrite On ZTest Lequal // Enable depth writing and set depth test function to less or equal
        Blend SrcAlpha OneMinusSrcAlpha // Enable alpha blending

        LOD 100 // Level of detail for the shader

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // Specify vertex shader function
            #pragma fragment frag // Specify fragment shader function

            #include "UnityCG.cginc" // Include common Unity shader functions

            struct appdata
            {
                float4 vertex : POSITION; // Vertex position
            };

            struct v2f
            {
                float3 worldView : TEXCOORD1; // World space view direction
                float4 vertex : SV_POSITION; // Screen space vertex position
            };

            // Vertex shader function
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Transform vertex position to clip space
                o.worldView = -WorldSpaceViewDir(v.vertex); // Calculate world space view direction
                return o;
            }

            sampler2D _MainTex; // Main texture sampler
            float4 _MainTex_ST; // Main texture scale and offset
            float _MainTexSpeed; // Main texture animation speed
            sampler2D _SecondaryTex; // Secondary texture sampler
            float4 _SecondaryTex_ST; // Secondary texture scale and offset
            float _SecondaryTexSpeed; // Secondary texture animation speed
            float _CutOff; // Alpha cutoff value
            float _SphereSize; // Sphere size for skybox mapping
            fixed4 _CustomColor; // Custom color
            fixed4 _EmissionColor; // Emission color
            float _DitherIntensity; // Dithering intensity
            float _NoiseIntensity; // Noise intensity
            float _NoiseScale; // Noise scale
            float _NoiseSpeed; // Noise speed
            fixed4 _GradientColor1; // First gradient color
            fixed4 _GradientColor2; // Second gradient color
            fixed4 _GradientColor3; // Third gradient color
            float _GradientIntensity; // Gradient intensity

            // Dithering function
            float dither(float2 position, float value)
            {
                int2 gridPos = int2(position) % 4; // Calculate grid position for dithering
                float threshold = frac(sin(dot(float2(gridPos), float2(12.9898, 78.233))) * 43758.5453); // Calculate threshold
                return step(threshold, value); // Return dithering result
            }

            // Simple animated noise function
            float noise(float2 uv, float time)
            {
                float3 uvw = float3(uv, time * _NoiseSpeed); // Calculate UVW coordinates
                return frac(sin(dot(uvw, float3(12.9898, 78.233, 45.164))) * 43758.5453); // Return noise value
            }

            // Function to calculate the gradient color
            fixed4 calculateGradient(float height)
            {
                // Blend between three gradient colors based on height
                if (height < 0.5)
                {
                    float t = height * 2.0; // Calculate interpolation factor for first blend
                    return lerp(_GradientColor1, _GradientColor2, t); // Return blended color
                }
                else
                {
                    float t = (height - 0.5) * 2.0; // Calculate interpolation factor for second blend
                    return lerp(_GradientColor2, _GradientColor3, t); // Return blended color
                }
            }

            // Fragment shader function
            fixed4 frag(v2f i) : SV_Target
            {
                float3 dir = normalize(float3(i.worldView.x / _SphereSize, i.worldView.y, i.worldView.z / _SphereSize)); // Calculate direction vector
                float2 secondaryTexUV = float2(dir.z, dir.x); // Calculate UV coordinates for secondary texture
                    
                secondaryTexUV.x *= _SecondaryTex_ST.x; // Apply secondary texture scale
                secondaryTexUV.y *= _SecondaryTex_ST.y; // Apply secondary texture scale
                secondaryTexUV.x += _Time * _SecondaryTexSpeed; // Apply secondary texture animation

                fixed4 col = tex2D(_SecondaryTex, secondaryTexUV); // Sample secondary texture

                if (col.a < _CutOff) { // If alpha is below cutoff
                    float2 mainTexUV = float2(dir.z, dir.x); // Calculate UV coordinates for main texture

                    mainTexUV.x *= _MainTex_ST.x; // Apply main texture scale
                    mainTexUV.y *= _MainTex_ST.y; // Apply main texture scale
                    mainTexUV.x += _Time * _MainTexSpeed; // Apply main texture animation

                    col = tex2D(_MainTex, mainTexUV); // Sample main texture
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

                return col; // Return final color
            }
            ENDCG
        }
    }
}
