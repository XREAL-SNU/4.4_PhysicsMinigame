Shader "Unlit/Barrier"
{
	Properties
	{
		_GridColor("Grid Color", Color) = (1,1,1,1)
		_MeshColor("Mesh Color", Color) = (0,0,0,0)
		_GridSize("Spacing", Float) = 1
		_LineThickness("Line Thickness", Float) = 0.03
		_Offset("Offset", Range(0, 1)) = 0
		_Speed("Speed", Range(0, 2)) = 1
		_FlashSpeed("Flash Speed", Range(0, 2)) = 1
	}

		SubShader
	{
		Tags { "Queue" = "Transparent"}
		LOD 100
		//ZWrite Off

		Pass
		{
			//Blend SrcAlpha OneMinusSrcAlpha
			Blend SrcAlpha One
			//Offset -20, -20

			CGPROGRAM
#define IF(a, b, c) lerp(b, c, step((fixed) (a), 0));
#pragma vertex vert
#pragma fragment frag

# include "UnityCG.cginc"

			fixed4 _GridColor, _MeshColor;
			float _GridSize, _LineThickness, _Offset, _Speed, _FlashSpeed;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = mul(unity_ObjectToWorld, v.vertex).xz;
				return o;
			}

			float DrawGrid(float2 uv, float sz, float aa)
			{
				float aaThresh = aa;
				float aaMin = aa * 0.1;
				float tt = uv.x + uv.y + _Offset + _Time.z * _Speed;

				float2 gUV = float2(tt, tt) / sz + aaThresh;

				float2 fl = floor(gUV);
				gUV = frac(gUV);
				gUV -= aaThresh;
				gUV = smoothstep(aaThresh, aaMin, abs(gUV));
				float d = max(gUV.x, gUV.y);

				return d;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed r = DrawGrid(i.uv, _GridSize, _LineThickness);
			fixed4 c = IF(
				r > 0
				, _GridColor
				, _MeshColor
			);
			c.a *= 0.75 + 0.25 * sin(_Time.z * _FlashSpeed);
				return c;
			}

			ENDCG
		}
	}
}
