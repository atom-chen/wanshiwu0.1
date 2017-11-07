Shader "SGame_Post/OutlineFilter" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "" {}
 _BlendFactor ("BlendFactor", Float) = 0.6
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
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _TexelOffset0;
uniform highp vec4 _TexelOffset1;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec4 xlv_TEXCOORD2;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 uv_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  tmpvar_5 = tmpvar_1.xyxy;
  uv_2 = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (uv_2 + _TexelOffset0);
  tmpvar_3 = tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (uv_2 + _TexelOffset1);
  tmpvar_4 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _BlendFactor;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec4 xlv_TEXCOORD2;
void main ()
{
  mediump vec4 c0_1;
  mediump vec4 c_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  c_2 = tmpvar_3;
  lowp float tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD1.xy).w;
  c0_1.x = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD1.zw).w;
  c0_1.y = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD2.xy).w;
  c0_1.z = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD2.zw).w;
  c0_1.w = tmpvar_7;
  mediump vec4 tmpvar_8;
  tmpvar_8 = (c0_1 - c_2.w);
  c_2.xyz = mix (vec3(0.004, 0.008, 0.1), c_2.xyz, vec3((1.0 - (
    clamp ((sqrt(dot (tmpvar_8, tmpvar_8)) * 1000.0), 0.0, 1.0)
   * _BlendFactor))));
  gl_FragData[0] = c_2;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _TexelOffset0;
uniform highp vec4 _TexelOffset1;
out mediump vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
out mediump vec4 xlv_TEXCOORD2;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 uv_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  tmpvar_5 = tmpvar_1.xyxy;
  uv_2 = tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (uv_2 + _TexelOffset0);
  tmpvar_3 = tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (uv_2 + _TexelOffset1);
  tmpvar_4 = tmpvar_7;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _BlendFactor;
in mediump vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
in mediump vec4 xlv_TEXCOORD2;
void main ()
{
  mediump vec4 c0_1;
  mediump vec4 c_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture (_MainTex, xlv_TEXCOORD0);
  c_2 = tmpvar_3;
  lowp float tmpvar_4;
  tmpvar_4 = texture (_MainTex, xlv_TEXCOORD1.xy).w;
  c0_1.x = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD1.zw).w;
  c0_1.y = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture (_MainTex, xlv_TEXCOORD2.xy).w;
  c0_1.z = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD2.zw).w;
  c0_1.w = tmpvar_7;
  mediump vec4 tmpvar_8;
  tmpvar_8 = (c0_1 - c_2.w);
  c_2.xyz = mix (vec3(0.004, 0.008, 0.1), c_2.xyz, vec3((1.0 - (
    clamp ((sqrt(dot (tmpvar_8, tmpvar_8)) * 1000.0), 0.0, 1.0)
   * _BlendFactor))));
  _glesFragData[0] = c_2;
}



#endif"
}
SubProgram "gles " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _TexelOffset0;
uniform highp vec4 _TexelOffset1;
uniform highp vec4 _TexelOffset2;
uniform highp vec4 _TexelOffset3;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 uv_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  mediump vec4 tmpvar_6;
  mediump vec4 tmpvar_7;
  tmpvar_7 = tmpvar_1.xyxy;
  uv_2 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (uv_2 + _TexelOffset0);
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9 = (uv_2 + _TexelOffset1);
  tmpvar_4 = tmpvar_9;
  highp vec4 tmpvar_10;
  tmpvar_10 = (uv_2 + _TexelOffset2);
  tmpvar_5 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11 = (uv_2 + _TexelOffset3);
  tmpvar_6 = tmpvar_11;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform mediump float _BlendFactor;
varying mediump vec2 xlv_TEXCOORD0;
varying mediump vec4 xlv_TEXCOORD1;
varying mediump vec4 xlv_TEXCOORD2;
varying mediump vec4 xlv_TEXCOORD3;
varying mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec4 c1_1;
  mediump vec4 c0_2;
  mediump vec4 c_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0);
  c_3 = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = texture2D (_MainTex, xlv_TEXCOORD1.xy).w;
  c0_2.x = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture2D (_MainTex, xlv_TEXCOORD1.zw).w;
  c0_2.y = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD2.xy).w;
  c0_2.z = tmpvar_7;
  lowp float tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD2.zw).w;
  c0_2.w = tmpvar_8;
  mediump vec4 tmpvar_9;
  tmpvar_9 = (c0_2 - c_3.w);
  lowp float tmpvar_10;
  tmpvar_10 = texture2D (_MainTex, xlv_TEXCOORD3.xy).w;
  c1_1.x = tmpvar_10;
  lowp float tmpvar_11;
  tmpvar_11 = texture2D (_MainTex, xlv_TEXCOORD3.zw).w;
  c1_1.y = tmpvar_11;
  lowp float tmpvar_12;
  tmpvar_12 = texture2D (_MainTex, xlv_TEXCOORD4.xy).w;
  c1_1.z = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = texture2D (_MainTex, xlv_TEXCOORD4.zw).w;
  c1_1.w = tmpvar_13;
  mediump vec4 tmpvar_14;
  tmpvar_14 = (c1_1 - c_3.w);
  c_3.xyz = mix (vec3(0.004, 0.008, 0.1), c_3.xyz, vec3((1.0 - (
    clamp ((sqrt((
      dot (tmpvar_9, tmpvar_9)
     + 
      dot (tmpvar_14, tmpvar_14)
    )) * 1000.0), 0.0, 1.0)
   * _BlendFactor))));
  gl_FragData[0] = c_3;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_HIGHQUALITY_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _TexelOffset0;
