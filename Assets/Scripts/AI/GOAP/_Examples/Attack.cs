using UnityEngine;
using SGoap;

namespace ImplementationExamples {
    namespace GOAP {
        //GOAP attack example
        public class Attack : BasicAction {
            //Prevent agent from attacking constantly.
            public override float CooldownTime => 0.8f;

            //Action to execute when it's called
            public override EActionStatus Perform () {
                Debug.Log("Attack Player");
                return EActionStatus.Success;
            }
        }
    }
}