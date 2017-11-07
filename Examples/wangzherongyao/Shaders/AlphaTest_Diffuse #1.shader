Shader "S_Game_Scene/Light_VertexLit/AlphaTest_Diffuse" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
 _AlphaTex ("千万不要填，系统用的", 2D) = "white" {}
 _Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
 Pass {
  Tags { "LIGHTMODE"="VertexLM" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Opaque" }
Program "vp" {
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = (texture2D (_MainTex, xlv_TEXCOORD0) * _Color);
  color_1.w = tmpvar_2.w;
  lowp float x_3;
  x_3 = (tmpvar_2.w - _Cutoff);
  if ((x_3 < 0.0)) {
    discard;
  };
  color_1.xyz = (tmpvar_2.xyz * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz));
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2 = (texture (_MainTex, xlv_TEXCOORD0) * _Color);
  color_1.w = tmpvar_2.w;
  lowp float x_3;
  x_3 = (tmpvar_2.w - _Cutoff);
  if ((x_3 < 0.0)) {
    discard;
  };
  color_1.xyz = (tmpvar_2.xyz * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD1).xyz));
  _glesFragData[0] = color_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_OFF" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_2.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * _Color);
  color_1.w = tmpvar_3.w;
  lowp float x_4;
  x_4 = (tmpvar_3.w - _Cutoff);
  if ((x_4 < 0.0)) {
    discard;
  };
  color_1.xyz = (tmpvar_3.xyz * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz));
  gl_FragData[0] = color_1;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_OFF" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
void main ()
{
  lowp vec4 color_1;
  lowp vec4 tmpvar_2;
  tmpvar_2.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_2.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (tmpvar_2 * _Color);
  color_1.w = tmpvar_3.w;
  lowp float x_4;
  x_4 = (tmpvar_3.w - _Cutoff);
  if ((x_4 < 0.0)) {
    discard;
  };
  color_1.xyz = (tmpvar_3.xyz * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD1).xyz));
  _glesFragData[0] = color_1;
}



#endif"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD2 = (_Object2World * _glesVertex);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _FogOfWar;
uniform highp float _SceneSize;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  mediump float fog_1;
  lowp vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (texture2D (_MainTex, xlv_TEXCOORD0) * _Color);
  color_2.w = tmpvar_3.w;
  lowp float x_4;
  x_4 = (tmpvar_3.w - _Cutoff);
  if ((x_4 < 0.0)) {
    discard;
  };
  color_2.xyz = (tmpvar_3.xyz * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz));
  highp vec2 tmpvar_5;
  tmpvar_5.x = ((xlv_TEXCOORD2.x / _SceneSize) + 0.5);
  tmpvar_5.y = ((xlv_TEXCOORD2.z / _SceneSize) + 0.5);
  lowp float tmpvar_6;
  tmpvar_6 = texture2D (_FogOfWar, tmpvar_5).w;
  fog_1 = tmpvar_6;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.3, fog_1);
  fog_1 = tmpvar_7;
  mediump vec3 tmpvar_8;
  tmpvar_8 = (color_2.xyz * tmpvar_7);
  color_2.xyz = tmpvar_8;
  gl_FragData[0] = color_2;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD2 = (_Object2World * _glesVertex);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _FogOfWar;
uniform highp float _SceneSize;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
void main ()
{
  mediump float fog_1;
  lowp vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = (texture (_MainTex, xlv_TEXCOORD0) * _Color);
  color_2.w = tmpvar_3.w;
  lowp float x_4;
  x_4 = (tmpvar_3.w - _Cutoff);
  if ((x_4 < 0.0)) {
    discard;
  };
  color_2.xyz = (tmpvar_3.xyz * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD1).xyz));
  highp vec2 tmpvar_5;
  tmpvar_5.x = ((xlv_TEXCOORD2.x / _SceneSize) + 0.5);
  tmpvar_5.y = ((xlv_TEXCOORD2.z / _SceneSize) + 0.5);
  lowp float tmpvar_6;
  tmpvar_6 = texture (_FogOfWar, tmpvar_5).w;
  fog_1 = tmpvar_6;
  mediump float tmpvar_7;
  tmpvar_7 = max (0.3, fog_1);
  fog_1 = tmpvar_7;
  mediump vec3 tmpvar_8;
  tmpvar_8 = (color_2.xyz * tmpvar_7);
  color_2.xyz = tmpvar_8;
  _glesFragData[0] = color_2;
}



#endif"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_ON" }
"!!GLES


#ifdef VERTEX

