Shader "S_Game_Effects/Scroll2TexBend_Opaque" {
Properties {
 _MainTex1 ("Tex1(RGB)", 2D) = "white" {}
 _MainTex2 ("Tex2(RGB)", 2D) = "white" {}
 _ScrollX ("Tex1 speed X", Float) = 1
 _ScrollY ("Tex1 speed Y", Float) = 0
 _Scroll2X ("Tex2 speed X", Float) = 1
 _Scroll2Y ("Tex2 speed Y", Float) = 0
 _Color ("Color", Color) = (1,1,1,1)
 _UVXX ("UVXX", Vector) = (0.3,1,1,1)
 _MMultiplier ("Layer Multiplier", Float) = 2
}
SubShader { 
 LOD 500
 Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
 Pass {
  Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex1_ST;
uniform highp vec4 _MainTex2_ST;
uniform highp float _ScrollX;
uniform highp float _ScrollY;
uniform highp float _Scroll2X;
uniform highp float _Scroll2Y;
uniform highp float _MMultiplier;
uniform highp vec4 _Color;
varying highp vec4 xlv_TEXCOORD0;
varying lowp vec4 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3.x = _ScrollX;
  tmpvar_3.y = _ScrollY;
  tmpvar_1.xy = (((_glesMultiTexCoord0.xy * _MainTex1_ST.xy) + _MainTex1_ST.zw) + fract((tmpvar_3 * _Time.x)));
  highp vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  tmpvar_1.zw = (((_glesMultiTexCoord0.xy * _MainTex2_ST.xy) + _MainTex2_ST.zw) + fract((tmpvar_4 * _Time.x)));
  highp vec4 tmpvar_5;
  tmpvar_5 = ((_MMultiplier * _Color) * _glesColor);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex1;
uniform sampler2D _MainTex2;
uniform highp vec4 _UVXX;
varying highp vec4 xlv_TEXCOORD0;
varying lowp vec4 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 uv_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex1, xlv_TEXCOORD0.xy);
  highp vec2 tmpvar_3;
  tmpvar_3 = vec2((tmpvar_2.x * _UVXX.x));
  uv_1 = tmpvar_3;
  highp vec2 P_4;
  P_4 = (xlv_TEXCOORD0.zw + uv_1);
  lowp vec4 tmpvar_5;
  tmpvar_5 = ((tmpvar_2 * texture2D (_MainTex2, P_4)) * xlv_TEXCOORD1);
  gl_FragData[0] = tmpvar_5;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex1_ST;
uniform highp vec4 _MainTex2_ST;
uniform highp float _ScrollX;
uniform highp float _ScrollY;
uniform highp float _Scroll2X;
uniform highp float _Scroll2Y;
uniform highp float _MMultiplier;
uniform highp vec4 _Color;
out highp vec4 xlv_TEXCOORD0;
out lowp vec4 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3.x = _ScrollX;
  tmpvar_3.y = _ScrollY;
  tmpvar_1.xy = (((_glesMultiTexCoord0.xy * _MainTex1_ST.xy) + _MainTex1_ST.zw) + fract((tmpvar_3 * _Time.x)));
  highp vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  tmpvar_1.zw = (((_glesMultiTexCoord0.xy * _MainTex2_ST.xy) + _MainTex2_ST.zw) + fract((tmpvar_4 * _Time.x)));
  highp vec4 tmpvar_5;
  tmpvar_5 = ((_MMultiplier * _Color) * _glesColor);
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex1;
uniform sampler2D _MainTex2;
uniform highp vec4 _UVXX;
in highp vec4 xlv_TEXCOORD0;
in lowp vec4 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 uv_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex1, xlv_TEXCOORD0.xy);
  highp vec2 tmpvar_3;
  tmpvar_3 = vec2((tmpvar_2.x * _UVXX.x));
  uv_1 = tmpvar_3;
  highp vec2 P_4;
  P_4 = (xlv_TEXCOORD0.zw + uv_1);
  lowp vec4 tmpvar_5;
  tmpvar_5 = ((tmpvar_2 * texture (_MainTex2, P_4)) * xlv_TEXCOORD1);
  _glesFragData[0] = tmpvar_5;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
"!!GLES"
}
SubProgram "gles3 " {
"!!GLES3"
}
}
 }
}
}