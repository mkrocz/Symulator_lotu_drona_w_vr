#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using System.Reflection;

public class WindControllerTests
{
    [Test]
    public void Wind_IsWithinExpectedStrength_AfterManualChange()
    {
        var go = new GameObject();
        var wind = go.AddComponent<WindController>();

        wind.minStrength = 1f;
        wind.maxStrength = 3f;

        MethodInfo changeWindMethod = typeof(WindController).GetMethod(
            "ChangeWind",
            BindingFlags.NonPublic | BindingFlags.Instance
        );
        changeWindMethod.Invoke(wind, null);

        Vector3 w = wind.GetWind();
        float magnitude = w.magnitude;


        Assert.That(magnitude, Is.GreaterThanOrEqualTo(wind.minStrength), "Si³a wiatru jest za ma³a.");
        Assert.That(magnitude, Is.LessThanOrEqualTo(wind.maxStrength), "Si³a wiatru jest za du¿a.");

        Object.DestroyImmediate(go);
    }
}
#endif