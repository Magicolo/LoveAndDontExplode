Shader "Game/BulletShader"
{
	Properties
	{
		_CoolDownColour("CoolDown Colour", Color) = (1.0, 1.0, 1.0, 1.0)
		_CoolDownRatio("CoolDown Ratio", Float) = 0.2
		_CoolDownThickness("CoolDown Thickness", Float) = 0.01

		_AmmoColour("Ammo Colour", Color) = (1.0, 1.0, 1.0, 1.0)
		_AmmoRatio("Ammo Ratio", Float) = 0.4
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			uniform float4 _CoolDownColour;
			uniform float _CoolDownThickness;
			uniform float _CoolDownRatio;

			uniform float4 _AmmoColour;
			uniform float _AmmoRatio;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 wp : TEXCOORD0;
				float2 uv : TEXCOORD1;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.wp = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target
			{
				
				if ((i.uv.x < _CoolDownThickness / 2 || i.uv.x  > 1 - _CoolDownThickness / 2) || (i.uv.y < _CoolDownThickness / 2 || i.uv.y > 1 - _CoolDownThickness / 2))
				{
					if (i.uv.y < _CoolDownRatio)
						return _CoolDownColour;
					else
						return float4(0, 0, 0, 0);
				}
				else
				{
					if (i.uv.y < _AmmoRatio)
						return _AmmoColour;
					else
						return float4(0, 0, 0, 0);
				}

				//UNITY_APPLY_FOG(i.fogCoord, col);
				//return col;
			}
			ENDCG
		}
	}
}
