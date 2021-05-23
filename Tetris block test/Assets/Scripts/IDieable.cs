using System;

public interface IDieable
{
    event EventHandler Death;
    void Die();
}