using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Coins : PlayerPrefsResource
{
    protected override string PlayerPrefsKey => "Resource_Coins";

    protected override int DefaultValue => 0;
}
