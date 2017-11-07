Shader "Mobile/Particles/Additive Cutoff" {
Properties {
 _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,1)
 _MainTex ("Particle Texture", 2D) = "white" {}
 _CutOff ("Cut off", Float) = 0.5
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend One One
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
Keywords { "_ALPHATEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD1 = _glesColor;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp float _CutOff;
uniform mediump vec4 _TintColor;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = ((color_2 * xlv_TEXCOORD1) * _TintColor);
  color_2 = tmpvar_4;
  tmpvar_1 = tmpvar_4;
  gl_FragData[0] = tmpvar_1;
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
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
  xlv_TEXCOORD1 = _glesColor;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp float _CutOff;
uniform mediump vec4 _TintColor;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = ((color_2 * xlv_TEXCOORD1) * _TintColor);
  color_2 = tmpvar_4;
  tmpvar_1 = tmpvar_4;
  _glesFragData[0] = tmpvar_1;
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
varying highp vec4 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = _glesMultiTexCoord1.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = _glesColor;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform highp float _CutOff;
uniform mediump vec4 _TintColor;
varying highp vec4 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  color_2.xyz = tmpvar_3.xyz;
  lowp float tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0.zw).x;
  color_2.w = tmpvar_4;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_5;
  tmpvar_5 = ((color_2 * xlv_TEXCOORD1) * _TintColor);
  color_2 = tmpvar_5;
  tmpvar_1 = tmpvar_5;
  gl_FragData[0] = tmpvar_1;
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
out highp vec4 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = _glesMultiTexCoord1.xy;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = _glesColor;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform highp float _CutOff;
uniform mediump vec4 _TintColor;
in highp vec4 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0.xy);
  color_2.xyz = tmpvar_3.xyz;
  lowp float tmpvar_4;
  tmpvar_4 = texture (_MainTex, xlv_TEXCOORD0.zw).x;
  color_2.w = tmpvar_4;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_5;
  tmpvar_5 = ((color_2 * xlv_TEXCOORD1) * _TintColor);
  color_2 = tmpvar_5;
  tmpvar_1 = tmpvar_5;
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