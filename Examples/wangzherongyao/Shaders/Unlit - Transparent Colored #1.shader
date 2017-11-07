Shader "UI/Gray Transparent Colored" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _StencilComp ("Stencil Comparison", Float) = 8
 _Stencil ("Stencil ID", Float) = 0
 _StencilOp ("Stencil Operation", Float) = 0
 _StencilWriteMask ("Stencil Write Mask", Float) = 255
 _StencilReadMask ("Stencil Read Mask", Float) = 255
 _ColorMask ("Color Mask", Float) = 15
 _Desaturate ("_Desaturate", Range(0,1)) = 1
 _Brightness ("_Brightness", Range(0.5,1.5)) = 1
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Stencil {
   Ref [_Stencil]
   ReadMask [_StencilReadMask]
   WriteMask [_StencilWriteMask]
   Comp [_StencilComp]
   Pass [_StencilOp]
  }
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask [_ColorMask]
  Offset -1, -1
Program "vp" {
SubProgram "gles " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying mediump vec3 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
void main ()
{
  mediump vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.z = min ((_glesColor.x * 255.0), 1.0);
  mediump vec3 tmpvar_4;
  tmpvar_4 = mix (vec3(1.0, 1.0, 1.0), _glesColor.xyz, tmpvar_1.zzz);
  tmpvar_2.xyz = tmpvar_4;
  mediump float tmpvar_5;
  tmpvar_5 = _glesColor.w;
  tmpvar_2.w = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_COLOR = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp float _Desaturate;
uniform lowp float _Brightness;
varying mediump vec3 xlv_TEXCOORD0;
varying lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec3 c2_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * xlv_COLOR);
  lowp vec3 tmpvar_4;
  tmpvar_4 = (mix (tmpvar_3.xyz, vec3(dot (tmpvar_2.xyz, vec3(0.299, 0.587, 0.114))), vec3(_Desaturate)) * _Brightness);
  mediump vec3 tmpvar_5;
  tmpvar_5 = mix (tmpvar_4, tmpvar_3.xyz, xlv_TEXCOORD0.zzz);
  c2_1 = tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = c2_1;
  tmpvar_6.w = tmpvar_3.w;
  gl_FragData[0] = tmpvar_6;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out mediump vec3 xlv_TEXCOORD0;
out lowp vec4 xlv_COLOR;
void main ()
{
  mediump vec3 tmpvar_1;
  lowp vec4 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_3;
  tmpvar_1.z = min ((_glesColor.x * 255.0), 1.0);
  mediump vec3 tmpvar_4;
  tmpvar_4 = mix (vec3(1.0, 1.0, 1.0), _glesColor.xyz, tmpvar_1.zzz);
  tmpvar_2.xyz = tmpvar_4;
  mediump float tmpvar_5;
  tmpvar_5 = _glesColor.w;
  tmpvar_2.w = tmpvar_5;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_COLOR = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp float _Desaturate;
uniform lowp float _Brightness;
in mediump vec3 xlv_TEXCOORD0;
in lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec3 c2_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0.xy);
  lowp vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * xlv_COLOR);
  lowp vec3 tmpvar_4;
  tmpvar_4 = (mix (tmpvar_3.xyz, vec3(dot (tmpvar_2.xyz, vec3(0.299, 0.587, 0.114))), vec3(_Desaturate)) * _Brightness);
  mediump vec3 tmpvar_5;
  tmpvar_5 = mix (tmpvar_4, tmpvar_3.xyz, xlv_TEXCOORD0.zzz);
  c2_1 = tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = c2_1;
  tmpvar_6.w = tmpvar_3.w;
  _glesFragData[0] = tmpvar_6;
}



#endif"
}
SubProgram "gles " {
Keywords { "_ALPHATEX_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying mediump vec3 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
varying lowp vec4 xlv_COLOR;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec2 tmpvar_2;
  lowp vec4 tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_4;
  tmpvar_1.z = min ((_glesColor.x * 255.0), 1.0);
  highp vec2 tmpvar_5;
  tmpvar_5 = ((_glesMultiTexCoord1.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = mix (vec3(1.0, 1.0, 1.0), _glesColor.xyz, tmpvar_1.zzz);
  tmpvar_3.xyz = tmpvar_6;
  mediump float tmpvar_7;
  tmpvar_7 = _glesColor.w;
  tmpvar_3.w = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp float _Desaturate;
uniform lowp float _Brightness;
varying mediump vec3 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
varying lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec3 c2_1;
  lowp vec4 c0_2;
  c0_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0.xy).xyz;
  c0_2.w = texture2D (_MainTex, xlv_TEXCOORD1).x;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (c0_2 * xlv_COLOR);
  lowp vec3 tmpvar_4;
  tmpvar_4 = (mix (tmpvar_3.xyz, vec3(dot (c0_2.xyz, vec3(0.299, 0.587, 0.114))), vec3(_Desaturate)) * _Brightness);
  mediump vec3 tmpvar_5;
  tmpvar_5 = mix (tmpvar_4, tmpvar_3.xyz, xlv_TEXCOORD0.zzz);
  c2_1 = tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = c2_1;
  tmpvar_6.w = tmpvar_3.w;
  gl_FragData[0] = tmpvar_6;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out mediump vec3 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD1;
out lowp vec4 xlv_COLOR;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec2 tmpvar_2;
  lowp vec4 tmpvar_3;
  highp vec2 tmpvar_4;
  tmpvar_4 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_1.xy = tmpvar_4;
  tmpvar_1.z = min ((_glesColor.x * 255.0), 1.0);
  highp vec2 tmpvar_5;
  tmpvar_5 = ((_glesMultiTexCoord1.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  tmpvar_2 = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = mix (vec3(1.0, 1.0, 1.0), _glesColor.xyz, tmpvar_1.zzz);
  tmpvar_3.xyz = tmpvar_6;
  mediump float tmpvar_7;
  tmpvar_7 = _glesColor.w;
  tmpvar_3.w = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_COLOR = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp float _Desaturate;
uniform lowp float _Brightness;
in mediump vec3 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD1;
in lowp vec4 xlv_COLOR;
void main ()
{
  lowp vec3 c2_1;
  lowp vec4 c0_2;
  c0_2.xyz = texture (_MainTex, xlv_TEXCOORD0.xy).xyz;
  c0_2.w = texture (_MainTex, xlv_TEXCOORD1).x;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (c0_2 * xlv_COLOR);
  lowp vec3 tmpvar_4;
  tmpvar_4 = (mix (tmpvar_3.xyz, vec3(dot (c0_2.xyz, vec3(0.299, 0.587, 0.114))), vec3(_Desaturate)) * _Brightness);
  mediump vec3 tmpvar_5;
  tmpvar_5 = mix (tmpvar_4, tmpvar_3.xyz, xlv_TEXCOORD0.zzz);
  c2_1 = tmpvar_5;
  lowp vec4 tmpvar_6;
  tmpvar_6.xyz = c2_1;
  tmpvar_6.w = tmpvar_3.w;
  _glesFragData[0] = tmpvar_6;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_ALPHATEX_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_ON" }
"!!GLES3"
}
}
 }
}
}