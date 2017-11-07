Shader "UI/UI_Color" {
Properties {
[PerRendererData]  _MainTex ("Font Texture", 2D) = "white" {}
 _Color ("Text Color", Color) = (1,1,1,1)
 _MMultiplier ("Layer Multiplier", Float) = 1
[HideInInspector]  _StencilComp ("Stencil Comparison", Float) = 8
[HideInInspector]  _Stencil ("Stencil ID", Float) = 0
[HideInInspector]  _StencilOp ("Stencil Operation", Float) = 0
[HideInInspector]  _StencilWriteMask ("Stencil Write Mask", Float) = 255
[HideInInspector]  _StencilReadMask ("Stencil Read Mask", Float) = 255
[HideInInspector]  _ColorMask ("Color Mask", Float) = 15
}
SubShader { 
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" }
  ZTest [unity_GUIZTestMode]
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
Program "vp" {
SubProgram "gles " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp float _MMultiplier;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = ((texture2D (_MainTex, xlv_TEXCOORD0) * _MMultiplier) * _Color);
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  mediump vec2 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp float _MMultiplier;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = ((texture (_MainTex, xlv_TEXCOORD0) * _MMultiplier) * _Color);
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_ALPHATEX_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec4 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord1.xy;
  mediump vec4 tmpvar_3;
  tmpvar_3.xy = tmpvar_1;
  tmpvar_3.zw = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp float _MMultiplier;
varying mediump vec4 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp vec4 col_2;
  col_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0.xy).xyz;
  col_2.w = texture2D (_MainTex, xlv_TEXCOORD0.zw).x;
  tmpvar_1 = ((col_2 * _MMultiplier) * _Color);
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_ALPHATEX_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec4 xlv_TEXCOORD0;
void main ()
{
  highp vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord1.xy;
  mediump vec4 tmpvar_3;
  tmpvar_3.xy = tmpvar_1;
  tmpvar_3.zw = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform highp float _MMultiplier;
in mediump vec4 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 tmpvar_1;
  lowp vec4 col_2;
  col_2.xyz = texture (_MainTex, xlv_TEXCOORD0.xy).xyz;
  col_2.w = texture (_MainTex, xlv_TEXCOORD0.zw).x;
  tmpvar_1 = ((col_2 * _MMultiplier) * _Color);
  _glesFragData[0] = tmpvar_1;
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