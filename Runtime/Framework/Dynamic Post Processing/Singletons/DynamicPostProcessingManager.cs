using System;
using System.Collections.Generic;
using System.Linq;
using Rich.Extensions;
using Rich.System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Rich.DynamicPostProcessing
{
    public class DynamicPostProcessingManager : Singleton<DynamicPostProcessingManager>
    {
        private static List<Volume> postProcessVolumes = new();
        private static List<VolumeComponent> ppVolumeComponents = new();
        private Type[] _postProcessComponentTypes;
        public static Type[] postProcessComponentTypes 
        {
            get 
            {
                return singleton._postProcessComponentTypes;
            }
        }
        private static Volume globalVolume;


        void Awake()
        {
            _postProcessComponentTypes = typeof(IPostProcessComponent).GetInterfacingTypes();
        }

        protected override void SceneChanged(Scene previousScene, Scene nextScene)
        {
            postProcessVolumes.Clear();
            postProcessVolumes.AddRange(FindObjectsOfType<Volume>());
            globalVolume = postProcessVolumes.FirstOrDefault(volume => volume.isGlobal);
            ppVolumeComponents.AddRange(postProcessVolumes.SelectMany(volume => volume.profile.components));
        }

        public static Volume CreateGlobalVolume()
        {
            if(globalVolume == null)
                globalVolume = new GameObject("Global Volume").AddComponent<Volume>();
            return globalVolume;
        }

        public static VolumeComponent GetPostEffect(Type effectType, int index = -1)
        {
            var ofType = ppVolumeComponents.Where(component => component.GetType() == effectType);
            if (index == -1)
            {
                return ofType.FirstOrDefault();
            }
            else
            {
                if (ofType.Count() > index)
                    return ofType.ToArray()[index];
            }

            return null;
        }

        public static T GetPostEffect<T>(int index = -1) where T : VolumeComponent
        {
            if(index == -1)
                return ppVolumeComponents.OfType<T>().FirstOrDefault();
            else
            {
                var componentsOfType = ppVolumeComponents.OfType<T>();
                if (componentsOfType.Count() > index)
                    return componentsOfType.ToArray()[index];
            }

            return null;
        }

        public static void AddPostEffect<T>() where T : VolumeComponent
        {
            if(globalVolume == null)
            {
                Debug.LogWarning("No global volume found, a new one will be created.");
                CreateGlobalVolume();
                return;
            }

            var postEffect = globalVolume.profile.Add<T>();
        }
    }
}
