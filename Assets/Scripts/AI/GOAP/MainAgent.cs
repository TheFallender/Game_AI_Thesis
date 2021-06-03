namespace SGoap {
    //Base agent
    public class MainAgent : Agent {
        //Agent data, the most important thing
        //of this class (and it's only purpose)
        public MainInfo AgentData;

        //Run this to set the info
        protected void Awake () {
            AgentData = new MainInfo {
                Agent = this,
            };

            //Get the components that implement the base info data and relate them to this agent        
            foreach (var dependency in GetComponentsInChildren<IDataBind<MainInfo>>())
                dependency.Bind(AgentData);

            //Get the components that implement the agent data and relate them to this agent        
            foreach (var dependency in GetComponentsInChildren<IDataBind<Agent>>())
                dependency.Bind(this);
        }
    }
}