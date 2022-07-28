using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {   
        private IAction currentAction = null;

        public void StartAction(IAction action) {
            if (currentAction == action) return;

            currentAction?.Cancel();
            currentAction = action;
        }
    }
}
