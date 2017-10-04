Shader "Self/Testshader"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		Subshader
	{
		Tags{ "Queue" = "Transparent" }
		Pass
	{
		Lighting On
		Blend SrcAlpha OneMinusSrcAlpha

		Material{
		Diffuse(1,1,1,1)
	}

		SetTexture[_MainTex]{
		Combine texture * primary, texture * primary
	}
	}
	}
}