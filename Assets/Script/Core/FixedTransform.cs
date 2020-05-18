using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedTransform : FixedComponent
{
    public Fixed3 position = Fixed3.zero;
    public Fixed3 rotation = Fixed3.zero;
    public Fixed3 scale = new Fixed3(1, 1, 1);


}
