#if UNITY_EDITOR
using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class DroneWindIntegrationTest
{
    private const float FixedDeltaTime = 0.02f;
    private const float Tolerance = 0.0001f;

    [Test]
    public void WindInfluence_IsAppliedCorrectly()
    {
        GameObject droneGO = new GameObject("TestDrone");
        Rigidbody rb = droneGO.AddComponent<Rigidbody>();
        DroneMovement droneMovement = droneGO.AddComponent<DroneMovement>();

        var rbField = typeof(DroneMovement).GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        rbField.SetValue(droneMovement, rb);

        GameObject windGO = new GameObject("TestWindController");
        WindController windController = windGO.AddComponent<WindController>();

        droneMovement.wind = windController;
        PlayerPrefs.SetInt("Wind", 1);
        droneMovement.SetWind(true);
        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        windController.minStrength = 1f;
        windController.maxStrength = 1f;
        windController.minChangeInterval = 1000f;
        windController.maxChangeInterval = 1000f;


        typeof(WindController)
            .GetMethod("ChangeWind", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(windController, null);

        Vector3 expectedWind = windController.GetWind();
        float windStrength = expectedWind.magnitude;

        droneMovement.windInfluence = 2.0f;

        Vector3 expectedMovement = expectedWind * droneMovement.windInfluence * FixedDeltaTime;

        Vector3 initialPosition = rb.position;

        droneMovement.Move(Vector3.zero, 0f, 0f);


        Vector3 finalPosition = rb.position;
        Vector3 actualMovement = finalPosition - initialPosition;


        
            Assert.That(actualMovement.x, Is.EqualTo(expectedMovement.x).Within(Tolerance), "Ruch X jest nieprawid³owy");
            Assert.That(actualMovement.y, Is.EqualTo(expectedMovement.y).Within(Tolerance), "Ruch Y jest nieprawid³owy");
            Assert.That(actualMovement.z, Is.EqualTo(expectedMovement.z).Within(Tolerance), "Ruch Z jest nieprawid³owy");

        Object.DestroyImmediate(droneGO);
        Object.DestroyImmediate(windGO);
        PlayerPrefs.DeleteKey("Wind");
    }


    [Test]
    public void WindInfluence_IsNotAppliedWhenDisabled()
    {
        GameObject droneGO = new GameObject("TestDrone");
        Rigidbody rb = droneGO.AddComponent<Rigidbody>();
        DroneMovement droneMovement = droneGO.AddComponent<DroneMovement>();

        var rbField = typeof(DroneMovement).GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        rbField.SetValue(droneMovement, rb);

        GameObject windGO = new GameObject("TestWindController");
        WindController windController = windGO.AddComponent<WindController>();

        droneMovement.wind = windController;

        PlayerPrefs.SetInt("Wind", 0);
        droneMovement.SetWind(false);

        rb.isKinematic = false;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        windController.minStrength = 1f;
        windController.maxStrength = 1f;

        typeof(WindController)
            .GetMethod("ChangeWind", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(windController, null);

        droneMovement.windInfluence = 2.0f;

        Vector3 initialPosition = rb.position;

        droneMovement.Move(Vector3.zero, 0f, 0f);

        Vector3 finalPosition = rb.position;
        Vector3 actualMovement = finalPosition - initialPosition;

   
        
            Assert.That(actualMovement.x, Is.EqualTo(0f).Within(Tolerance), "Ruch X powinien byæ zerowy, gdy wiatr jest nieaktywny");
            Assert.That(actualMovement.y, Is.EqualTo(0f).Within(Tolerance), "Ruch Y powinien byæ zerowy, gdy wiatr jest nieaktywny");
            Assert.That(actualMovement.z, Is.EqualTo(0f).Within(Tolerance), "Ruch Z powinien byæ zerowy, gdy wiatr jest nieaktywny");
     

        Object.DestroyImmediate(droneGO);
        Object.DestroyImmediate(windGO);
        PlayerPrefs.DeleteKey("Wind");
    }
}
#endif