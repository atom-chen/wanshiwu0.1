Shader "S_Game_Effects/Light_VertexLit/Scroll2TexBend_LightMap" {
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
 LOD 200
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "LIGHTMODE"="VertexLM" "RenderType"="Opaque" }
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex1_ST;
uniform highp vec4 _MainTex2_ST;
uniform highp vec4 unity_LightmapST;
uniform highp float _ScrollX;
uniform highp float _ScrollY;
uniform highp float _Scroll2X;
uniform highp float _Scroll2Y;
uniform highp float _MMultiplier;
uniform highp vec4 _Color;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (_MMultiplier * _Color);
  tmpvar_2 = tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4.x = _ScrollX;
  tmpvar_4.y = _ScrollY;
  highp vec2 tmpvar_5;
  tmpvar_5 = fract((tmpvar_4 * _Time.x));
  tmpvar_1.xy = tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6.x = _Scroll2X;
  tmpvar_6.y = _Scroll2Y;
  highp vec2 tmpvar_7;
  tmpvar_7 = fract((tmpvar_6 * _Time.x));
  tmpvar_1.zw = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex1_ST.xy) + _MainTex1_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MainTex2_ST.xy) + _MainTex2_ST.zw);
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = tmpvar_1;
  xlv_TEXCOORD4 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex1;
uniform sampler2D _MainTex2;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _UVXX;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec2 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  mediump vec2 uv_2;
  lowp vec4 tmpvar_3;
  highp vec2 P_4;
  P_4 = (xlv_TEXCOORD0 + xlv_TEXCOORD3.xy);
  tmpvar_3 = texture2D (_MainTex1, P_4);
  highp vec2 tmpvar_5;
  tmpvar_5 = vec2((tmpvar_3.x * _UVXX.x));
  uv_2 = tmpvar_5;
  highp vec2 P_6;
  P_6 = ((xlv_TEXCOORD1 + xlv_TEXCOORD3.zw) + uv_2);
  lowp vec4 tmpvar_7;
  tmpvar_7 = (tmpvar_3 + texture2D (_MainTex2, P_6));
  mediump vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz * xlv_TEXCOORD4.xyz);
  c_1.xyz = tmpvar_8;
  c_1.w = 1.0;
  c_1.xyz = (c_1.xyz * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD2).xyz));
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex1_ST;
uniform highp vec4 _MainTex2_ST;
uniform highp vec4 unity_LightmapST;
uniform highp float _ScrollX;
uniform highp float _ScrollY;
uniform highp float _Scroll2X;
uniform highp float _Scroll2Y;
uniform highp float _MMultiplier;
uniform highp vec4 _Color;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
out highp vec2 xlv_TEXCOORD2;
out mediump vec4 xlv_TEXCOORD3;
out mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec4 tmpvar_2;
  highp vec4 tmpvar_3;
  tmpvar_3 = (_MMultiplier * _Color);
  tmpvar_2 = tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4.x = _ScrollX;
  tmpvar_4.y = _ScrollY;
  highp vec2 tmpvar_5;
  tmpvar_5 = fract((tmpvar_4 * _Time.x));
  tmpvar_1.xy = tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6.x = _Scroll2X;
  tmpvar_6.y = _Scroll2Y;
  highp vec2 tmpvar_7;
  tmpvar_7 = fract((tmpvar_6 * _Time.x));
  tmpvar_1.zw = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex1_ST.xy) + _MainTex1_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord0.xy * _MainTex2_ST.xy) + _MainTex2_ST.zw);
  xlv_TEXCOORD2 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD3 = tmpvar_1;
  xlv_TEXCOORD4 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex1;
uniform sampler2D _MainTex2;
uniform sampler2D unity_Lightmap;
uniform highp vec4 _UVXX;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
in highp vec2 xlv_TEXCOORD2;
in mediump vec4 xlv_TEXCOORD3;
in mediump vec4 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 c_1;
  mediump vec2 uv_2;
  lowp vec4 tmpvar_3;
  highp vec2 P_4;
  P_4 = (xlv_TEXCOORD0 + xlv_TEXCOORD3.xy);
  tmpvar_3 = texture (_MainTex1, P_4);
  highp vec2 tmpvar_5;
  tmpvar_5 = vec2((tmpvar_3.x * _UVXX.x));
  uv_2 = tmpvar_5;
  highp vec2 P_6;
  P_6 = ((xlv_TEXCOORD1 + xlv_TEXCOORD3.zw) + uv_2);
  lowp vec4 tmpvar_7;
  tmpvar_7 = (tmpvar_3 + texture (_MainTex2, P_6));
  mediump vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz * xlv_TEXCOORD4.xyz);
  c_1.xyz = tmpvar_8;
  c_1.w = 1.0;
  c_1.xyz = (c_1.xyz * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD2).xyz));
  _glesFragData[0] = c_1;
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