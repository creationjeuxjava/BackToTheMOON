// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32085,y:32651|diff-11-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:2,x:32976,y:32489;n:type:ShaderForge.SFN_Tex2d,id:3,x:32976,y:32691,ptlb:MainTex,ptin:_MainTex,tex:416c91bf2fbc2554186fa39c03b1cebf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:4,x:33311,y:32998,ptlb:night,ptin:_night,glob:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:5,x:33374,y:33191,ptlb:half night,ptin:_halfnight,glob:False,c1:0.4705882,c2:0.126572,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:6,x:32969,y:33262,ptlb:day,ptin:_day,glob:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7,x:32665,y:32792|A-3-RGB,B-284-RGB;n:type:ShaderForge.SFN_Multiply,id:8,x:32641,y:32984|A-3-RGB,B-298-RGB;n:type:ShaderForge.SFN_Multiply,id:9,x:32626,y:33157|A-3-RGB,B-6-RGB;n:type:ShaderForge.SFN_ValueProperty,id:10,x:32572,y:32598,ptlb:node_10,ptin:_node_10,glob:False,v1:1;n:type:ShaderForge.SFN_If,id:11,x:32363,y:32741|A-10-OUT,B-2-Y,GT-7-OUT,EQ-8-OUT,LT-9-OUT;n:type:ShaderForge.SFN_Time,id:24,x:33197,y:32547;n:type:ShaderForge.SFN_Lerp,id:283,x:32347,y:33105;n:type:ShaderForge.SFN_Tex2d,id:284,x:33138,y:32883,ptlb:node_284,ptin:_node_284,tex:faad7474c289beb4d86b6cacdadffed6,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:298,x:32959,y:33027,ptlb:node_284_copy,ptin:_node_284_copy,tex:9fbd55bef791b004dbbc130297e57cf4,ntxv:0,isnm:False;proporder:3-4-5-6-10-284-298;pass:END;sub:END;*/

Shader "Custom/levelSwitch" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _night ("night", Color) = (0,0,0,1)
        _halfnight ("half night", Color) = (0.4705882,0.126572,0,1)
        _day ("day", Color) = (1,1,1,1)
        _node_10 ("node_10", Float ) = 1
        _node_284 ("node_284", 2D) = "white" {}
        _node_284_copy ("node_284_copy", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _day;
            uniform float _node_10;
            uniform sampler2D _node_284; uniform float4 _node_284_ST;
            uniform sampler2D _node_284_copy; uniform float4 _node_284_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float node_11_if_leA = step(_node_10,objPos.g);
                float node_11_if_leB = step(objPos.g,_node_10);
                float2 node_370 = i.uv0;
                float4 node_3 = tex2D(_MainTex,TRANSFORM_TEX(node_370.rg, _MainTex));
                float3 node_11 = lerp((node_11_if_leA*(node_3.rgb*_day.rgb))+(node_11_if_leB*(node_3.rgb*tex2D(_node_284,TRANSFORM_TEX(node_370.rg, _node_284)).rgb)),(node_3.rgb*tex2D(_node_284_copy,TRANSFORM_TEX(node_370.rg, _node_284_copy)).rgb),node_11_if_leA*node_11_if_leB);
                finalColor += diffuseLight * node_11;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _day;
            uniform float _node_10;
            uniform sampler2D _node_284; uniform float4 _node_284_ST;
            uniform sampler2D _node_284_copy; uniform float4 _node_284_copy_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                float4 objPos = mul ( _Object2World, float4(0,0,0,1) );
                i.normalDir = normalize(i.normalDir);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                float node_11_if_leA = step(_node_10,objPos.g);
                float node_11_if_leB = step(objPos.g,_node_10);
                float2 node_371 = i.uv0;
                float4 node_3 = tex2D(_MainTex,TRANSFORM_TEX(node_371.rg, _MainTex));
                float3 node_11 = lerp((node_11_if_leA*(node_3.rgb*_day.rgb))+(node_11_if_leB*(node_3.rgb*tex2D(_node_284,TRANSFORM_TEX(node_371.rg, _node_284)).rgb)),(node_3.rgb*tex2D(_node_284_copy,TRANSFORM_TEX(node_371.rg, _node_284_copy)).rgb),node_11_if_leA*node_11_if_leB);
                finalColor += diffuseLight * node_11;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
