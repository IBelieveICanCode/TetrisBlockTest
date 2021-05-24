using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private readonly List<GateBlock> _blocks = new List<GateBlock>();
    public List<GateBlock> Blocks => _blocks;
    public void DisableBlocks()
    {
        foreach (GateBlock block in _blocks)
        {
            block.IsPrepared = true;
        }
    }
}
