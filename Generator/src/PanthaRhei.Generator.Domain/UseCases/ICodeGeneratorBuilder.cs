﻿namespace LiquidVisions.PanthaRhei.Generator.Domain.UseCases
{
    /// <summary>
    /// Represents a contract of an object that is able to build a <see cref="ICodeGenerator"/>.
    /// </summary>
    public interface ICodeGeneratorBuilder
    {
        /// <summary>
        /// Executes the building command.
        /// </summary>
        /// <returns><seealso cref="ICodeGenerator"/></returns>
        ICodeGenerator Build();
    }
}
