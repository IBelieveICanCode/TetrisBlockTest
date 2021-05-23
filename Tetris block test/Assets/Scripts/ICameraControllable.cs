using UnityEngine;
public interface ICameraControllable
{
    Transform Position { get; }
    float OrtographicCoef { get; }
}
