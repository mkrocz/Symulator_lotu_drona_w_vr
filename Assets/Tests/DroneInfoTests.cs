#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;

public class DroneInfoTests
{
    [Test]
    public void GetPosition_ReturnsCorrectPosition()
    {
 
        var go = new GameObject();
        var rb = go.AddComponent<Rigidbody>();
        var info = go.AddComponent<DroneInfo>();
        info.rb = rb;

        Vector3 initialPosition = new Vector3(10f, 20f, 30f);
        rb.position = initialPosition;

        Vector3 position = info.GetPosition();

        Assert.That(position.x, Is.EqualTo(initialPosition.x).Within(0.001f));
        Assert.That(position.y, Is.EqualTo(initialPosition.y).Within(0.001f));
        Assert.That(position.z, Is.EqualTo(initialPosition.z).Within(0.001f));

        Object.DestroyImmediate(go);
    }
}
#endif