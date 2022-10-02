using System;
using CodeGenerator;

namespace ProjectEditorEcoSystem
{
    public interface IProjectCodeGenerator : ICodeGenerator
    {
        Type FileType { get; }
        void GenerateToFile();
    }
}