uniform highp vec4 _TexelOffset1;
uniform highp vec4 _TexelOffset2;
uniform highp vec4 _TexelOffset3;
out mediump vec2 xlv_TEXCOORD0;
out mediump vec4 xlv_TEXCOORD1;
out mediump vec4 xlv_TEXCOORD2;
out mediump vec4 xlv_TEXCOORD3;
out mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec2 tmpvar_1;
  tmpvar_1 = _glesMultiTexCoord0.xy;
  highp vec4 uv_2;
  mediump vec4 tmpvar_3;
  mediump vec4 tmpvar_4;
  mediump vec4 tmpvar_5;
  mediump vec4 tmpvar_6;
  mediump vec4 tmpvar_7;
  tmpvar_7 = tmpvar_1.xyxy;
  uv_2 = tmpvar_7;
  highp vec4 tmpvar_8;
  tmpvar_8 = (uv_2 + _TexelOffset0);
  tmpvar_3 = tmpvar_8;
  highp vec4 tmpvar_9;
  tmpvar_9 = (uv_2 + _TexelOffset1);
  tmpvar_4 = tmpvar_9;
  highp vec4 tmpvar_10;
  tmpvar_10 = (uv_2 + _TexelOffset2);
  tmpvar_5 = tmpvar_10;
  highp vec4 tmpvar_11;
  tmpvar_11 = (uv_2 + _TexelOffset3);
  tmpvar_6 = tmpvar_11;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = tmpvar_1;
  xlv_TEXCOORD1 = tmpvar_3;
  xlv_TEXCOORD2 = tmpvar_4;
  xlv_TEXCOORD3 = tmpvar_5;
  xlv_TEXCOORD4 = tmpvar_6;
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform mediump float _BlendFactor;
in mediump vec2 xlv_TEXCOORD0;
in mediump vec4 xlv_TEXCOORD1;
in mediump vec4 xlv_TEXCOORD2;
in mediump vec4 xlv_TEXCOORD3;
in mediump vec4 xlv_TEXCOORD4;
void main ()
{
  mediump vec4 c1_1;
  mediump vec4 c0_2;
  mediump vec4 c_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture (_MainTex, xlv_TEXCOORD0);
  c_3 = tmpvar_4;
  lowp float tmpvar_5;
  tmpvar_5 = texture (_MainTex, xlv_TEXCOORD1.xy).w;
  c0_2.x = tmpvar_5;
  lowp float tmpvar_6;
  tmpvar_6 = texture (_MainTex, xlv_TEXCOORD1.zw).w;
  c0_2.y = tmpvar_6;
  lowp float tmpvar_7;
  tmpvar_7 = texture (_MainTex, xlv_TEXCOORD2.xy).w;
  c0_2.z = tmpvar_7;
  lowp float tmpvar_8;
  tmpvar_8 = texture (_MainTex, xlv_TEXCOORD2.zw).w;
  c0_2.w = tmpvar_8;
  mediump vec4 tmpvar_9;
  tmpvar_9 = (c0_2 - c_3.w);
  lowp float tmpvar_10;
  tmpvar_10 = texture (_MainTex, xlv_TEXCOORD3.xy).w;
  c1_1.x = tmpvar_10;
  lowp float tmpvar_11;
  tmpvar_11 = texture (_MainTex, xlv_TEXCOORD3.zw).w;
  c1_1.y = tmpvar_11;
  lowp float tmpvar_12;
  tmpvar_12 = texture (_MainTex, xlv_TEXCOORD4.xy).w;
  c1_1.z = tmpvar_12;
  lowp float tmpvar_13;
  tmpvar_13 = texture (_MainTex, xlv_TEXCOORD4.zw).w;
  c1_1.w = tmpvar_13;
  mediump vec4 tmpvar_14;
  tmpvar_14 = (c1_1 - c_3.w);
  c_3.xyz = mix (vec3(0.004, 0.008, 0.1), c_3.xyz, vec3((1.0 - (
    clamp ((sqrt((
      dot (tmpvar_9, tmpvar_9)
     + 
      dot (tmpvar_14, tmpvar_14)
    )) * 1000.0), 0.0, 1.0)
   * _BlendFactor))));
  _glesFragData[0] = c_3;
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