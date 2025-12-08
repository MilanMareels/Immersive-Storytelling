using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinwheel.Tutorial.SkyShader
{
    [ExecuteInEditMode]
    public class SkyMaterialController : MonoBehaviour
    {
        public static readonly int SUN_DIRECTION = Shader.PropertyToID("_SunDirection");

        public Material material;
        public Transform mainLight;

        void Update()
        {
            if (material != null && mainLight != null)
            {
                material.SetVector(SUN_DIRECTION, mainLight.forward);
            }
        }
    }
}