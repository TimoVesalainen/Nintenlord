using Microsoft.Extensions.Logging;
using Nintenlord.StateMachines.Combinatorics;
using Nintenlord.StateMachines.Finite;
using Nintenlord.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nintenlord.StateMachines
{
    public static partial class StateMachineHelpers
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

        public static bool IsAccepted<TState, TInput>(
            this IStateMachine<TState, TInput> machine,
            IEnumerable<TInput> input)
        {
            TState start = machine.StartState;

            foreach (var item in input)
            {
                start = machine.Transition(start, item);
            }

            return machine.IsFinalState(start);
        }

        public static IEnumerable<TState> GetFinalStates<TState, TInput>(this IFiniteStateMachine<TState, TInput> stateMachine)
        {
            return stateMachine.States.Where(x => stateMachine.IsFinalState(x));
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

        public static LoggingStateMachine2<TState, TInput> LogToLogger<TState, TInput>(this IStateMachine<TState, TInput> original, ILogger logger, LogLevel level = LogLevel.Information, Func<TInput, TState, TState, string> format = null)
        {
            return new LoggingStateMachine2<TState, TInput>(original, logger, format, level);
        }

        public static TransformInputStateMachine<TState, TInput, TInput2> TransformInput<TState, TInput, TInput2>(this IStateMachine<TState, TInput> original, Func<TInput2, TInput> transformInput)
        {
            return new TransformInputStateMachine<TState, TInput, TInput2>(original, transformInput);
        }

        public static TwoStepStateMachine<TState, TInput> TwoSteps<TState, TInput>(this IStateMachine<TState, TInput> original)
        {
            return new TwoStepStateMachine<TState, TInput>(original);
        }

        public static MultiStepStateMachine<TState, TInput> MultipleSteps<TState, TInput>(this IStateMachine<TState, TInput> original)
        {
            return new MultiStepStateMachine<TState, TInput>(original);
        }

        public static UnionStateMachine<TState1, TState2, TInput> Union<TState1, TState2, TInput>(this IStateMachine<TState1, TInput> machine1, IStateMachine<TState2, TInput> machine2, Either<TState1, TState2> start)
        {
            return new UnionStateMachine<TState1, TState2, TInput>(machine1, machine2, start);
        }

        public static TransformStateMachine<TStateIn, TStateOut, TInput> TransformState<TStateIn, TStateOut, TInput>(this IStateMachine<TStateIn, TInput> machine, Func<TStateIn, TStateOut> funcOut, Func<TStateOut, TStateIn> funcIn)
        {
            return new TransformStateMachine<TStateIn, TStateOut, TInput>(machine, funcOut, funcIn);
        }

        public static AddCommandsStateMachine<TState, TInput1, TInput2> AddCommands<TState, TInput1, TInput2>(this IStateMachine<TState, TInput1> machine, Func<TInput2, TState, TState> newCommandsHandler)
        {
            return new AddCommandsStateMachine<TState, TInput1, TInput2>(machine, newCommandsHandler);
        }

        public static DictionaryStateMachine<TState, TInput> ToDictionaryStateMachine<TState, TInput>(this IFiniteStateMachine<TState, TInput> machine, IEnumerable<TInput> inputs)
        {
            Dictionary<(TState, TInput), TState> dictionary = new();

            foreach (var state in machine.States)
            {
                foreach (var input in inputs)
                {
                    dictionary[(state, input)] = machine.Transition(state, input);
                }
            }

            return new DictionaryStateMachine<TState, TInput>(dictionary, machine.IsFinalState, machine.StartState);
        }
    }
}
