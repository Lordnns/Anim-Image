Shader "8TRD150/Terrain"
{
    Properties
    {
        _HeightMap ("Height Map", 2D) = "white" {}
        _ColorRamp ("Color Gradient (by Height)", 2D) = "white" {}
        _HeightScale ("Height Scale", Float) = 1.0
        _HeightMin ("Min Height", Float) = 0.0
        _HeightMax ("Max Height", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        sampler2D _HeightMap;
        sampler2D _ColorRamp;
        float _HeightScale;
        float _HeightMin;
        float _HeightMax;

        struct Input
        {
            float2 uv_HeightMap;
            float heightValue;
        };

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);

            float2 uv = v.texcoord.xy;
            
            float height = tex2Dlod(_HeightMap, float4(uv, 0, 0)).r;

            // Apply height displacement
            float worldHeight = height * _HeightScale;
            v.vertex.z += worldHeight;
            
            o.heightValue = saturate((worldHeight - _HeightMin) / (_HeightMax - _HeightMin));
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Use the height as X to sample the gradient
            float2 rampUV = float2(IN.heightValue, 0.5);
            fixed4 col = tex2D(_ColorRamp, rampUV);

            o.Albedo = col.rgb;
        }
        ENDCG
    }

    FallBack "Diffuse"
}
