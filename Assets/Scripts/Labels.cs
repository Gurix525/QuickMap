using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labels : MonoBehaviour
{
    [field: SerializeField] public TextWindow TextWindow { get; set; }
    [field: SerializeField] public GameObject Menu { get; set; }
    [field: SerializeField] public Input Input { get; set; }
}
