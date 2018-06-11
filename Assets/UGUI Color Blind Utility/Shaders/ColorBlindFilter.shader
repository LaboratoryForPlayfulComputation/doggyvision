// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/ColorBlindFilter" 
{
	Properties 
	{
		_MainTex("", any) = "" {}
	}
		
	CGINCLUDE
	#include "UnityCG.cginc"
	
	struct v2f 
	{
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	uniform float4x4 _Filter;
	
	v2f vert(appdata_img v) 
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv =  v.texcoord.xy;
		return o;
	}
	
	half4 frag(v2f i) : COLOR 
	{
		half4 original = tex2D(_MainTex, i.uv);
		half4 processed = mul(_Filter, original);
		return processed;
	}
	
	ENDCG
	
	Subshader 
	{
		Pass 
		{
			ZTest Always 
			Cull Off 
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}

	Fallback Off
}