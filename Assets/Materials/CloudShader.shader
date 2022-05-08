Shader "Custom/Cloud Diffuse" {
    Properties{
        _MainTex("Base (RGB)", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "white" {}
        _NoiseScl("Noise Scale", Range(0.1, 20)) = 1

        _Speed("Speed", Range(0.01, 2)) = 1
        _OtlWidth("Width", Range(0,20)) = 1
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            //Offset -2, -2
            LOD 150

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert noforwardadd

        sampler2D _MainTex, _NoiseTex;
        float _NoiseScl, _OtlWidth, _Speed;

        struct Input {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            float btime = _Time.x * _Speed;
            float2 pos = float2(v.vertex.x, v.vertex.y) / _NoiseScl + float2(btime, btime);
            //float noise = (tex2D(_NoiseTex, float2(btime, btime) * float2(-0.9, 0.8) + pos).r + tex2D(_NoiseTex, float2(btime * 1.1, btime * 1.2) * float2(0.8, -1.0) + pos).r) / 2.0;
            float noise = tex2Dlod(_NoiseTex, float4(pos.x, pos.y, 0.0, 0.0)).r;
            v.vertex.xyz += normalize(v.normal.xyz) * _OtlWidth * (noise * 2.0 - 1.0) * 0.008;
        }

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

        
        ENDCG
    }

        Fallback "Mobile/Diffuse"
}