Shader "SGame_Post/RadialBlur" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
}
SubShader { 
 Pass {
  ZTest Always
  ZWrite Off
  Cull Off
  Fog { Mode Off }
Program "vp" {
SubProgram "gles " {
Keywords { "_HIGHQUALITY_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _ScreenParams;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump float _BlurScale;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * vec2(2.0, 2.0)) + vec2(-1.0, -1.0));
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_BlurScale * 0.2) / _ScreenParams.xy);
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump vec2 _ScreenCenter;
uniform mediump float _FalloffExp;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = (xlv_TEXCOORD0.zw - _ScreenCenter);
  mediump float tmpvar_3;
  tmpvar_3 = sqrt(dot (tmpvar_2, tmpvar_2));
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((tmpvar_2 / tmpvar_3) * (pow (tmpvar_3, _FalloffExp) * xlv_TEXCOORD1));
  c_1.w = 0.0;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  c_1.xyz = tmpvar_5.xyz;
  lowp vec4 tmpvar_6;
  mediump vec2 P_7;
  P_7 = (xlv_TEXCOORD0.xy + tmpvar_4);
  tmpvar_6 = texture2D (_MainTex, P_7);
  c_1.xyz = (c_1.xyz + tmpvar_6.xyz);
  lowp vec4 tmpvar_8;
  mediump vec2 P_9;
  P_9 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 2.0));
  tmpvar_8 = texture2D (_MainTex, P_9);
  c_1.xyz = (c_1.xyz + tmpvar_8.xyz);
  lowp vec4 tmpvar_10;
  mediump vec2 P_11;
  P_11 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 3.0));
  tmpvar_10 = texture2D (_MainTex, P_11);
  c_1.xyz = (c_1.xyz + tmpvar_10.xyz);
  lowp vec4 tmpvar_12;
  mediump vec2 P_13;
  P_13 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 4.0));
  tmpvar_12 = texture2D (_MainTex, P_13);
  c_1.xyz = (c_1.xyz + tmpvar_12.xyz);
  c_1.xyz = (c_1.xyz * 0.2);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _ScreenParams;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump float _BlurScale;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * vec2(2.0, 2.0)) + vec2(-1.0, -1.0));
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_BlurScale * 0.2) / _ScreenParams.xy);
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump vec2 _ScreenCenter;
uniform mediump float _FalloffExp;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = (xlv_TEXCOORD0.zw - _ScreenCenter);
  mediump float tmpvar_3;
  tmpvar_3 = sqrt(dot (tmpvar_2, tmpvar_2));
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((tmpvar_2 / tmpvar_3) * (pow (tmpvar_3, _FalloffExp) * xlv_TEXCOORD1));
  c_1.w = 0.0;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0.xy);
  c_1.xyz = tmpvar_5.xyz;
  lowp vec4 tmpvar_6;
  mediump vec2 P_7;
  P_7 = (xlv_TEXCOORD0.xy + tmpvar_4);
  tmpvar_6 = texture (_MainTex, P_7);
  c_1.xyz = (c_1.xyz + tmpvar_6.xyz);
  lowp vec4 tmpvar_8;
  mediump vec2 P_9;
  P_9 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 2.0));
  tmpvar_8 = texture (_MainTex, P_9);
  c_1.xyz = (c_1.xyz + tmpvar_8.xyz);
  lowp vec4 tmpvar_10;
  mediump vec2 P_11;
  P_11 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 3.0));
  tmpvar_10 = texture (_MainTex, P_11);
  c_1.xyz = (c_1.xyz + tmpvar_10.xyz);
  lowp vec4 tmpvar_12;
  mediump vec2 P_13;
  P_13 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 4.0));
  tmpvar_12 = texture (_MainTex, P_13);
  c_1.xyz = (c_1.xyz + tmpvar_12.xyz);
  c_1.xyz = (c_1.xyz * 0.2);
  _glesFragData[0] = c_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _ScreenParams;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump float _BlurScale;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * vec2(2.0, 2.0)) + vec2(-1.0, -1.0));
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_BlurScale * 0.2) / _ScreenParams.xy);
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump vec2 _ScreenCenter;
uniform mediump float _FalloffExp;
varying mediump vec4 xlv_TEXCOORD0;
varying mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = (xlv_TEXCOORD0.zw - _ScreenCenter);
  mediump float tmpvar_3;
  tmpvar_3 = sqrt(dot (tmpvar_2, tmpvar_2));
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((tmpvar_2 / tmpvar_3) * (pow (tmpvar_3, _FalloffExp) * xlv_TEXCOORD1));
  c_1.w = 0.0;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD0.xy);
  c_1.xyz = tmpvar_5.xyz;
  lowp vec4 tmpvar_6;
  mediump vec2 P_7;
  P_7 = (xlv_TEXCOORD0.xy + tmpvar_4);
  tmpvar_6 = texture2D (_MainTex, P_7);
  c_1.xyz = (c_1.xyz + tmpvar_6.xyz);
  lowp vec4 tmpvar_8;
  mediump vec2 P_9;
  P_9 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 2.0));
  tmpvar_8 = texture2D (_MainTex, P_9);
  c_1.xyz = (c_1.xyz + tmpvar_8.xyz);
  lowp vec4 tmpvar_10;
  mediump vec2 P_11;
  P_11 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 3.0));
  tmpvar_10 = texture2D (_MainTex, P_11);
  c_1.xyz = (c_1.xyz + tmpvar_10.xyz);
  lowp vec4 tmpvar_12;
  mediump vec2 P_13;
  P_13 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 4.0));
  tmpvar_12 = texture2D (_MainTex, P_13);
  c_1.xyz = (c_1.xyz + tmpvar_12.xyz);
  c_1.xyz = (c_1.xyz * 0.2);
  gl_FragData[0] = c_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp vec4 _ScreenParams;
