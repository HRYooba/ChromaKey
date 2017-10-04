using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChromaKey
{
    public class ChromaKeyController : MonoBehaviour
    {

        [Space(5), HeaderAttribute("WebCam Settings")]
        public int deviceNum = 0;
        public int width = 1920;
        public int height = 1080;
        public int FPS = 30;

        [Space(10), HeaderAttribute("ChromaKey Settings")]
        public Material chromaKeyMat;
        public RawImage displayImage;
        public Color detectionColor;   
		[Range(0, 5)]
		public float threshold;
		public Texture inputTexture;

        private WebCamTexture webCamTexture;

        // Use this for initialization
        void Start()
        {
            InitWebCamDevice();
			InitChromaKeyMat();
        }

        // Update is called once per frame
        void Update()
        {
			// Update input value
			chromaKeyMat.SetColor("_Color", detectionColor);
			chromaKeyMat.SetFloat("_Threshold", threshold);
        }

        private void InitWebCamDevice()
        {
            WebCamDevice[] devices = WebCamTexture.devices;
            for (var i = 0; i < devices.Length; i++)
            {
                Debug.Log(devices[i].name);
            }
            webCamTexture = new WebCamTexture(devices[deviceNum].name, width, height, FPS);
            webCamTexture.Play();
        }

		private void InitChromaKeyMat() {

			// Set up Material
            chromaKeyMat.SetTexture("_MainTex", webCamTexture);
			chromaKeyMat.SetTexture("_InputTex", inputTexture);
			chromaKeyMat.SetColor("_Color", detectionColor);
			chromaKeyMat.SetFloat("_Threshold", threshold);

			// Display set material
			displayImage.material = chromaKeyMat;
		}
    }
}
