using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TG.Core
{
    public class SceneEnder : MonoBehaviour
    {
        public event Action onSceneEnd;

        public void EndScene() { if (onSceneEnd != null) { onSceneEnd(); } }
    }
}