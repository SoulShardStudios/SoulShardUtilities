using UnityEngine;
using System.Collections.Generic;
using System;

namespace SoulShard.Math
{
    /// <summary>
    /// The type of easing.
    /// </summary>
    public enum EaseType
    {
        linear,
        sine,
        quad,
        cubic,
        quart,
        quint,
        expo,
        circ,
        back,
        elastic,
        bounce
    }

    /// <summary>
    /// The direction of the easing.
    /// </summary>
    public enum EaseDirection
    {
        In,
        Out,
        InOut
    }

    /// <summary>
    /// Contains a variety of easing functions.
    /// 
    /// All of this is modified from https://easings.net/
    /// </summary>
    public class EaseManager
    {
        static readonly Dictionary<(EaseType, EaseDirection), Func<float, float>> easingFunctions =
            new Dictionary<(EaseType, EaseDirection), Func<float, float>>()
            {            
            { (EaseType.linear, EaseDirection.In), EaseLinear },
            { (EaseType.linear, EaseDirection.Out), EaseLinear },
            { (EaseType.linear, EaseDirection.InOut), EaseLinear },
            { (EaseType.sine, EaseDirection.In), EaseInSine },
            { (EaseType.sine, EaseDirection.Out), EaseOutSine },
            { (EaseType.sine, EaseDirection.InOut), EaseInOutSine },
            { (EaseType.quad, EaseDirection.In), EaseInQuad },
            { (EaseType.quad, EaseDirection.Out), EaseOutQuad },
            { (EaseType.quad, EaseDirection.InOut), EaseInOutQuad },
            { (EaseType.cubic, EaseDirection.In), EaseInCubic },
            { (EaseType.cubic, EaseDirection.Out), EaseOutCubic },
            { (EaseType.cubic, EaseDirection.InOut), EaseInOutCubic },
            { (EaseType.quart, EaseDirection.In), EaseInQuart },
            { (EaseType.quart, EaseDirection.Out), EaseOutQuart },
            { (EaseType.quart, EaseDirection.InOut), EaseInOutQuart },
            { (EaseType.quint, EaseDirection.In), EaseInQuint },
            { (EaseType.quint, EaseDirection.Out), EaseOutQuint },
            { (EaseType.quint, EaseDirection.InOut), EaseInOutQuint },
            { (EaseType.expo, EaseDirection.In), EaseInExpo },
            { (EaseType.expo, EaseDirection.Out), EaseOutExpo },
            { (EaseType.expo, EaseDirection.InOut), EaseInOutExpo },
            { (EaseType.circ, EaseDirection.In), EaseInCirc },
            { (EaseType.circ, EaseDirection.Out), EaseOutCirc },
            { (EaseType.circ, EaseDirection.InOut), EaseInOutCirc },
            { (EaseType.back, EaseDirection.In), EaseInBack },
            { (EaseType.back, EaseDirection.Out), EaseOutBack },
            { (EaseType.back, EaseDirection.InOut), EaseInOutBack },
            { (EaseType.elastic, EaseDirection.In), EaseInElastic },
            { (EaseType.elastic, EaseDirection.Out), EaseOutElastic },
            { (EaseType.elastic, EaseDirection.InOut), EaseInOutElastic },
            { (EaseType.bounce, EaseDirection.In), EaseInBounce },
            { (EaseType.bounce, EaseDirection.Out), EaseOutBounce },
            { (EaseType.bounce, EaseDirection.InOut), EaseInOutBounce },
            };

        public static float Ease(
            float x,
            EaseType type,
            EaseDirection direction = EaseDirection.In
        ) => easingFunctions[(type, direction)](x);

        public static float EaseLinear(float x) => x;
        public static float EaseInSine(float x) => 1 - Mathf.Cos(x * Mathf.PI / 2);

        public static float EaseOutSine(float x) => Mathf.Sin(x * Mathf.PI / 2);

        public static float EaseInOutSine(float x) => -(Mathf.Cos(Mathf.PI * x) - 1) / 2;

        public static float EaseInQuad(float x) => x * x;

        public static float EaseOutQuad(float x) => 1 - (1 - x) * (1 - x);

        public static float EaseInOutQuad(float x) =>
            x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;

        public static float EaseInCubic(float x) => x * x * x;

        public static float EaseOutCubic(float x) => 1 - Mathf.Pow(1 - x, 3);

        public static float EaseInOutCubic(float x) =>
            x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;

        public static float EaseInQuart(float x) => x * x * x * x;

        public static float EaseOutQuart(float x) => 1 - Mathf.Pow(1 - x, 4);

        public static float EaseInOutQuart(float x) =>
            x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;

        public static float EaseInQuint(float x) => x * x * x * x * x;

        public static float EaseOutQuint(float x) => 1 - Mathf.Pow(1 - x, 5);

        public static float EaseInOutQuint(float x) =>
            x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;

        public static float EaseInExpo(float x) => x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);

        public static float EaseOutExpo(float x) => x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);

        public static float EaseInOutExpo(float x) =>
            x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? Mathf.Pow(2, 20 * x - 10) / 2
                        : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;

        public static float EaseInCirc(float x) => 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));

        public static float EaseOutCirc(float x) => Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));

        public static float EaseInOutCirc(float x) =>
            x < 0.5
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;

        public static float EaseInBack(float x) => 2.70158f * x * x * x - 1.70158f * x * x;

        public static float EaseOutBack(float x) =>
            1 + 2.70158f * Mathf.Pow(x - 1, 3) + 1.70158f * Mathf.Pow(x - 1, 2);

        public static float EaseInOutBack(float x) =>
            x < 0.5
                ? (Mathf.Pow(2 * x, 2) * (3.5949095f * 2 * x - 2.5949095f)) / 2
                : (Mathf.Pow(2 * x - 2, 2) * (3.5949095f * (x * 2 - 2) + 2.5949095f) + 2) / 2;

        public static float EaseInElastic(float x) =>
            x == 0
                ? 0
                : x == 1
                    ? 1
                    : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * (2 * Mathf.PI) / 3);

        public static float EaseOutElastic(float x) =>
            x == 0
                ? 0
                : x == 1
                    ? 1
                    : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * (2 * Mathf.PI) / 3) + 1;

        public static float EaseInOutElastic(float x) =>
            x == 0
                ? 0
                : x == 1
                    ? 1
                    : x < 0.5
                        ? -(
                              Mathf.Pow(2, 20 * x - 10)
                              * Mathf.Sin((20 * x - 11.125f) * (2 * Mathf.PI) / 4.5f)
                          ) / 2
                        : (
                              Mathf.Pow(2, -20 * x + 10)
                              * Mathf.Sin((20 * x - 11.125f) * (2 * Mathf.PI) / 4.5f)
                          ) / 2
                          + 1;

        public static float EaseInBounce(float x) => 1 - EaseOutBounce(1 - x);

        public static float EaseOutBounce(float x)
        {
            var n1 = 7.5625f;
            var d1 = 2.75f;
            if (x < 1 / d1)
                return n1 * x * x;
            else if (x < 2 / d1)
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            else if (x < 2.5 / d1)
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            else
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }

        public static float EaseInOutBounce(float x) =>
            x < 0.5 ? (1 - EaseOutBounce(1 - 2 * x)) / 2 : (1 + EaseOutBounce(2 * x - 1)) / 2;
    }

    /// <summary>
    /// A serializeable easing that can be embedded in monobehaviors.
    /// </summary>
    [Serializable]
    public class Easing
    {
        public EaseType type;
        public EaseDirection direction;
        public float Ease(float x) => EaseManager.Ease(x, type, direction);
    }
}