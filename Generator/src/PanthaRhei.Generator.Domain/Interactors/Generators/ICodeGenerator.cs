﻿namespace LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Generators
{
    /// <summary>
    /// Represents a contract for an object that executes a code generation process.
    /// </summary>
    public interface ICodeGenerator
    {
        /// <summary>
        /// Starts the code generation execution process.
        /// </summary>
        void Execute();
    }
}