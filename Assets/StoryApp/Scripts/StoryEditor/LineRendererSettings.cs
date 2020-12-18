using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StoryApp
{
    public class LineRendererSettings : MonoBehaviour
    {
        //public static Func<string, Color> OnGetColor
        public static Action<int, LineRenderer> OnLineColor;

        private void OnEnable()
        {
            Node.OnLineInitalSettings += LineInitialSettings;
            MouseController.OnSetLineEndPos += UpdateLineEndPosition;
        }

        // LineRenderer Start Position Assignments
        public void LineInitialSettings(int outLinkCount, LineRenderer lineLink, Vector3 startPosition, Vector3 endPosition)
        {
            lineLink.enabled = true;
            lineLink.material = new Material(Shader.Find("Sprites/Default"));

            OnLineColor?.Invoke(outLinkCount, lineLink);

            lineLink.positionCount = 2;
            lineLink.startWidth = 0.2f;
            lineLink.SetPosition(0, startPosition);
            lineLink.SetPosition(1, endPosition);
        }

        public void UpdateLineEndPosition(LineRenderer lineLink, Vector3 endPosition)
        {
            lineLink.positionCount = 2;
            lineLink.SetPosition(1, endPosition);
            //lineLink.SetPosition(1, currentLink.endPos);
        }

        private void OnDisable()
        {
            Node.OnLineInitalSettings -= LineInitialSettings;
        }
    }
}
