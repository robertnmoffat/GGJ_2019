Shader "Marrt/NESDownSample" 
{
	Properties
	{
		_Color00 ("Color 00", Color) = (0.48627,0.48627,0.48627)
		_Color01 ("Color 01", Color) = (0.00000,0.00000,0.98824)
		_Color02 ("Color 02", Color) = (0.00000,0.00000,0.73725)
		_Color03 ("Color 03", Color) = (0.26667,0.15686,0.73725)
		_Color04 ("Color 04", Color) = (0.58039,0.00000,0.51765)
		_Color05 ("Color 05", Color) = (0.65882,0.00000,0.12549)
		_Color06 ("Color 06", Color) = (0.65882,0.06275,0.00000)
		_Color07 ("Color 07", Color) = (0.53333,0.07843,0.00000)
		_Color08 ("Color 08", Color) = (0.31373,0.18824,0.00000)
		_Color09 ("Color 09", Color) = (0.00000,0.47059,0.00000)
		_Color10 ("Color 10", Color) = (0.00000,0.40784,0.00000)
		_Color11 ("Color 11", Color) = (0.00000,0.34510,0.00000)
		_Color12 ("Color 12", Color) = (0.00000,0.25098,0.34510)
		_Color13 ("Color 13", Color) = (0.00000,0.00000,0.00000)
		_Color14 ("Color 14", Color) = (0.00000,0.00000,0.00000)
		_Color15 ("Color 15", Color) = (0.00000,0.00000,0.00000)
		_Color16 ("Color 16", Color) = (0.73725,0.73725,0.73725)
		_Color17 ("Color 17", Color) = (0.00000,0.47059,0.97255)
		_Color18 ("Color 18", Color) = (0.00000,0.34510,0.97255)
		_Color19 ("Color 19", Color) = (0.40784,0.26667,0.98824)
		_Color20 ("Color 20", Color) = (0.84706,0.00000,0.80000)
		_Color21 ("Color 21", Color) = (0.89412,0.00000,0.34510)
		_Color22 ("Color 22", Color) = (0.97255,0.21961,0.00000)
		_Color23 ("Color 23", Color) = (0.89412,0.36078,0.06275)
		_Color24 ("Color 24", Color) = (0.67451,0.48627,0.00000)
		_Color25 ("Color 25", Color) = (0.00000,0.72157,0.00000)
		_Color26 ("Color 26", Color) = (0.00000,0.65882,0.00000)
		_Color27 ("Color 27", Color) = (0.00000,0.65882,0.26667)
		_Color28 ("Color 28", Color) = (0.00000,0.53333,0.53333)
		_Color29 ("Color 29", Color) = (0.00000,0.00000,0.00000)
		_Color30 ("Color 30", Color) = (0.00000,0.00000,0.00000)
		_Color31 ("Color 31", Color) = (0.00000,0.00000,0.00000)
		_Color32 ("Color 32", Color) = (0.97255,0.97255,0.97255)
		_Color33 ("Color 33", Color) = (0.23529,0.73725,0.98824)
		_Color34 ("Color 34", Color) = (0.40784,0.53333,0.98824)
		_Color35 ("Color 35", Color) = (0.59608,0.47059,0.97255)
		_Color36 ("Color 36", Color) = (0.97255,0.47059,0.97255)
		_Color37 ("Color 37", Color) = (0.97255,0.34510,0.59608)
		_Color38 ("Color 38", Color) = (0.97255,0.47059,0.34510)
		_Color39 ("Color 39", Color) = (0.98824,0.62745,0.26667)
		_Color40 ("Color 40", Color) = (0.97255,0.72157,0.00000)
		_Color41 ("Color 41", Color) = (0.72157,0.97255,0.09412)
		_Color42 ("Color 42", Color) = (0.34510,0.84706,0.32941)
		_Color43 ("Color 43", Color) = (0.34510,0.97255,0.59608)
		_Color44 ("Color 44", Color) = (0.00000,0.90980,0.84706)
		_Color45 ("Color 45", Color) = (0.47059,0.47059,0.47059)
		_Color46 ("Color 46", Color) = (0.00000,0.00000,0.00000)
		_Color47 ("Color 47", Color) = (0.00000,0.00000,0.00000)
		_Color48 ("Color 48", Color) = (0.98824,0.98824,0.98824)
		_Color49 ("Color 49", Color) = (0.64314,0.89412,0.98824)
		_Color50 ("Color 50", Color) = (0.72157,0.72157,0.97255)
		_Color51 ("Color 51", Color) = (0.84706,0.72157,0.97255)
		_Color52 ("Color 52", Color) = (0.97255,0.72157,0.97255)
		_Color53 ("Color 53", Color) = (0.97255,0.64314,0.75294)
		_Color54 ("Color 54", Color) = (0.94118,0.81569,0.69020)
		_Color55 ("Color 55", Color) = (0.98824,0.87843,0.65882)
		_Color56 ("Color 56", Color) = (0.97255,0.84706,0.47059)
		_Color57 ("Color 57", Color) = (0.84706,0.97255,0.47059)
		_Color58 ("Color 58", Color) = (0.72157,0.97255,0.72157)
		_Color59 ("Color 59", Color) = (0.72157,0.97255,0.84706)
		_Color60 ("Color 60", Color) = (0.00000,0.98824,0.98824)
		_Color61 ("Color 61", Color) = (0.97255,0.84706,0.97255)
		_Color62 ("Color 62", Color) = (0.00000,0.00000,0.00000)
		_Color63 ("Color 63", Color) = (0.00000,0.00000,0.00000)

	 	_MainTex ("", 2D) = "white" {}
	}
	 
	SubShader
	{
		Lighting Off
		ZTest Always
		Cull Off
		ZWrite Off
		Fog { Mode Off }
		
		Pass
		{
			CGPROGRAM
			#pragma exclude_renderers flash
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
		
			uniform fixed4 _Color00;
			uniform fixed4 _Color01;
			uniform fixed4 _Color02;
			uniform fixed4 _Color03;
			uniform fixed4 _Color04;
			uniform fixed4 _Color05;
			uniform fixed4 _Color06;
			uniform fixed4 _Color07;
			uniform fixed4 _Color08;
			uniform fixed4 _Color09;
			uniform fixed4 _Color10;
			uniform fixed4 _Color11;
			uniform fixed4 _Color12;
			uniform fixed4 _Color13;
			uniform fixed4 _Color14;
			uniform fixed4 _Color15;
			uniform fixed4 _Color16;
			uniform fixed4 _Color17;
			uniform fixed4 _Color18;
			uniform fixed4 _Color19;
			uniform fixed4 _Color20;
			uniform fixed4 _Color21;
			uniform fixed4 _Color22;
			uniform fixed4 _Color23;
			uniform fixed4 _Color24;
			uniform fixed4 _Color25;
			uniform fixed4 _Color26;
			uniform fixed4 _Color27;
			uniform fixed4 _Color28;
			uniform fixed4 _Color29;
			uniform fixed4 _Color30;
			uniform fixed4 _Color31;
			uniform fixed4 _Color32;
			uniform fixed4 _Color33;
			uniform fixed4 _Color34;
			uniform fixed4 _Color35;
			uniform fixed4 _Color36;
			uniform fixed4 _Color37;
			uniform fixed4 _Color38;
			uniform fixed4 _Color39;
			uniform fixed4 _Color40;
			uniform fixed4 _Color41;
			uniform fixed4 _Color42;
			uniform fixed4 _Color43;
			uniform fixed4 _Color44;
			uniform fixed4 _Color45;
			uniform fixed4 _Color46;
			uniform fixed4 _Color47;
			uniform fixed4 _Color48;
			uniform fixed4 _Color49;
			uniform fixed4 _Color50;
			uniform fixed4 _Color51;
			uniform fixed4 _Color52;
			uniform fixed4 _Color53;
			uniform fixed4 _Color54;
			uniform fixed4 _Color55;
			uniform fixed4 _Color56;
			uniform fixed4 _Color57;
			uniform fixed4 _Color58;
			uniform fixed4 _Color59;
			uniform fixed4 _Color60;
			uniform fixed4 _Color61;
			uniform fixed4 _Color62;
			uniform fixed4 _Color63;
			
			uniform sampler2D _MainTex;
		
			fixed4 frag (v2f_img i) : COLOR
			{
				fixed3 original = tex2D (_MainTex, i.uv).rgb;
		
				fixed dist00 = distance (original, _Color00.rgb);
				fixed dist01 = distance (original, _Color01.rgb);
				fixed dist02 = distance (original, _Color02.rgb);
				fixed dist03 = distance (original, _Color03.rgb);
				fixed dist04 = distance (original, _Color04.rgb);
				fixed dist05 = distance (original, _Color05.rgb);
				fixed dist06 = distance (original, _Color06.rgb);
				fixed dist07 = distance (original, _Color07.rgb);
				fixed dist08 = distance (original, _Color08.rgb);
				fixed dist09 = distance (original, _Color09.rgb);
				fixed dist10 = distance (original, _Color10.rgb);
				fixed dist11 = distance (original, _Color11.rgb);
				fixed dist12 = distance (original, _Color12.rgb);
				fixed dist13 = distance (original, _Color13.rgb);
				fixed dist14 = distance (original, _Color14.rgb);
				fixed dist15 = distance (original, _Color15.rgb);
				fixed dist16 = distance (original, _Color16.rgb);
				fixed dist17 = distance (original, _Color17.rgb);
				fixed dist18 = distance (original, _Color18.rgb);
				fixed dist19 = distance (original, _Color19.rgb);
				fixed dist20 = distance (original, _Color20.rgb);
				fixed dist21 = distance (original, _Color21.rgb);
				fixed dist22 = distance (original, _Color22.rgb);
				fixed dist23 = distance (original, _Color23.rgb);
				fixed dist24 = distance (original, _Color24.rgb);
				fixed dist25 = distance (original, _Color25.rgb);
				fixed dist26 = distance (original, _Color26.rgb);
				fixed dist27 = distance (original, _Color27.rgb);
				fixed dist28 = distance (original, _Color28.rgb);
				fixed dist29 = distance (original, _Color29.rgb);
				fixed dist30 = distance (original, _Color30.rgb);
				fixed dist31 = distance (original, _Color31.rgb);
				fixed dist32 = distance (original, _Color32.rgb);
				fixed dist33 = distance (original, _Color33.rgb);
				fixed dist34 = distance (original, _Color34.rgb);
				fixed dist35 = distance (original, _Color35.rgb);
				fixed dist36 = distance (original, _Color36.rgb);
				fixed dist37 = distance (original, _Color37.rgb);
				fixed dist38 = distance (original, _Color38.rgb);
				fixed dist39 = distance (original, _Color39.rgb);
				fixed dist40 = distance (original, _Color40.rgb);
				fixed dist41 = distance (original, _Color41.rgb);
				fixed dist42 = distance (original, _Color42.rgb);
				fixed dist43 = distance (original, _Color43.rgb);
				fixed dist44 = distance (original, _Color44.rgb);
				fixed dist45 = distance (original, _Color45.rgb);
				fixed dist46 = distance (original, _Color46.rgb);
				fixed dist47 = distance (original, _Color47.rgb);
				fixed dist48 = distance (original, _Color48.rgb);
				fixed dist49 = distance (original, _Color49.rgb);
				fixed dist50 = distance (original, _Color50.rgb);
				fixed dist51 = distance (original, _Color51.rgb);
				fixed dist52 = distance (original, _Color52.rgb);
				fixed dist53 = distance (original, _Color53.rgb);
				fixed dist54 = distance (original, _Color54.rgb);
				fixed dist55 = distance (original, _Color55.rgb);
				fixed dist56 = distance (original, _Color56.rgb);
				fixed dist57 = distance (original, _Color57.rgb);
				fixed dist58 = distance (original, _Color58.rgb);
				fixed dist59 = distance (original, _Color59.rgb);
				fixed dist60 = distance (original, _Color60.rgb);
				fixed dist61 = distance (original, _Color61.rgb);
				fixed dist62 = distance (original, _Color62.rgb);
				fixed dist63 = distance (original, _Color63.rgb);
				
				fixed4 col = fixed4 (0,0,0,0);
				fixed dist = 10.0;

				if (dist00 < dist){	dist = dist00;	col = _Color00;	}				
				if (dist01 < dist){	dist = dist01;	col = _Color01;	}
				if (dist02 < dist){	dist = dist02;	col = _Color02;	}				
				if (dist03 < dist){	dist = dist03;	col = _Color03;	}
				if (dist04 < dist){	dist = dist04;	col = _Color04;	}				
				if (dist05 < dist){	dist = dist05;	col = _Color05;	}
				if (dist06 < dist){	dist = dist06;	col = _Color06;	}				
				if (dist07 < dist){	dist = dist07;	col = _Color07;	}
				if (dist08 < dist){	dist = dist08;	col = _Color08;	}				
				if (dist09 < dist){	dist = dist09;	col = _Color09;	}
				if (dist10 < dist){	dist = dist10;	col = _Color10;	}				
				if (dist11 < dist){	dist = dist11;	col = _Color11;	}
				if (dist12 < dist){	dist = dist12;	col = _Color12;	}				
				if (dist13 < dist){	dist = dist13;	col = _Color13;	}
				if (dist14 < dist){	dist = dist14;	col = _Color14;	}				
				if (dist15 < dist){	dist = dist15;	col = _Color15;	}
				if (dist16 < dist){	dist = dist16;	col = _Color16;	}				
				if (dist17 < dist){	dist = dist17;	col = _Color17;	}
				if (dist18 < dist){	dist = dist18;	col = _Color18;	}				
				if (dist19 < dist){	dist = dist19;	col = _Color19;	}
				if (dist20 < dist){	dist = dist20;	col = _Color20;	}				
				if (dist21 < dist){	dist = dist21;	col = _Color21;	}
				if (dist22 < dist){	dist = dist22;	col = _Color22;	}				
				if (dist23 < dist){	dist = dist23;	col = _Color23;	}
				if (dist24 < dist){	dist = dist24;	col = _Color24;	}				
				if (dist25 < dist){	dist = dist25;	col = _Color25;	}
				if (dist26 < dist){	dist = dist26;	col = _Color26;	}				
				if (dist27 < dist){	dist = dist27;	col = _Color27;	}
				if (dist28 < dist){	dist = dist28;	col = _Color28;	}				
				if (dist29 < dist){	dist = dist29;	col = _Color29;	}
				if (dist30 < dist){	dist = dist30;	col = _Color30;	}				
				if (dist31 < dist){	dist = dist31;	col = _Color31;	}
				if (dist32 < dist){	dist = dist32;	col = _Color32;	}				
				if (dist33 < dist){	dist = dist33;	col = _Color33;	}
				if (dist34 < dist){	dist = dist34;	col = _Color34;	}				
				if (dist35 < dist){	dist = dist35;	col = _Color35;	}
				if (dist36 < dist){	dist = dist36;	col = _Color36;	}				
				if (dist37 < dist){	dist = dist37;	col = _Color37;	}
				if (dist38 < dist){	dist = dist38;	col = _Color38;	}				
				if (dist39 < dist){	dist = dist39;	col = _Color39;	}
				if (dist40 < dist){	dist = dist40;	col = _Color40;	}				
				if (dist41 < dist){	dist = dist41;	col = _Color41;	}
				if (dist42 < dist){	dist = dist42;	col = _Color42;	}				
				if (dist43 < dist){	dist = dist43;	col = _Color43;	}
				if (dist44 < dist){	dist = dist44;	col = _Color44;	}				
				if (dist45 < dist){	dist = dist45;	col = _Color45;	}
				if (dist46 < dist){	dist = dist46;	col = _Color46;	}				
				if (dist47 < dist){	dist = dist47;	col = _Color47;	}
				if (dist48 < dist){	dist = dist48;	col = _Color48;	}				
				if (dist49 < dist){	dist = dist49;	col = _Color49;	}
				if (dist50 < dist){	dist = dist50;	col = _Color50;	}				
				if (dist51 < dist){	dist = dist51;	col = _Color51;	}
				if (dist52 < dist){	dist = dist52;	col = _Color52;	}				
				if (dist53 < dist){	dist = dist53;	col = _Color53;	}
				if (dist54 < dist){	dist = dist54;	col = _Color54;	}				
				if (dist55 < dist){	dist = dist55;	col = _Color55;	}
				if (dist56 < dist){	dist = dist56;	col = _Color56;	}				
				if (dist57 < dist){	dist = dist57;	col = _Color57;	}
				if (dist58 < dist){	dist = dist58;	col = _Color58;	}				
				if (dist59 < dist){	dist = dist59;	col = _Color59;	}
				if (dist60 < dist){	dist = dist60;	col = _Color60;	}				
				if (dist61 < dist){	dist = dist61;	col = _Color61;	}
				if (dist62 < dist){	dist = dist62;	col = _Color62;	}				
				if (dist63 < dist){	dist = dist63;	col = _Color63;	}
				
				return col;
	  		}
	  		
	  		ENDCG
	 	}
	}
	
	FallBack "Diffuse"
}
