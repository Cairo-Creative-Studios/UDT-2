using UnityEngine;
using Rich.System;
using Rich.Scriptables;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace ScriptablesTests
{
    public class ScriptablesTestRuntime : Runtime<ScriptablesTestRuntime, ScriptableTestData>
    {
        TestClass testClass = new TestClass(5);
        List<TestClass> testInstances = new();
        float timer = 0;
        void Start()
        {
            Bind<TestClass>("testInt", BindMode.Set, testClass, "testInt");
        }

        void Update()
        {
            testInstances.Add(new TestClass(5));
            Bind<TestClass>("testInt", BindMode.Get, testInstances[testInstances.Count - 1], "testInt");
            Debug.Log(testInstances.Count + " instances bound to testInt");
        }
        public class TestClass
        {
            public int testInt;

            public TestClass(int testInt)
            {
                this.testInt = testInt;
            }
        }
    }
}