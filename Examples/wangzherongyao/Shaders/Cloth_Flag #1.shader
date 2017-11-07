Shader "S_Game_Scene/Cloth_Flag" {
Properties {
 _MainTex ("Texture", 2D) = "white" {}
 _Frequency ("Frequency(10-100)", Range(10,100)) = 10
 _AlphaTex ("千万不要填，系统用的", 2D) = "white" {}
}
SubShader { 
 Pass {
  Cull Off
Program "vp" {
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp float _Frequency;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = _glesVertex.w;
  highp float tmpvar_2;
  tmpvar_2 = ((_glesVertex.x + _glesVertex.y) + _glesVertex.z);
  highp float tmpvar_3;
  tmpvar_3 = (-(_Time) * _Frequency).x;
  tmpvar_1.x = (_glesVertex.x + ((
    sin(((tmpvar_3 * 1.45) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5));
  tmpvar_1.y = (((
    sin(((tmpvar_3 * 3.12) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5) - ((_glesMultiTexCoord0.x * _glesMultiTexCoord0.y) * 0.9));
  tmpvar_1.z = (_glesVertex.z - ((
    sin(((tmpvar_3 * 2.2) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.2));
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp float _Frequency;
out highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = _glesVertex.w;
  highp float tmpvar_2;
  tmpvar_2 = ((_glesVertex.x + _glesVertex.y) + _glesVertex.z);
  highp float tmpvar_3;
  tmpvar_3 = (-(_Time) * _Frequency).x;
  tmpvar_1.x = (_glesVertex.x + ((
    sin(((tmpvar_3 * 1.45) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5));
  tmpvar_1.y = (((
    sin(((tmpvar_3 * 3.12) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5) - ((_glesMultiTexCoord0.x * _glesMultiTexCoord0.y) * 0.9));
  tmpvar_1.z = (_glesVertex.z - ((
    sin(((tmpvar_3 * 2.2) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.2));
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
in highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  mediump vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0);
  color_2 = tmpvar_3;
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp float _Frequency;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = _glesVertex.w;
  highp float tmpvar_2;
  tmpvar_2 = ((_glesVertex.x + _glesVertex.y) + _glesVertex.z);
  highp float tmpvar_3;
  tmpvar_3 = (-(_Time) * _Frequency).x;
  tmpvar_1.x = (_glesVertex.x + ((
    sin(((tmpvar_3 * 1.45) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5));
  tmpvar_1.y = (((
    sin(((tmpvar_3 * 3.12) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5) - ((_glesMultiTexCoord0.x * _glesMultiTexCoord0.y) * 0.9));
  tmpvar_1.z = (_glesVertex.z - ((
    sin(((tmpvar_3 * 2.2) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.2));
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  tmpvar_1 = tmpvar_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _Time;
uniform highp mat4 glstate_matrix_mvp;
uniform highp float _Frequency;
out highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  tmpvar_1.w = _glesVertex.w;
  highp float tmpvar_2;
  tmpvar_2 = ((_glesVertex.x + _glesVertex.y) + _glesVertex.z);
  highp float tmpvar_3;
  tmpvar_3 = (-(_Time) * _Frequency).x;
  tmpvar_1.x = (_glesVertex.x + ((
    sin(((tmpvar_3 * 1.45) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5));
  tmpvar_1.y = (((
    sin(((tmpvar_3 * 3.12) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.5) - ((_glesMultiTexCoord0.x * _glesMultiTexCoord0.y) * 0.9));
  tmpvar_1.z = (_glesVertex.z - ((
    sin(((tmpvar_3 * 2.2) + tmpvar_2))
   * _glesMultiTexCoord0.x) * 0.2));
  gl_Position = (glstate_matrix_mvp * tmpvar_1);
  xlv_TEXCOORD0 = _glesMultiTexCoord0.xy;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
void main ()
{
  highp vec4 tmpvar_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_2.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  tmpvar_1 = tmpvar_2;
  _glesFragData[0] = tmpvar_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" }
"!!GLES3"
}
}
 }
}
Fallback "VertexLit"
}