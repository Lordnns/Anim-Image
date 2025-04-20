// UNITY_SHADER_NO_UPGRADE

Shader "8TRD150/AnimVec_Skinned"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            
            #define MAX_BONES 5
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 bones : TEXCOORD1;
                float2 boneWeight: TEXCOORD2;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float4x4 _Bones[MAX_BONES];
            
            v2f vert (appdata v)
            {
                v2f o;

                // Récupère les indices d'os
                int i1 = (int)v.bones.x;
                int i2 = (int)v.bones.y;

                // Récupère les poids
                float w1 = v.boneWeight.x;
                float w2 = v.boneWeight.y;

                // Applique la transformation par chaque os
                float4 p1 = mul(_Bones[i1], v.vertex);
                float4 p2 = mul(_Bones[i2], v.vertex);

                // Linear Blend Skinning
                float4 skinned = p1 * w1 + p2 * w2;

                // Projection
                o.vertex = UnityObjectToClipPos(skinned);
                o.uv = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
