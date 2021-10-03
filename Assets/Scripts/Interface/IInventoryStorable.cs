using UnityEngine;

public interface IInventoryStorable 
{
     bool IsInSocket { get; set; }
     void OffInventorySocket();
}
