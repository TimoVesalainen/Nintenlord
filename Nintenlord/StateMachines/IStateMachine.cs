using System;
using System.Collections.Generic;
using System.IO;

namespace Nintenlord.StateMachines
{
    public interface IStateMachine<TState, in TInput>
    {
        TState StartState { get; }
        bool IsFinalState(TState state);
        TState Transition(TState currentState, TInput input);
    }

    public static class StateMachineHelpers
    {
        public static IEnumerable<TState> RunUntilFinalState<TState, TInput>(
            this IStateMachine<TState, TInput> machine,
            IEnumerable<TInput> input)
        {
            return machine.RunUntilFinalState(input, machine.StartState);
        }

        public static IEnumerable<TState> RunUntilFinalState<TState, TInput>(
            this IStateMachine<TState, TInput> machine,
            IEnumerable<TInput> input,
            TState start)
        {
            foreach (var item in input)
            {
                yield return start;

                if (machine.IsFinalState(start))
                {
                    yield break;
                }

                start = machine.Transition(start, item);
            }
            yield return start;
        }

        public static HistoryKeepingStateMachine<TState, TInput>
            GetHistoryKeeping<TState, TInput>(this IStateMachine<TState, TInput> machine)
        {
            return new HistoryKeepingStateMachine<TState, TInput>(machine);
        }

        public static CombinedStateMachine<TState1, TState2, TInput1, TInput2>
            Combine<TState1, TState2, TInput1, TInput2>(
            this IStateMachine<TState1, TInput1> machine1,
            IStateMachine<TState2, TInput2> machine2)
        {
            return new CombinedStateMachine<TState1, TState2, TInput1, TInput2>(machine1, machine2);
        }

        public static LoggingStateMachine<TState, TInput> LogToFile<TState, TInput>(this IStateMachine<TState, TInput> original, TextWriter writer)
        {
            void LogState(TState state, bool isNew)
            {
                writer.WriteLine($"{(isNew ? "New" : "Old")} state is {state}");
            }

            void LogInput(TInput input)
            {
                writer.WriteLine($"Input is {input}");
            }

            return new LoggingStateMachine<TState, TInput>(original, LogState, LogInput);
        }

        public static LoggingStateMachine<TState, TInput> LogToFunc<TState, TInput>(this IStateMachine<TState, TInput> original, Action<string> writer)
        {
            void LogState(TState state, bool isNew)
            {
                writer($"{(isNew ? "New" : "Old")} state is {state}");
            }

            void LogInput(TInput input)
            {
                writer($"Input is {input}");
            }

            return new LoggingStateMachine<TState, TInput>(original, LogState, LogInput);
        }
    }
}
