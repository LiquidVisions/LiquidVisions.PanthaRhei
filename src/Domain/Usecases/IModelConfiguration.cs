using System;
using System.Collections.Generic;
using LiquidVisions.PanthaRhei.Domain.Entities;
using LiquidVisions.PanthaRhei.Domain.Models;

namespace LiquidVisions.PanthaRhei.Domain.Usecases
{
    /// <summary>
    /// Represents a contract for a generic Model Configuration.
    /// </summary>
    public interface IModelConfiguration
    {
        /// <summary>
        /// Gets the indexes of an entity of a certain <seealso cref="Type"/>
        /// </summary>
        /// <param name="entityType">The <seealso cref="Type"/> of the entity</param>
        /// <returns>An array of index names</returns>
        string[] GetIndexes(Type entityType);

        /// <summary>
        /// Gets the keys of an entity of a certain <seealso cref="Type"/>
        /// </summary>
        /// <param name="entityType">The <seealso cref="Type"/> of the entity.</param>
        /// <returns>An array of key names</returns>
        string[] GetKeys(Type entityType);

        /// <summary>
        /// Gets the size of the property of an entity of a certain <seealso cref="Type"/>
        /// </summary>
        /// <param name="entityType">The <seealso cref="Type"/> of the entity</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns></returns>
        int? GetSize(Type entityType, string propertyName);


        /// <summary>
        /// Gets a boolean indicating whether the property of an entity of a certain <seealso cref="Type"/> is required.
        /// </summary>
        /// <param name="entityType">The <seealso cref="Type"/> of the entity</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns></returns>
        bool GetIsRequired(Type entityType, string propertyName);

        /// <summary>
        /// Gets Relationship information of an entity.
        /// </summary>
        /// <param name="entity"><see cref="Entity"/>.</param>
        /// <returns>A <seealso cref="RelationshipDto">List of RelationshipDto's</seealso> representing the available information of the <seealso cref="Relationship"/></returns>
        List<RelationshipDto> GetRelationshipInfo(Entity entity);
    }
}
