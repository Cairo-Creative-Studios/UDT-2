using NaughtyAttributes;
using UDT.Scriptables.Utilities;
using UnityEngine.SceneManagement;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Scenes/Scene")]
    public sealed class Scene : VariableNode<string>
    {
        [Dropdown("GetScenes")]
        public new string value;

        public DropdownList<string> GetScenes()
        {
            DropdownList<string> returnList = new();

            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < sceneCount; i++)
            {
                returnList.Add(SceneUtility.GetScenePathByBuildIndex(i), SceneUtility.GetScenePathByBuildIndex(i));
            }

            return returnList;
        }
    }
}

