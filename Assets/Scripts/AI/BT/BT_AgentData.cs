using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime {
    [System.Serializable]
    public class SharedAgentData : SharedVariable<BT_AgentData> {
        public static implicit operator SharedAgentData (BT_AgentData value) {
            return new SharedAgentData { Value = value };
        }
    }

    public class BT_AgentData {
        public Transform target;
        public Heal availableHeal;
        public HealthSystem health;
        public GoTo goTo;
        public int agentState;
    }
}