using System.Collections;
using System.Collections.Generic;
using Map;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    static int[] values = new int[] { 1,2,3,4, 5, 6 , 7,8,9};
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestMapGeneratorSmooth1([ValueSource("values")] int value)
    {
        
            List<List<int>> map = MapGenerator.GenerateArray(50, 15, true);
            float seed = Random.value;
            Assert.DoesNotThrow(()=>MapGenerator.RandomWalkTopSmoothed(map, seed, value ));
            yield return null;
    }
    
    
}
