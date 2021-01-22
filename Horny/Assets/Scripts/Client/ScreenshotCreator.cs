using System;
using UnityEngine;

namespace Client
{
    public class ScreenshotCreator : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown("s"))
            {
                ScreenCapture.CaptureScreenshot($"/Users/alina.chupahina/Projects/PukPuk/Screenshot{Time.time}.jpg");
            }
        }
    }
}