uniform highp mat4 glstate_matrix_mvp;
uniform mediump float _BlurScale;
out mediump vec4 xlv_TEXCOORD0;
out mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 tmpvar_1;
  mediump vec2 tmpvar_2;
  tmpvar_1.xy = _glesMultiTexCoord0.xy;
  tmpvar_1.zw = ((_glesMultiTexCoord0.xy * vec2(2.0, 2.0)) + vec2(-1.0, -1.0));
  highp vec2 tmpvar_3;
  tmpvar_3 = ((_BlurScale * 0.2) / _ScreenParams.xy);
  tmpvar_2 = tmpvar_3;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_2;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump vec2 _ScreenCenter;
uniform mediump float _FalloffExp;
in mediump vec4 xlv_TEXCOORD0;
in mediump vec2 xlv_TEXCOORD1;
void main ()
{
  mediump vec4 c_1;
  mediump vec2 tmpvar_2;
  tmpvar_2 = (xlv_TEXCOORD0.zw - _ScreenCenter);
  mediump float tmpvar_3;
  tmpvar_3 = sqrt(dot (tmpvar_2, tmpvar_2));
  mediump vec2 tmpvar_4;
  tmpvar_4 = ((tmpvar_2 / tmpvar_3) * (pow (tmpvar_3, _FalloffExp) * xlv_TEXCOORD1));
  c_1.w = 0.0;
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD0.xy);
  c_1.xyz = tmpvar_5.xyz;
  lowp vec4 tmpvar_6;
  mediump vec2 P_7;
  P_7 = (xlv_TEXCOORD0.xy + tmpvar_4);
  tmpvar_6 = texture (_MainTex, P_7);
  c_1.xyz = (c_1.xyz + tmpvar_6.xyz);
  lowp vec4 tmpvar_8;
  mediump vec2 P_9;
  P_9 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 2.0));
  tmpvar_8 = texture (_MainTex, P_9);
  c_1.xyz = (c_1.xyz + tmpvar_8.xyz);
  lowp vec4 tmpvar_10;
  mediump vec2 P_11;
  P_11 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 3.0));
  tmpvar_10 = texture (_MainTex, P_11);
  c_1.xyz = (c_1.xyz + tmpvar_10.xyz);
  lowp vec4 tmpvar_12;
  mediump vec2 P_13;
  P_13 = (xlv_TEXCOORD0.xy + (tmpvar_4 * 4.0));
  tmpvar_12 = texture (_MainTex, P_13);
  c_1.xyz = (c_1.xyz + tmpvar_12.xyz);
  c_1.xyz = (c_1.xyz * 0.2);
  _glesFragData[0] = c_1;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_HIGHQUALITY_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES3"
}
}
 }
}
Fallback Off
}