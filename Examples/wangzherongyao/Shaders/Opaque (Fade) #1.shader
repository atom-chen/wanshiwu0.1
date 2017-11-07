Shader "S_Game_Particle/Opaque (Fade)" {
Properties {
 _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,1)
 _MainTex ("Particle Texture", 2D) = "white" {}
 _AlphaTex ("千万不要填，系统用的", 2D) = "white" {}
 _CutOff ("Cut off", Float) = 0.5
 _FadeFactor ("Fade Factor", Float) = 1
}
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
  ZWrite Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
Keywords { "_TINTCOLOR_OFF" "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_TINTCOLOR_OFF" "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
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
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
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
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" "_TINTCOLOR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" "_TINTCOLOR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
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
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
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
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_ON" "_TINTCOLOR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesColor;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_ON" "_TINTCOLOR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesColor;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump vec4 _TintColor;
uniform highp vec4 _MainTex_ST;
out highp vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  tmpvar_1 = _glesColor;
  mediump vec4 tmpvar_2;
  tmpvar_2 = tmpvar_1;
  mediump vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * (_TintColor * 2.0));
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _CutOff;
uniform mediump float _FadeFactor;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  color_2 = tmpvar_3;
  if ((color_2.w < _CutOff)) {
    discard;
  };
  mediump vec4 tmpvar_4;
  tmpvar_4 = (color_2 * xlv_TEXCOORD1);
  color_2.xyz = tmpvar_4.xyz;
  color_2.w = (tmpvar_4.w * _FadeFactor);
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_TINTCOLOR_OFF" "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_TINTCOLOR_OFF" "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_TINTCOLOR_OFF" "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_OFF" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" "_TINTCOLOR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_CUTOFF_OFF" "_TINTCOLOR_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_OFF" "_TINTCOLOR_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_ON" "_TINTCOLOR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_CUTOFF_ON" "_SEPERATE_ALPHA_TEX_ON" "_TINTCOLOR_ON" }
"!!GLES3"
}
}
 }
}
}