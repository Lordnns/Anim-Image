// UNITY_SHADER_NO_UPGRADE

Shader "8TRD150/Tex_0_Tree4"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _IntensityTex ("Texture d'intensité", 2D) = "white" {}
        _DeformationTex ("Texture de déformation", 2D) = "white" {}
        _Scale ("Facteur d'échelle", Float) = 1.0
        _CustomTime ("Temps personnalisé", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _IntensityTex;
            sampler2D _DeformationTex;
            float _Scale;
            float _CustomTime;
            float4x4 modelMatrix; // Transmis par le contrôleur C#

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                // Calcul de la matrice MVP à partir de la matrice du modèle passée depuis le contrôleur.
                float4x4 mvp = mul(UNITY_MATRIX_VP, modelMatrix);
                o.vertex = mul(mvp, v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 1. Calcul de l'intensité de déformation :
                // On échantillonne la texture d'intensité aux coordonnées (_CustomTime, 0.5)
                float intensity = tex2D(_IntensityTex, float2(_CustomTime, 0.5)).r;
                intensity *= _Scale;
                
                // 2. Définition d'un décalage temporel à l'aide d'une fonction sinusoïdale
                float timeOffset = sin(_CustomTime);
                
                // 3. Récupération du vecteur de déformation depuis la texture de déformation,
                // en ajoutant le décalage temporel aux coordonnées UV d'origine.
                float2 deformationVec = tex2D(_DeformationTex, i.uv + timeOffset).rg;
                deformationVec *= intensity;
                
                // 4. Calcul des nouvelles coordonnées de texture
                float2 newUV = i.uv + deformationVec;
                
                // 5. Échantillonnage de la texture d'entrée avec les nouvelles coordonnées
                fixed4 col = tex2D(_MainTex, newUV);
                return col;
            }
            ENDCG
        }
    }
}