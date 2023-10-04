using NaughtyAttributes;
using UDT.Scriptables;
using UDT.Scriptables.Utilities;
using UDT.Scriptables.Variables;
using UnityEngine;

namespace UDT.Feedbacks
{
    [CreateAssetMenu(fileName = "New Tween Feedback", menuName = "Rich/Feedbacks/Tween")]
    public class Tween : Feedback<Tween>
    {

        [Foldout("Feedback Settings")]
        [Header(" - Scriptable Variables - ")]
        [Tooltip("The Scriptable Variable associated with the current Value of the Feedback. You can tie this to a Field or Property in a Script," +
            "and the Feedback and also automatically handle acquiring these values to set.")]
        [Expandable]
        [AllowNesting]
        public ScriptableObjectAsset<ScriptableVariable> SerializedVariable;

        [Foldout("Tween Settings")]
        public AnimationCurve curve;

        [Foldout("Tween Settings")]
        [Tooltip("Sets the shape of the Animation Curve that is used for the motion of the Tween.")]
        public TweeningMode tweenMode = TweeningMode.Linear;
        [Foldout("Tween Settings")]
        [Tooltip("Sets the amount of bounces or sinusodial steps of the Tween Mode")]
        [ShowIf("UsesIterations")]
        public int iterations = 6;

        public bool UsesIterations()
        {
            return tweenMode == TweeningMode.EaseInElastic || tweenMode == TweeningMode.EaseOutElastic;
        }

        [Foldout("Tween Settings")]
        public ValueType valueType = ValueType.Float;
        [Foldout("Tween Settings")]
        public bool angleTween = false;

        [Foldout("Tween Settings")]
        [ShowIf("valueType", ValueType.Float)]
        public float minFloat = 0;
        [ShowIf("valueType", ValueType.Float)]
        public float maxFloat = 1;

        [Foldout("Tween Settings")]
        [ShowIf("valueType", ValueType.Vec2)]
        public Vector2 minVector2 = Vector2.zero;
        [Foldout("Tween Settings")]
        [ShowIf("valueType", ValueType.Vec2)]
        public Vector2 maxVector2 = Vector3.one;

        [Foldout("Tween Settings")]
        [ShowIf("valueType", ValueType.Vec3)]
        public Vector3 minVector3 = Vector3.zero;
        [Foldout("Tween Settings")]
        [ShowIf("valueType", ValueType.Vec3)]
        public Vector3 maxVector3 = new Vector3(1f,1f,1f);

        private (object, string) _targetFieldOrProperty;
        private float max = 0;

        void OnValidate()
        {
            switch (tweenMode)
            {
                case TweeningMode.Linear:
                    curve.ClearKeys();
                    curve.AddKey(0f, 0f);
                    curve.AddKey(1f, 1f);
                    break;
                case TweeningMode.EaseOut:
                    curve.ClearKeys();

                    curve.AddKey(new Keyframe(0, 0));
                    curve.AddKey(new Keyframe(1, 1));

                    curve.SmoothTangents(0, 0);
                    break;
                case TweeningMode.EaseIn:
                    curve.ClearKeys();

                    curve.AddKey(new Keyframe(0, 0));
                    curve.AddKey(new Keyframe(1, 1));

                    curve.SmoothTangents(1, 0);
                    break;
                case TweeningMode.EaseInOut:
                    curve.ClearKeys();

                    curve.AddKey(new Keyframe(0, 0));
                    curve.AddKey(new Keyframe(1, 1));

                    break;
                case TweeningMode.EaseInElastic:
                    curve.ClearKeys();

                    curve.AddKey(new Keyframe(0, 0));

                    max = Mathf.Pow(2, iterations);
                    for (int i = 1; i <= (float)iterations; i++)
                    {
                        if (i == iterations)
                            curve.AddKey(new Keyframe(1, 1));
                        else
                            curve.AddKey(new Keyframe(i / (float)iterations, (Mathf.Pow(2, i) * (((i % 2) * 2) - 1)) / max));
                    }
                    break;
                case TweeningMode.EaseOutElastic:
                    curve.ClearKeys();

                    curve.AddKey(new Keyframe(1, 1));

                    max = Mathf.Pow(2, iterations);
                    for (int i = 1; i <= iterations; i++)
                    {
                        if (i == iterations)
                            curve.AddKey(new Keyframe(0, 1));
                        else
                            curve.AddKey(new Keyframe(1 - (i / (float)iterations), (Mathf.Pow(2, i) * (((i % 2) * 2) - 1)) / max));
                    }
                    break;
            }

            if (valueType == ValueType.Float)
                SerializedVariable.GetScriptableAs<ScriptableVar_Float>(true);
            if (valueType == ValueType.Vec2)
                SerializedVariable.GetScriptableAs<ScriptableVar_Vector2>(true);
            if (valueType == ValueType.Vec3)
                SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>(true);
        }

