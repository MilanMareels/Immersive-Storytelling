Shader "PinwheelStudio/Tutorial_SkyShader"
{
	Properties
	{
		_SkyColor("Sky Color", Color) = (0.35, 0.55, 0.75, 1.0)
		_HorizonColor("Horizon Color", Color) = (1.0, 0.95, 0.85, 1.0)
		_GroundColor("Ground Color", Color) = (0.4, 0.4, 0.4, 1.0)
		_HorizonThickness("Horizon Thickness", Range(0.0, 1.0)) = 1.0
		_HorizonExponent("Horizon Exponent", Float) = 3.0

		_SunColor("Sun Color", Color) = (1.0, 0.97, 0.9, 1.0)
		_SunSize("Sun Size", Float) = 0.07
		_SunSoftEdge("Sun Soft Edge", Float) = 0.5
		_SunGlow("Sun Glow", Float) = 0.45
		_SunDirection("Sun Direction", Vector) = (0, -1, -1, 0)

		_CloudTex("Cloud Texture", 2D) = "white" {}
		_OverheadCloudColor("Overhead Cloud Color", Color) = (1, 1, 1, 0.5)
		_OverheadCloudAltitude("Overhead Cloud Altitude", Float) = 1000
		_OverheadCloudSize("Overhead Cloud Size", Float) = 10
		_OverheadCloudAnimationSpeed("Overhead Cloud Animation Speed", Float) = 100
		_OverheadCloudFlowDirectionX("Overhead Cloud Flow X", Float) = 1
		_OverheadCloudFlowDirectionZ("Overhead Cloud Flow X", Float) = 1
		_OverheadCloudRemapMin("Overhead Cloud Remap Min", Float) = -0.5
		_OverheadCloudRemapMax("Overhead Cloud Remap Max", Float) = 1.5
	}
	SubShader
	{
		Tags { "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 localPos : TEXCOORD1;
			};

			uniform fixed4 _SkyColor;
			uniform fixed4 _HorizonColor;
			uniform fixed4 _GroundColor;
			uniform fixed _HorizonThickness;
			uniform fixed _HorizonExponent;

			uniform fixed4 _SunColor;
			uniform fixed _SunSize;
			uniform fixed _SunSoftEdge;
			uniform fixed _SunGlow;
			uniform fixed4 _SunDirection;

			uniform sampler2D _CloudTex;
			uniform fixed4 _OverheadCloudColor;
			uniform fixed _OverheadCloudAltitude;
			uniform fixed _OverheadCloudSize;
			uniform fixed _OverheadCloudAnimationSpeed;
			uniform fixed _OverheadCloudFlowDirectionX;
			uniform fixed _OverheadCloudFlowDirectionZ;
			uniform fixed _OverheadCloudRemapMin;
			uniform fixed _OverheadCloudRemapMax;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.localPos = v.vertex;
				return o;
			}

			float InverseLerpUnclamped(float a, float b, float value)
			{
				//adding a==b check if needed
				return (value - a) / (b - a + 0.00000001);
			}

			fixed4 BlendOverlay(fixed4 src, fixed4 des)
			{
				fixed4 result = src * src.a + des * (1 - src.a);
				result.a = 1 - (1 - src.a) * (1 - des.a);
				return result;
			}

			fixed4 BlendPremul(fixed4 src, fixed4 des)
			{
				return src * src.a + des;
			}

			void CalculateSkyGradientColor(
				fixed4 viewDir,
				fixed4 skyColor, fixed4 horizonColor, fixed4 groundColor,
				fixed horizonThickness, fixed horizonExponent,
				out fixed4 skyBlendColor, out fixed4 horizonBlendColor)
			{
				skyBlendColor = lerp(groundColor, skyColor, viewDir.y > 0);
				horizonBlendColor = float4(0, 0, 0, 0);

				fixed horizonBlendFactor = saturate(1 - InverseLerpUnclamped(0, horizonThickness, abs(viewDir.y)));
				horizonBlendFactor = pow(horizonBlendFactor, horizonExponent);
				horizonBlendColor = fixed4(horizonColor.xyz, horizonBlendFactor * horizonColor.a);
			}

			float SqrDistance(float3 pt1, float3 pt2)
			{
				float3 v = pt2 - pt1;
				return dot(v, v);
			}

			void CalculateSunColor(
				fixed4 viewDir,
				fixed4 tintColor,
				fixed size, fixed softEdge, fixed glow,
				fixed4 direction,
				out fixed4 color)
			{
				fixed3 sunPos = -direction;
				fixed3 rayDir = viewDir.xyz;

				fixed rayLength = 1 / dot(rayDir, sunPos); //potential div by zero
				fixed3 intersectionPoint = rayDir * rayLength;
				fixed sqrDistanceToSun = SqrDistance(intersectionPoint, sunPos);

				fixed fSize = 1 - InverseLerpUnclamped(0, size * size * 0.25, sqrDistanceToSun);
				fixed fGlow = 1 - InverseLerpUnclamped(0, size * size * 400 * glow * glow, sqrDistanceToSun);
				fGlow = saturate(fGlow);

				fixed4 clear = fixed4(0, 0, 0, 0);
				fixed4 white = fixed4(1, 1, 1, 1);

				fixed4 texColor = white;
				fixed4 glowColor = fixed4(tintColor.xyz, fGlow * fGlow * fGlow * fGlow * fGlow * fGlow * glow);
				fixed fSoftEdge = saturate(lerp(0, 1 / softEdge, fSize)); //potential div by zero
				texColor = lerp(glowColor, texColor, fSoftEdge);

				color = texColor + glowColor * glowColor.a * glowColor.a;
				color *= tintColor;
				color.a = saturate(color.a);

				fixed dotProduct = dot(viewDir.xyz, sunPos);
				color = lerp(clear, color, dotProduct >= 0);
			}

			void CalculateOverheadCloudColor(
				fixed4 viewDir,
				fixed4 cloudColor,
				fixed cloudAltitude,
				fixed cloudSize,
				fixed animationSpeed,
				fixed flowX, fixed flowZ,
				fixed remapMin, fixed remapMax,
				out fixed4 color)
			{
				fixed3 rayDir = viewDir;
				float3 cloudPlaneOrigin = float3(0, cloudAltitude, 0);
				float3 cloudPlaneNormal = float3(0, 1, 0);

				float rayLength = cloudAltitude / dot(rayDir, cloudPlaneNormal); //potential div by zero;
				float3 intersectionPoint = rayDir * rayLength;

				fixed loop;
#if SHADER_API_MOBILE
				loop = 1;
#else
				loop = 2;
#endif

				fixed noise = 0;
				fixed sample = 0;
				fixed noiseSize = cloudSize * 1000;
				fixed noiseAmp = 1;
				fixed2 span = fixed2(flowX, flowZ) * animationSpeed * _Time.y * 0.0001;
				for (fixed i = 0; i < loop; ++i)
				{
					sample = tex2D(_CloudTex, (intersectionPoint.xz) / noiseSize + span)  * noiseAmp; //potential div by zero
					noise += sample;
					noiseSize *= 0.5;
					noiseAmp *= 0.5;
				}
				noise = noise * 0.5 + 0.5;
				noise = noise * cloudColor.a;
				noise = lerp(remapMin, remapMax, noise);
				noise = saturate(noise);

				color = fixed4(cloudColor.rgb, noise);

				float sqrCloudDiscRadius = 100000000;
				float sqrDistanceToCloudPlaneOrigin = SqrDistance(intersectionPoint, cloudPlaneOrigin);

				fixed fDistance = saturate(1 - InverseLerpUnclamped(0, sqrCloudDiscRadius, sqrDistanceToCloudPlaneOrigin));
				color.a *= fDistance * fDistance;

				fixed4 clear = fixed4(0, 0, 0, 0);
				color = lerp(clear, color, viewDir.y > 0);
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 localPos = fixed4(i.localPos.xyz, 1);
				fixed4 viewDir = fixed4(normalize(localPos.xyz), 1);
				fixed4 color = fixed4(0,0,0,0);

				fixed4 skyBlendColor;
				fixed4 horizonBlendColor;
				CalculateSkyGradientColor(
					viewDir,
					_SkyColor, _HorizonColor, _GroundColor,
					_HorizonThickness, _HorizonExponent,
					skyBlendColor, horizonBlendColor);
				color = BlendOverlay(skyBlendColor, color);

				fixed4 sunColor;
				CalculateSunColor(
					viewDir,
					_SunColor,
					_SunSize, _SunSoftEdge, _SunGlow,
					_SunDirection,
					sunColor);
				color = BlendPremul(sunColor, color);

				fixed4 overheadCloudColor;
				CalculateOverheadCloudColor(
					viewDir,
					_OverheadCloudColor,
					_OverheadCloudAltitude,
					_OverheadCloudSize,
					_OverheadCloudAnimationSpeed,
					_OverheadCloudFlowDirectionX,
					_OverheadCloudFlowDirectionZ,
					_OverheadCloudRemapMin,
					_OverheadCloudRemapMax,
					overheadCloudColor);
				color = BlendOverlay(overheadCloudColor, color);

				color = BlendOverlay(horizonBlendColor, color);
				return color;
			}
			ENDCG
		}
	}
}
