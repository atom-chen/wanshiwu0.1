Shader "S_Game_Hero/Hero_Show" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _MaskTex ("Mask (R,G,B)", 2D) = "white" {}
 _RimPower ("Rim Power", Range(1,3)) = 1
 _ReflectionLV ("Reflection Multiplier", Float) = 2
 _LightTex ("轮廓光 (RGB)", 2D) = "white" {}
 _Reflection ("反射 (RGB)", 2D) = "white" {}
 _NormalTex ("Normal", 2D) = "bump" {}
 _NoiseTex ("Noise(RGB)", 2D) = "white" {}
 _Scroll2X ("Noise speed X", Float) = 1
 _Scroll2Y ("Noise speed Y", Float) = 0
 _Color ("Color", Color) = (1,1,1,1)
 _MMultiplier ("Layer Multiplier", Float) = 2
}
SubShader { 
 LOD 200
 Tags { "RenderType"="Opaque" }
 Pass {
  Tags { "RenderType"="Opaque" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.zw = tmpvar_1.xy;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_5;
  tmpvar_5 = (glstate_matrix_modelview0 * tmpvar_4).xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = (tmpvar_2.xyz * (texture2D (_LightTex, tmpvar_1) * 2.0).xyz);
  tmpvar_3.w = tmpvar_2.w;
  gl_FragData[0] = tmpvar_3;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.zw = tmpvar_1.xy;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_5;
  tmpvar_5 = (glstate_matrix_modelview0 * tmpvar_4).xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = (tmpvar_2.xyz * (texture (_LightTex, tmpvar_1) * 2.0).xyz);
  tmpvar_3.w = tmpvar_2.w;
  _glesFragData[0] = tmpvar_3;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  highp vec2 tmpvar_5;
  tmpvar_5 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_4 * _Time.x)));
  tmpvar_1.zw = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_7;
  tmpvar_7 = (glstate_matrix_modelview0 * tmpvar_6).xyz;
  tmpvar_2 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _NoiseTex;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec3 noise_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz * (tmpvar_3.xyz * _Color.xyz));
  noise_1 = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = (noise_1 * (tmpvar_4.y * _MMultiplier));
  noise_1 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8.xyz = ((tmpvar_3.xyz * (texture2D (_LightTex, tmpvar_2) * 2.0).xyz) + noise_1);
  tmpvar_8.w = tmpvar_3.w;
  gl_FragData[0] = tmpvar_8;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  highp vec2 tmpvar_5;
  tmpvar_5 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_4 * _Time.x)));
  tmpvar_1.zw = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_7;
  tmpvar_7 = (glstate_matrix_modelview0 * tmpvar_6).xyz;
  tmpvar_2 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _NoiseTex;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec3 noise_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz * (tmpvar_3.xyz * _Color.xyz));
  noise_1 = tmpvar_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = (noise_1 * (tmpvar_4.y * _MMultiplier));
  noise_1 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8.xyz = ((tmpvar_3.xyz * (texture (_LightTex, tmpvar_2) * 2.0).xyz) + noise_1);
  tmpvar_8.w = tmpvar_3.w;
  _glesFragData[0] = tmpvar_8;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  tmpvar_2.zw = tmpvar_2.xy;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_8;
  tmpvar_8 = (glstate_matrix_modelview0 * tmpvar_7).xyz;
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_4 = tmpvar_10;
  mediump vec3 tmpvar_11;
  tmpvar_11 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_12;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _NormalTex;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  mediump vec3 normal_1;
  mediump mat3 tmpvar_2;
  tmpvar_2[0].x = xlv_TEXCOORD2.x;
  tmpvar_2[0].y = xlv_TEXCOORD3.x;
  tmpvar_2[0].z = xlv_TEXCOORD1.x;
  tmpvar_2[1].x = xlv_TEXCOORD2.y;
  tmpvar_2[1].y = xlv_TEXCOORD3.y;
  tmpvar_2[1].z = xlv_TEXCOORD1.y;
  tmpvar_2[2].x = xlv_TEXCOORD2.z;
  tmpvar_2[2].y = xlv_TEXCOORD3.z;
  tmpvar_2[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_3;
  tmpvar_3 = ((texture2D (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_1 = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((normalize(
    (normal_1 * tmpvar_2)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = (tmpvar_5.xyz * (texture2D (_LightTex, tmpvar_4) * 2.0).xyz);
  tmpvar_6.w = tmpvar_5.w;
  gl_FragData[0] = tmpvar_6;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  tmpvar_2.zw = tmpvar_2.xy;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_8;
  tmpvar_8 = (glstate_matrix_modelview0 * tmpvar_7).xyz;
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_4 = tmpvar_10;
  mediump vec3 tmpvar_11;
  tmpvar_11 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_12;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _NormalTex;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in mediump vec3 xlv_TEXCOORD3;
void main ()
{
  mediump vec3 normal_1;
  mediump mat3 tmpvar_2;
  tmpvar_2[0].x = xlv_TEXCOORD2.x;
  tmpvar_2[0].y = xlv_TEXCOORD3.x;
  tmpvar_2[0].z = xlv_TEXCOORD1.x;
  tmpvar_2[1].x = xlv_TEXCOORD2.y;
  tmpvar_2[1].y = xlv_TEXCOORD3.y;
  tmpvar_2[1].z = xlv_TEXCOORD1.y;
  tmpvar_2[2].x = xlv_TEXCOORD2.z;
  tmpvar_2[2].y = xlv_TEXCOORD3.z;
  tmpvar_2[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_3;
  tmpvar_3 = ((texture (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_1 = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((normalize(
    (normal_1 * tmpvar_2)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = (tmpvar_5.xyz * (texture (_LightTex, tmpvar_4) * 2.0).xyz);
  tmpvar_6.w = tmpvar_5.w;
  _glesFragData[0] = tmpvar_6;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7.x = _Scroll2X;
  tmpvar_7.y = _Scroll2Y;
  highp vec2 tmpvar_8;
  tmpvar_8 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_7 * _Time.x)));
  tmpvar_2.zw = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_3 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 0.0;
  tmpvar_11.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (glstate_matrix_modelview0 * tmpvar_11).xyz;
  tmpvar_4 = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_14;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _NormalTex;
uniform sampler2D _NoiseTex;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 normal_2;
  mediump mat3 tmpvar_3;
  tmpvar_3[0].x = xlv_TEXCOORD2.x;
  tmpvar_3[0].y = xlv_TEXCOORD3.x;
  tmpvar_3[0].z = xlv_TEXCOORD1.x;
  tmpvar_3[1].x = xlv_TEXCOORD2.y;
  tmpvar_3[1].y = xlv_TEXCOORD3.y;
  tmpvar_3[1].z = xlv_TEXCOORD1.y;
  tmpvar_3[2].x = xlv_TEXCOORD2.z;
  tmpvar_3[2].y = xlv_TEXCOORD3.z;
  tmpvar_3[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_4;
  tmpvar_4 = ((texture2D (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_2 = tmpvar_4;
  mediump vec2 tmpvar_5;
  tmpvar_5 = ((normalize(
    (normal_2 * tmpvar_3)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8.xyz * (tmpvar_6.xyz * _Color.xyz));
  noise_1 = tmpvar_9;
  mediump vec3 tmpvar_10;
  tmpvar_10 = (noise_1 * (tmpvar_7.y * _MMultiplier));
  noise_1 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11.xyz = ((tmpvar_6.xyz * (texture2D (_LightTex, tmpvar_5) * 2.0).xyz) + noise_1);
  tmpvar_11.w = tmpvar_6.w;
  gl_FragData[0] = tmpvar_11;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7.x = _Scroll2X;
  tmpvar_7.y = _Scroll2Y;
  highp vec2 tmpvar_8;
  tmpvar_8 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_7 * _Time.x)));
  tmpvar_2.zw = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_3 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 0.0;
  tmpvar_11.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (glstate_matrix_modelview0 * tmpvar_11).xyz;
  tmpvar_4 = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_14;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _NormalTex;
uniform sampler2D _NoiseTex;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in mediump vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 normal_2;
  mediump mat3 tmpvar_3;
  tmpvar_3[0].x = xlv_TEXCOORD2.x;
  tmpvar_3[0].y = xlv_TEXCOORD3.x;
  tmpvar_3[0].z = xlv_TEXCOORD1.x;
  tmpvar_3[1].x = xlv_TEXCOORD2.y;
  tmpvar_3[1].y = xlv_TEXCOORD3.y;
  tmpvar_3[1].z = xlv_TEXCOORD1.y;
  tmpvar_3[2].x = xlv_TEXCOORD2.z;
  tmpvar_3[2].y = xlv_TEXCOORD3.z;
  tmpvar_3[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_4;
  tmpvar_4 = ((texture (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_2 = tmpvar_4;
  mediump vec2 tmpvar_5;
  tmpvar_5 = ((normalize(
    (normal_2 * tmpvar_3)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_6;
  tmpvar_6 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8.xyz * (tmpvar_6.xyz * _Color.xyz));
  noise_1 = tmpvar_9;
  mediump vec3 tmpvar_10;
  tmpvar_10 = (noise_1 * (tmpvar_7.y * _MMultiplier));
  noise_1 = tmpvar_10;
  lowp vec4 tmpvar_11;
  tmpvar_11.xyz = ((tmpvar_6.xyz * (texture (_LightTex, tmpvar_5) * 2.0).xyz) + noise_1);
  tmpvar_11.w = tmpvar_6.w;
  _glesFragData[0] = tmpvar_11;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.zw = tmpvar_1.xy;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_5;
  tmpvar_5 = (glstate_matrix_modelview0 * tmpvar_4).xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec3 Reflection_1;
  lowp vec3 color_2;
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4.xyz * (texture2D (_LightTex, tmpvar_3) * 2.0).xyz);
  lowp vec3 tmpvar_6;
  tmpvar_6 = texture2D (_Reflection, tmpvar_3).xyz;
  Reflection_1 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = texture2D (_MaskTex, xlv_TEXCOORD0.xy).xxx;
  mediump vec3 tmpvar_8;
  tmpvar_8 = mix (tmpvar_5, ((tmpvar_5 * 
    pow (Reflection_1, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_7);
  color_2 = tmpvar_8;
  lowp vec4 tmpvar_9;
  tmpvar_9.xyz = color_2;
  tmpvar_9.w = tmpvar_4.w;
  gl_FragData[0] = tmpvar_9;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.zw = tmpvar_1.xy;
  highp vec4 tmpvar_4;
  tmpvar_4.w = 0.0;
  tmpvar_4.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_5;
  tmpvar_5 = (glstate_matrix_modelview0 * tmpvar_4).xyz;
  tmpvar_2 = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec3 Reflection_1;
  lowp vec3 color_2;
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_4.xyz * (texture (_LightTex, tmpvar_3) * 2.0).xyz);
  lowp vec3 tmpvar_6;
  tmpvar_6 = texture (_Reflection, tmpvar_3).xyz;
  Reflection_1 = tmpvar_6;
  lowp vec3 tmpvar_7;
  tmpvar_7 = texture (_MaskTex, xlv_TEXCOORD0.xy).xxx;
  mediump vec3 tmpvar_8;
  tmpvar_8 = mix (tmpvar_5, ((tmpvar_5 * 
    pow (Reflection_1, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_7);
  color_2 = tmpvar_8;
  lowp vec4 tmpvar_9;
  tmpvar_9.xyz = color_2;
  tmpvar_9.w = tmpvar_4.w;
  _glesFragData[0] = tmpvar_9;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  highp vec2 tmpvar_5;
  tmpvar_5 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_4 * _Time.x)));
  tmpvar_1.zw = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_7;
  tmpvar_7 = (glstate_matrix_modelview0 * tmpvar_6).xyz;
  tmpvar_2 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NoiseTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 Reflection_2;
  lowp vec3 color_3;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz * (texture2D (_LightTex, tmpvar_4) * 2.0).xyz);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_8;
  tmpvar_8 = texture2D (_Reflection, tmpvar_4).xyz;
  Reflection_2 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = mix (tmpvar_6, ((tmpvar_6 * 
    pow (Reflection_2, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_7.xxx);
  color_3 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10.xyz * (tmpvar_5.xyz * _Color.xyz));
  noise_1 = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = (noise_1 * (tmpvar_7.y * _MMultiplier));
  noise_1 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = (color_3 + noise_1);
  color_3 = tmpvar_13;
  lowp vec4 tmpvar_14;
  tmpvar_14.xyz = tmpvar_13;
  tmpvar_14.w = tmpvar_5.w;
  gl_FragData[0] = tmpvar_14;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  mediump vec2 tmpvar_4;
  tmpvar_4.x = _Scroll2X;
  tmpvar_4.y = _Scroll2Y;
  highp vec2 tmpvar_5;
  tmpvar_5 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_4 * _Time.x)));
  tmpvar_1.zw = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6.w = 0.0;
  tmpvar_6.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_7;
  tmpvar_7 = (glstate_matrix_modelview0 * tmpvar_6).xyz;
  tmpvar_2 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NoiseTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 Reflection_2;
  lowp vec3 color_3;
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_6;
  tmpvar_6 = (tmpvar_5.xyz * (texture (_LightTex, tmpvar_4) * 2.0).xyz);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_8;
  tmpvar_8 = texture (_Reflection, tmpvar_4).xyz;
  Reflection_2 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = mix (tmpvar_6, ((tmpvar_6 * 
    pow (Reflection_2, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_7.xxx);
  color_3 = tmpvar_9;
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_11;
  tmpvar_11 = (tmpvar_10.xyz * (tmpvar_5.xyz * _Color.xyz));
  noise_1 = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = (noise_1 * (tmpvar_7.y * _MMultiplier));
  noise_1 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = (color_3 + noise_1);
  color_3 = tmpvar_13;
  lowp vec4 tmpvar_14;
  tmpvar_14.xyz = tmpvar_13;
  tmpvar_14.w = tmpvar_5.w;
  _glesFragData[0] = tmpvar_14;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  tmpvar_2.zw = tmpvar_2.xy;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_8;
  tmpvar_8 = (glstate_matrix_modelview0 * tmpvar_7).xyz;
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_4 = tmpvar_10;
  mediump vec3 tmpvar_11;
  tmpvar_11 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_12;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NormalTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  mediump vec3 Reflection_1;
  lowp vec3 color_2;
  mediump vec3 normal_3;
  mediump mat3 tmpvar_4;
  tmpvar_4[0].x = xlv_TEXCOORD2.x;
  tmpvar_4[0].y = xlv_TEXCOORD3.x;
  tmpvar_4[0].z = xlv_TEXCOORD1.x;
  tmpvar_4[1].x = xlv_TEXCOORD2.y;
  tmpvar_4[1].y = xlv_TEXCOORD3.y;
  tmpvar_4[1].z = xlv_TEXCOORD1.y;
  tmpvar_4[2].x = xlv_TEXCOORD2.z;
  tmpvar_4[2].y = xlv_TEXCOORD3.z;
  tmpvar_4[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_5;
  tmpvar_5 = ((texture2D (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_3 = tmpvar_5;
  mediump vec2 tmpvar_6;
  tmpvar_6 = ((normalize(
    (normal_3 * tmpvar_4)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz * (texture2D (_LightTex, tmpvar_6) * 2.0).xyz);
  lowp vec3 tmpvar_9;
  tmpvar_9 = texture2D (_Reflection, tmpvar_6).xyz;
  Reflection_1 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = texture2D (_MaskTex, xlv_TEXCOORD0.xy).xxx;
  mediump vec3 tmpvar_11;
  tmpvar_11 = mix (tmpvar_8, ((tmpvar_8 * 
    pow (Reflection_1, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_10);
  color_2 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12.xyz = color_2;
  tmpvar_12.w = tmpvar_7.w;
  gl_FragData[0] = tmpvar_12;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform highp vec4 _MainTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  tmpvar_2.zw = tmpvar_2.xy;
  highp vec4 tmpvar_7;
  tmpvar_7.w = 0.0;
  tmpvar_7.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_8;
  tmpvar_8 = (glstate_matrix_modelview0 * tmpvar_7).xyz;
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_4 = tmpvar_10;
  mediump vec3 tmpvar_11;
  tmpvar_11 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_12;
  tmpvar_12 = (tmpvar_11 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_12;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NormalTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in mediump vec3 xlv_TEXCOORD3;
void main ()
{
  mediump vec3 Reflection_1;
  lowp vec3 color_2;
  mediump vec3 normal_3;
  mediump mat3 tmpvar_4;
  tmpvar_4[0].x = xlv_TEXCOORD2.x;
  tmpvar_4[0].y = xlv_TEXCOORD3.x;
  tmpvar_4[0].z = xlv_TEXCOORD1.x;
  tmpvar_4[1].x = xlv_TEXCOORD2.y;
  tmpvar_4[1].y = xlv_TEXCOORD3.y;
  tmpvar_4[1].z = xlv_TEXCOORD1.y;
  tmpvar_4[2].x = xlv_TEXCOORD2.z;
  tmpvar_4[2].y = xlv_TEXCOORD3.z;
  tmpvar_4[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_5;
  tmpvar_5 = ((texture (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_3 = tmpvar_5;
  mediump vec2 tmpvar_6;
  tmpvar_6 = ((normalize(
    (normal_3 * tmpvar_4)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_8;
  tmpvar_8 = (tmpvar_7.xyz * (texture (_LightTex, tmpvar_6) * 2.0).xyz);
  lowp vec3 tmpvar_9;
  tmpvar_9 = texture (_Reflection, tmpvar_6).xyz;
  Reflection_1 = tmpvar_9;
  lowp vec3 tmpvar_10;
  tmpvar_10 = texture (_MaskTex, xlv_TEXCOORD0.xy).xxx;
  mediump vec3 tmpvar_11;
  tmpvar_11 = mix (tmpvar_8, ((tmpvar_8 * 
    pow (Reflection_1, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_10);
  color_2 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12.xyz = color_2;
  tmpvar_12.w = tmpvar_7.w;
  _glesFragData[0] = tmpvar_12;
}



#endif"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesTANGENT;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7.x = _Scroll2X;
  tmpvar_7.y = _Scroll2Y;
  highp vec2 tmpvar_8;
  tmpvar_8 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_7 * _Time.x)));
  tmpvar_2.zw = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_3 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 0.0;
  tmpvar_11.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (glstate_matrix_modelview0 * tmpvar_11).xyz;
  tmpvar_4 = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_14;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NormalTex;
uniform sampler2D _NoiseTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec3 xlv_TEXCOORD2;
varying mediump vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 Reflection_2;
  lowp vec3 color_3;
  mediump vec3 normal_4;
  mediump mat3 tmpvar_5;
  tmpvar_5[0].x = xlv_TEXCOORD2.x;
  tmpvar_5[0].y = xlv_TEXCOORD3.x;
  tmpvar_5[0].z = xlv_TEXCOORD1.x;
  tmpvar_5[1].x = xlv_TEXCOORD2.y;
  tmpvar_5[1].y = xlv_TEXCOORD3.y;
  tmpvar_5[1].z = xlv_TEXCOORD1.y;
  tmpvar_5[2].x = xlv_TEXCOORD2.z;
  tmpvar_5[2].y = xlv_TEXCOORD3.z;
  tmpvar_5[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_6;
  tmpvar_6 = ((texture2D (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_4 = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7 = ((normalize(
    (normal_4 * tmpvar_5)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8.xyz * (texture2D (_LightTex, tmpvar_7) * 2.0).xyz);
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture2D (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_11;
  tmpvar_11 = texture2D (_Reflection, tmpvar_7).xyz;
  Reflection_2 = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = mix (tmpvar_9, ((tmpvar_9 * 
    pow (Reflection_2, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_10.xxx);
  color_3 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture2D (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13.xyz * (tmpvar_8.xyz * _Color.xyz));
  noise_1 = tmpvar_14;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (noise_1 * (tmpvar_10.y * _MMultiplier));
  noise_1 = tmpvar_15;
  lowp vec3 tmpvar_16;
  tmpvar_16 = (color_3 + noise_1);
  color_3 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17.xyz = tmpvar_16;
  tmpvar_17.w = tmpvar_8.w;
  gl_FragData[0] = tmpvar_17;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
in vec4 _glesTANGENT;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
uniform mediump float _Scroll2X;
uniform mediump float _Scroll2Y;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 _NoiseTex_ST;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out mediump vec3 xlv_TEXCOORD2;
out mediump vec3 xlv_TEXCOORD3;
void main ()
{
  vec4 tmpvar_1;
  tmpvar_1.xyz = normalize(_glesTANGENT.xyz);
  tmpvar_1.w = _glesTANGENT.w;
  mediump vec4 tmpvar_2;
  mediump vec3 tmpvar_3;
  mediump vec3 tmpvar_4;
  mediump vec3 tmpvar_5;
  highp vec2 tmpvar_6;
  tmpvar_6 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2.xy = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7.x = _Scroll2X;
  tmpvar_7.y = _Scroll2Y;
  highp vec2 tmpvar_8;
  tmpvar_8 = (((_glesMultiTexCoord0.xy * _NoiseTex_ST.xy) + _NoiseTex_ST.zw) + fract((tmpvar_7 * _Time.x)));
  tmpvar_2.zw = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9.w = 0.0;
  tmpvar_9.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_10;
  tmpvar_10 = (glstate_matrix_modelview0 * tmpvar_9).xyz;
  tmpvar_3 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11.w = 0.0;
  tmpvar_11.xyz = tmpvar_1.xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = (glstate_matrix_modelview0 * tmpvar_11).xyz;
  tmpvar_4 = tmpvar_12;
  mediump vec3 tmpvar_13;
  tmpvar_13 = ((tmpvar_3.yzx * tmpvar_4.zxy) - (tmpvar_3.zxy * tmpvar_4.yzx));
  highp vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13 * _glesTANGENT.w);
  tmpvar_5 = tmpvar_14;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _LightTex;
uniform sampler2D _MaskTex;
uniform sampler2D _Reflection;
uniform sampler2D _NormalTex;
uniform sampler2D _NoiseTex;
uniform mediump float _RimPower;
uniform mediump float _ReflectionLV;
uniform mediump float _MMultiplier;
uniform mediump vec4 _Color;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
in mediump vec3 xlv_TEXCOORD2;
in mediump vec3 xlv_TEXCOORD3;
void main ()
{
  lowp vec3 noise_1;
  mediump vec3 Reflection_2;
  lowp vec3 color_3;
  mediump vec3 normal_4;
  mediump mat3 tmpvar_5;
  tmpvar_5[0].x = xlv_TEXCOORD2.x;
  tmpvar_5[0].y = xlv_TEXCOORD3.x;
  tmpvar_5[0].z = xlv_TEXCOORD1.x;
  tmpvar_5[1].x = xlv_TEXCOORD2.y;
  tmpvar_5[1].y = xlv_TEXCOORD3.y;
  tmpvar_5[1].z = xlv_TEXCOORD1.y;
  tmpvar_5[2].x = xlv_TEXCOORD2.z;
  tmpvar_5[2].y = xlv_TEXCOORD3.z;
  tmpvar_5[2].z = xlv_TEXCOORD1.z;
  lowp vec3 tmpvar_6;
  tmpvar_6 = ((texture (_NormalTex, xlv_TEXCOORD0.xy).xyz * 2.0) - 1.0);
  normal_4 = tmpvar_6;
  mediump vec2 tmpvar_7;
  tmpvar_7 = ((normalize(
    (normal_4 * tmpvar_5)
  ).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_9;
  tmpvar_9 = (tmpvar_8.xyz * (texture (_LightTex, tmpvar_7) * 2.0).xyz);
  lowp vec4 tmpvar_10;
  tmpvar_10 = texture (_MaskTex, xlv_TEXCOORD0.xy);
  lowp vec3 tmpvar_11;
  tmpvar_11 = texture (_Reflection, tmpvar_7).xyz;
  Reflection_2 = tmpvar_11;
  mediump vec3 tmpvar_12;
  tmpvar_12 = mix (tmpvar_9, ((tmpvar_9 * 
    pow (Reflection_2, vec3(_RimPower))
  ) * _ReflectionLV), tmpvar_10.xxx);
  color_3 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13 = texture (_NoiseTex, xlv_TEXCOORD0.zw);
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_13.xyz * (tmpvar_8.xyz * _Color.xyz));
  noise_1 = tmpvar_14;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (noise_1 * (tmpvar_10.y * _MMultiplier));
  noise_1 = tmpvar_15;
  lowp vec3 tmpvar_16;
  tmpvar_16 = (color_3 + noise_1);
  color_3 = tmpvar_16;
  lowp vec4 tmpvar_17;
  tmpvar_17.xyz = tmpvar_16;
  tmpvar_17.w = tmpvar_8.w;
  _glesFragData[0] = tmpvar_17;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NORMALMAP_OFF" "_NOISETEX_OFF" "_REFLECTION_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_OFF" "_REFLECTION_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_OFF" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_NOISETEX_ON" "_NORMALMAP_ON" "_REFLECTION_ON" }
"!!GLES3"
}
}
 }
}
}