        /// <summary>
        /// Sets the Value that is Tweened to the Instance's Field or Property with the given Name. 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldOrPropertyName"></param>
        public void SetTweenedValue<T>(object instance, string fieldOrPropertyName, T min, T max)
        {
            switch (typeof(T))
            {
                case var foo when foo.GetType() == typeof(float):
                    valueType = ValueType.Float;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Float>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    minFloat = (float)(object)min;
                    maxFloat = (float)(object)max;
                    break;
                case var foo when foo.GetType() == typeof(Vector2):
                    valueType = ValueType.Vec2;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Vector2>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    minVector2 = (Vector2)(object)min;
                    maxVector2 = (Vector2)(object)max;
                    break;
                case var foo when foo.GetType() == typeof(Vector3):
                    valueType = ValueType.Vec3;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    minVector3 = (Vector3)(object)min;
                    maxVector3 = (Vector3)(object)max;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// Sets the Value that is Tweened to the Instance's Field or Property with the given Name. 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="fieldOrPropertyName"></param>
        public void SetTweenedValue<T>(object instance, string fieldOrPropertyName)
        {
            switch (typeof(T))
            {
                case var foo when foo.GetType() == typeof(float):
                    valueType = ValueType.Float;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Float>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    break;
                case var foo when foo.GetType() == typeof(Vector2):
                    valueType = ValueType.Vec2;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Vector2>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    break;
                case var foo when foo.GetType() == typeof(Vector3):
                    valueType = ValueType.Vec3;

                    SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>(true).BindGetter<T>(instance, fieldOrPropertyName);
                    break;
                default:
                    return;
            }
        }

        public override void UpdateFeedback()
        {
            // Implement tweening based on the selected mode
            switch (valueType)
            {
                case ValueType.Float:
                    if (angleTween)
                        SerializedVariable.GetScriptableAs<ScriptableVar_Float>(true).Value = Mathf.LerpAngle(minFloat, maxFloat, curve.Evaluate(timeElapsed * speed));
                    else
                        SerializedVariable.GetScriptableAs<ScriptableVar_Float>(true).Value = Mathf.Lerp(minFloat, maxFloat, curve.Evaluate(timeElapsed * speed));
                    break;
                case ValueType.Vec2:
                    if(angleTween)
                        SerializedVariable.GetScriptableAs<ScriptableVar_Vector2>(true).Value = minVector2.LerpAngle(maxVector2, curve.Evaluate(timeElapsed * speed));
                    else
                        SerializedVariable.GetScriptableAs<ScriptableVar_Vector2>(true).Value = Vector2.Lerp(minVector2, maxVector2, curve.Evaluate(timeElapsed * speed));
                    break;
                case ValueType.Vec3:
                    if (angleTween)
                        SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>(true).Value = minVector3.LerpAngle(maxVector3, curve.Evaluate(timeElapsed * speed));
                    else
                        SerializedVariable.GetScriptableAs<ScriptableVar_Vector3>(true).Value = Vector3.Lerp(minVector3, maxVector3, curve.Evaluate(timeElapsed * speed));
                    break;
            }
        }
    }
    public enum TweeningMode
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
        EaseInElastic,
        EaseOutElastic,
        Custom
    }

    public enum ValueType
    {
        Float,
        Vec2,
        Vec3
    }
}
