#if UNITY_EDITOR
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class VirtualInputTests
{
    [Test]
    public void LeverInput_CorrectlyNormalized()
    {
        var go = new GameObject();
        var input = go.AddComponent<VirtualInput>();
        var lever = new GameObject().transform;
        input.moveLever = lever;

        lever.localEulerAngles = new Vector3(10f, 0f, 10f);
        Vector2 result = (Vector2)typeof(VirtualInput)
            .GetMethod("GetLeverInput2D", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(input, new object[] { lever });

        Assert.LessOrEqual(result.magnitude, 1f);
        Assert.Greater(result.magnitude, 0f);


        Object.DestroyImmediate(go);
    }
}
#endif