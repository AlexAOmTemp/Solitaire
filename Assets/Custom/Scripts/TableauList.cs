using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TableauList : MonoBehaviour
{
    [FormerlySerializedAs("CardStackSlots")] public List<TableauSlot> TableauSlots = new();
}
