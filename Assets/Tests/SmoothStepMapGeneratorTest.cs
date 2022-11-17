using System.Collections;
using System.Collections.Generic;
using Map;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class SmoothStepMapGeneratorTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void SmoothStepMapGeneratorTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SmoothStepMapGeneratorTestWithEnumeratorPasses()
    {
        
        //Tilemap til = new Tilemap();
        List<List<int>> map = MapGenerator.GenerateArray(50, 50, true);
        float seed = Random.value;
        Assert.DoesNotThrow(()=>MapGenerator.RandomWalkTopSmoothed(map, seed, 3 ));
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