attribute vec4 _glesVertex;
attribute vec4 _glesMultiTexCoord0;
attribute vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD2 = (_Object2World * _glesVertex);
}



#endif
#ifdef FRAGMENT

uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _FogOfWar;
uniform highp float _SceneSize;
uniform sampler2D _AlphaTex;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec2 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
void main ()
{
  mediump float fog_1;
  lowp vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture2D (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture2D (_AlphaTex, xlv_TEXCOORD0).x;
  lowp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * _Color);
  color_2.w = tmpvar_4.w;
  lowp float x_5;
  x_5 = (tmpvar_4.w - _Cutoff);
  if ((x_5 < 0.0)) {
    discard;
  };
  color_2.xyz = (tmpvar_4.xyz * (2.0 * texture2D (unity_Lightmap, xlv_TEXCOORD1).xyz));
  highp vec2 tmpvar_6;
  tmpvar_6.x = ((xlv_TEXCOORD2.x / _SceneSize) + 0.5);
  tmpvar_6.y = ((xlv_TEXCOORD2.z / _SceneSize) + 0.5);
  lowp float tmpvar_7;
  tmpvar_7 = texture2D (_FogOfWar, tmpvar_6).w;
  fog_1 = tmpvar_7;
  mediump float tmpvar_8;
  tmpvar_8 = max (0.3, fog_1);
  fog_1 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = (color_2.xyz * tmpvar_8);
  color_2.xyz = tmpvar_9;
  gl_FragData[0] = color_2;
}



#endif"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_ON" }
"!!GLES3#version 300 es


#ifdef VERTEX


in vec4 _glesVertex;
in vec4 _glesMultiTexCoord0;
in vec4 _glesMultiTexCoord1;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp vec4 _MainTex_ST;
uniform highp vec4 unity_LightmapST;
out highp vec2 xlv_TEXCOORD0;
out highp vec2 xlv_TEXCOORD1;
out highp vec4 xlv_TEXCOORD2;
void main ()
{
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = ((_glesMultiTexCoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
  xlv_TEXCOORD2 = (_Object2World * _glesVertex);
}



#endif
#ifdef FRAGMENT


layout(location=0) out mediump vec4 _glesFragData[4];
uniform sampler2D _MainTex;
uniform lowp vec4 _Color;
uniform lowp float _Cutoff;
uniform sampler2D unity_Lightmap;
uniform sampler2D _FogOfWar;
uniform highp float _SceneSize;
uniform sampler2D _AlphaTex;
in highp vec2 xlv_TEXCOORD0;
in highp vec2 xlv_TEXCOORD1;
in highp vec4 xlv_TEXCOORD2;
void main ()
{
  mediump float fog_1;
  lowp vec4 color_2;
  lowp vec4 tmpvar_3;
  tmpvar_3.xyz = texture (_MainTex, xlv_TEXCOORD0).xyz;
  tmpvar_3.w = texture (_AlphaTex, xlv_TEXCOORD0).x;
  lowp vec4 tmpvar_4;
  tmpvar_4 = (tmpvar_3 * _Color);
  color_2.w = tmpvar_4.w;
  lowp float x_5;
  x_5 = (tmpvar_4.w - _Cutoff);
  if ((x_5 < 0.0)) {
    discard;
  };
  color_2.xyz = (tmpvar_4.xyz * (2.0 * texture (unity_Lightmap, xlv_TEXCOORD1).xyz));
  highp vec2 tmpvar_6;
  tmpvar_6.x = ((xlv_TEXCOORD2.x / _SceneSize) + 0.5);
  tmpvar_6.y = ((xlv_TEXCOORD2.z / _SceneSize) + 0.5);
  lowp float tmpvar_7;
  tmpvar_7 = texture (_FogOfWar, tmpvar_6).w;
  fog_1 = tmpvar_7;
  mediump float tmpvar_8;
  tmpvar_8 = max (0.3, fog_1);
  fog_1 = tmpvar_8;
  mediump vec3 tmpvar_9;
  tmpvar_9 = (color_2.xyz * tmpvar_8);
  color_2.xyz = tmpvar_9;
  _glesFragData[0] = color_2;
}



#endif"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_OFF" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_OFF" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_OFF" "_FOG_OF_WAR_ON" }
"!!GLES3"
}
SubProgram "gles " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_ON" }
"!!GLES"
}
SubProgram "gles3 " {
Keywords { "_SEPERATE_ALPHA_TEX_ON" "_FOG_OF_WAR_ON" }
"!!GLES3"
}
}
 }
}
Fallback "Transparent/Cutout/VertexLit"
}