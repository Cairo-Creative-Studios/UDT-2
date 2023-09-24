using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;
using Rich.System;
using Rich.Controllables.Extensions;
using Rich.Serialization;

namespace SerializationTests
{
    internal class SerializationTestRuntime : Runtime<SerializationTestRuntime, SerializationTestData>
    {
        public Vector2 testVec = new Vector2(1.234f, 3.455f);
        public Vector2 testOutputVec;

        void Update()
        {
            var testByteArr = testVec.ConvertToByte();
            testOutputVec = testByteArr.ConvertToVector2();
        }
    }
}
