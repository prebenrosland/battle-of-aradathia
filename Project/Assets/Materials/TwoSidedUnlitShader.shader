Shader "Custom/TwoSidedUnlitShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        sampler2D _MainTex;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
        }
        ENDCG
    }
    FallBack "Transparent/VertexLit"
}
