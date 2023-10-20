using UnityEngine;

namespace AI.Math
{
    public class AIMath
    {
        /// <summary>
        /// Calculates a damped oscillation with a sigmoidal scaling, returning a value between 0 and -1 as the input x varies.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float SigmoidalDampedOscillation(float x)
        {
            return (1 / (1 + System.MathF.Exp(-x))) * -((Mathf.Sin(x * 2) * 2.71828f) / 2);
        }

        public static float Sigmoid(float x)
        {
            return 1 / (1 + System.MathF.Exp(-x));
        }

        public static float SigmoidDerivative(float x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }

        public static float ReLU(float x)
        {
            return x > 0 ? x : 0;
        }

        public static float OscillatingReLU(float x)
        {
            return x > 0 ? x : (Mathf.Sin(x * 2) * 2.71828f) / 2;
        }

        public static float OscillatingReLUDerivative(float x)
        {
            return x > 0 ? 1 : (Mathf.Cos(x * 2) * 2.71828f) / 2;
        }
    }
}