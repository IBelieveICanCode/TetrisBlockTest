public interface ISpeedable
{
    void SpeedUp(float amount);
    void Stop();
    float Speed { get; set; }
    float MaxSpeed { get; }
    float MinSpeed { get; }

}
