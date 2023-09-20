using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Rich.ScriptableEvents
{
    internal class ScriptableVariable<T> : ScriptableObject
    {
        public T Value;
    }
}
