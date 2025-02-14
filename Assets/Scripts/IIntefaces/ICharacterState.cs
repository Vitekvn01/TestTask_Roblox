using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    public void UpdateState(IInputController inputController);
}
