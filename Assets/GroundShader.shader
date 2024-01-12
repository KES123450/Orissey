Shader "Custom/GroundShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}

    }
        SubShader
    {
        Tags { "RenderType" = "Opaque"}
        LOD 200


        //1Pass
        CGPROGRAM
        #pragma surface surf Standard vertex:vert noshadow no ambient

        sampler2D _MainTex;
        fixed4 _Color;

        void vert(inout appdata_full v) {
             v.vertex.y = v.vertex.y + 0.05f;
        }

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Emission = float4(0.15, 0.001, 0, 1);
            o.Alpha = c.a;
        }
        ENDCG


            //2Pass
            CGPROGRAM
            #pragma surface surf Standard 

            sampler2D _MainTex;
            fixed4 _Color;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Emission = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
    }
        FallBack "Diffuse"
}