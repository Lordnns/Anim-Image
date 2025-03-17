// UNITY_SHADER_NO_UPGRADE

Shader "8TRD150/TriangleColorShader"
{
    Properties
    {
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

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color  : COLOR;
            };
            
            float4x4 modelMatrix;

            v2f vert (appdata v)
            {
                v2f o;
                float4x4 mvp = mul(UNITY_MATRIX_VP, modelMatrix);
                o.vertex = mul(mvp, v.vertex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }
}
