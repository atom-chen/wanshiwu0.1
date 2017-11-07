Shader "S_Game_Hero/Hero_Battle (Occlusion)" {
Properties {
 _MainTex ("Diffuse (RGB)", 2D) = "white" {}
 _LightTex ("LightTex (RGB)", 2D) = "white" {}
 _LightScale ("LightScale", Float) = 2
 _HurtColor ("HurtColor", Vector) = (0,0,0,0)
 _OcclusionColor ("Occlusion Color", Color) = (1,0,0,0.2)
 _OcclusionRim ("Occlusion Rim(x: power, y: scale)", Vector) = (3,2.5,0,0)
[HideInInspector]  _PlayerId ("Player ID", Float) = 0
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="AlphaTest+1" "RenderType"="Opaque" }
 Pass {
  Tags { "QUEUE"="AlphaTest+1" "RenderType"="Opaque" }
  ZTest Greater
  ZWrite Off
  Fog { Mode Off }
  Blend SrcAlpha One
  ColorMask RGB
Program "vp" {
SubProgram "gles " {
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
varying mediump vec3 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
varying mediump vec2 xlv_TEXCOORD2;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec2 tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (_WorldSpaceCameraPos - ((_Object2World * _glesVertex).xyz * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_6;
  tmpvar_6 = (_Object2World * tmpvar_5).xyz;
  tmpvar_2 = tmpvar_6;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
}



#endif
#ifdef FRAGMENT

uniform mediump vec4 _OcclusionColor;
uniform mediump vec4 _OcclusionRim;
varying mediump vec3 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  color_2.w = _OcclusionColor.w;
  color_2.xyz = (_OcclusionColor.xyz * (pow (
    (1.0 - clamp (dot (normalize(xlv_TEXCOORD0), normalize(xlv_TEXCOORD1)), 0.0, 1.0))
  , _OcclusionRim.x) * _OcclusionRim.y));
  tmpvar_1 = color_2;
  gl_FragData[0] = tmpvar_1;
}



#endif"
}
SubProgram "gles3 " {
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
uniform highp vec3 _WorldSpaceCameraPos;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 unity_Scale;
out mediump vec3 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
out mediump vec2 xlv_TEXCOORD2;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec2 tmpvar_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (_WorldSpaceCameraPos - ((_Object2World * _glesVertex).xyz * unity_Scale.w));
  tmpvar_1 = tmpvar_4;
  highp vec4 tmpvar_5;
  tmpvar_5.w = 0.0;
  tmpvar_5.xyz = normalize(_glesNormal);
  highp vec3 tmpvar_6;
  tmpvar_6 = (_Object2World * tmpvar_5).xyz;
  tmpvar_2 = tmpvar_6;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
  xlv_TEXCOORD2 = tmpvar_3;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform mediump vec4 _OcclusionColor;
uniform mediump vec4 _OcclusionRim;
in mediump vec3 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 color_2;
  color_2.w = _OcclusionColor.w;
  color_2.xyz = (_OcclusionColor.xyz * (pow (
    (1.0 - clamp (dot (normalize(xlv_TEXCOORD0), normalize(xlv_TEXCOORD1)), 0.0, 1.0))
  , _OcclusionRim.x) * _OcclusionRim.y));
  tmpvar_1 = color_2;
  _glesFragData[0] = tmpvar_1;
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
 Pass {
  Tags { "QUEUE"="AlphaTest+1" "RenderType"="Opaque" }
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec2 tmpvar_1;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _PlayerId;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 color_1;
  color_1.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  color_1.w = _PlayerId;
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec2 tmpvar_1;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _PlayerId;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 color_1;
  color_1.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  color_1.w = _PlayerId;
  _glesFragData[0] = color_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec2 tmpvar_1;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump vec3 _HurtColor;
uniform mediump float _PlayerId;
varying mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  mediump vec3 tmpvar_3;
  tmpvar_3 = (tmpvar_2.xyz + _HurtColor);
  color_1.xyz = tmpvar_3;
  color_1.w = _PlayerId;
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
out mediump vec2 xlv_TEXCOORD0;
void main ()
{
  mediump vec2 tmpvar_1;
  highp vec2 tmpvar_2;
  tmpvar_2 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_2;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump vec3 _HurtColor;
uniform mediump float _PlayerId;
in mediump vec2 xlv_TEXCOORD0;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0);
  mediump vec3 tmpvar_3;
  tmpvar_3 = (tmpvar_2.xyz + _HurtColor);
  color_1.xyz = tmpvar_3;
  color_1.w = _PlayerId;
  _glesFragData[0] = color_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_3;
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
uniform mediump float _LightScale;
uniform mediump float _PlayerId;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_LightTex, tmpvar_3);
  mediump vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_2.xyz * (tmpvar_4.xyz * _LightScale));
  color_1.xyz = tmpvar_5;
  color_1.w = _PlayerId;
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
out mediump vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_3;
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
uniform mediump float _LightScale;
uniform mediump float _PlayerId;
in mediump vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0);
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_LightTex, tmpvar_3);
  mediump vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_2.xyz * (tmpvar_4.xyz * _LightScale));
  color_1.xyz = tmpvar_5;
  color_1.w = _PlayerId;
  _glesFragData[0] = color_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_3;
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
uniform mediump float _LightScale;
uniform mediump vec3 _HurtColor;
uniform mediump float _PlayerId;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture2D (_MainTex, xlv_TEXCOORD0);
  color_1.w = tmpvar_2.w;
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_LightTex, tmpvar_3);
  mediump vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_2.xyz * (tmpvar_4.xyz * _LightScale));
  color_1.xyz = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = (color_1.xyz + _HurtColor);
  color_1.xyz = tmpvar_6;
  color_1.w = _PlayerId;
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec3 _glesNormal;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 glstate_matrix_modelview0;
out mediump vec2 xlv_TEXCOORD0;
out mediump vec3 xlv_TEXCOORD1;
void main ()
{
  mediump vec2 tmpvar_1;
  mediump vec3 tmpvar_2;
  highp vec2 tmpvar_3;
  tmpvar_3 = _glesMultiTexCoord0.xy;
  tmpvar_1 = tmpvar_3;
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
uniform mediump float _LightScale;
uniform mediump vec3 _HurtColor;
uniform mediump float _PlayerId;
in mediump vec2 xlv_TEXCOORD0;
in mediump vec3 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = texture (_MainTex, xlv_TEXCOORD0);
  color_1.w = tmpvar_2.w;
  mediump vec2 tmpvar_3;
  tmpvar_3 = ((normalize(xlv_TEXCOORD1).xy * 0.5) + 0.5);
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_LightTex, tmpvar_3);
  mediump vec3 tmpvar_5;
  tmpvar_5 = (tmpvar_2.xyz * (tmpvar_4.xyz * _LightScale));
  color_1.xyz = tmpvar_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = (color_1.xyz + _HurtColor);
  color_1.xyz = tmpvar_6;
  color_1.w = _PlayerId;
  _glesFragData[0] = color_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_OFF" "_HURT_EFFECT_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_LIGHT_TEX_ON" "_HURT_EFFECT_ON" }
"!!GLES3"
}
}
 }
}
}