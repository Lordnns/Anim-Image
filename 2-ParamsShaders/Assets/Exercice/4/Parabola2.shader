Shader "8TRD150/Parabola2"
{
    Properties
    {
        _Speed ("Oscillation Speed", Float) = 1.0
        _Amplitude ("Oscillation Amplitude", Float) = 0.5
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            float _Speed;
            float _Amplitude; 

            v2f vert (appdata v)
            {
                v2f o;
                // osciller le mesh au complet
                float offset = sin(_Time.y * _Speed) * _Amplitude;
                v.vertex.y += offset;

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(1, 1, 0, 1);
            }
            ENDCG
        }
    }
}
