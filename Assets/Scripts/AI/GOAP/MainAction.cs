namespace SGoap {
    //Base for action that holds the minimum info
    public abstract class MainAction : Action, IDataBind<MainInfo> {
        public MainInfo AgentData;

        //Allow to bind this data
        public void Bind (MainInfo data) {
            AgentData = data;
        }
    }
}
