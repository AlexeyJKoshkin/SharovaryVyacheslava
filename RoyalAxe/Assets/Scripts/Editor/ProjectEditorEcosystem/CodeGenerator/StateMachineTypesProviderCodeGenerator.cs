/*#region

using CodeGenerator;
using EldritchHorror;
using EldritchHorror.Core;
using GameKit.Editor;
using System;

#endregion

namespace Editor.EldritchHorrorEditorEcosystem
{
    public class StateMachineTypesProviderCodeGenerator : ClassGenerator, IEldritchHorrorCodeGenerator
    {
        public StateMachineTypesProviderCodeGenerator() : base(typeof(StateMachineTypesProvider).Name) { }

        public Type FileType => typeof(StateMachineTypesProvider);

        public void GenerateToFile()
        {
            GeneratorUtils.WriteCode<StateMachineTypesProvider>(this);
        }

        protected override string[] GetUsings()
        {
            return new[] {"System", "System.Collections.Generic"};
        }

        protected override void AppendClassBodyScope()
        {
            AppendLine(" public static IEnumerable<Type> AllStateMachineType()");
            BeginTabBracers();
            GenerateFor<IStateMachine>();
            EndTabBracers();

            AppendLine(" public static IEnumerable<Type> AllStatesMachineType()");
            BeginTabBracers();
            //   GenerateFor<IStateMachineState>();
            EndTabBracers();
        }

        private void GenerateFor<T>()
        {
            AppendTab();
            ReflectionHelper.GetAllTypesInSolutionWithInterface<T>()
                            .ForEach(e => AppendLine($" yield return typeof({e.FullName});"));
        }
    }
}*/

