namespace Nintenlord.StateMachines
{
    public class StatefulObject<TState, TInput>
    {
        readonly IStateMachine<TState, TInput> stateMachine;
        TState state;

        public TState CurrentState => state;

        public StatefulObject(IStateMachine<TState, TInput> stateMachine)
        {
            this.stateMachine = stateMachine;
            state = stateMachine.StartState;
        }

        public void Update(TInput input)
        {
            state = stateMachine.Transition(state, input);
        }

        public bool IsFinished()
        {
            return stateMachine.IsFinalState(state);
        }
    }
}
