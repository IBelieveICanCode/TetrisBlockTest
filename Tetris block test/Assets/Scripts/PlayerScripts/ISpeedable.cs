public interface ISpeedable
{
    void SpeedUp();
    void SlowDown();
    void Stop();
    bool IsSlowedDown { get; }

}
