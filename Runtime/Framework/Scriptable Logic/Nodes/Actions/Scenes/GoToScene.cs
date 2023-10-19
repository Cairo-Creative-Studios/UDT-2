using UDT.Scriptables.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UDT.Scriptables.Actions
{
    public class GoToScene : ActionNode
    {
        [Input(backingValue = ShowBackingValue.Unconnected)]
        public Variables.Scene scene;

        public override void Process()
        {
            SceneManager.LoadScene(scene.Value);
            base.Process();
        }
    }
}