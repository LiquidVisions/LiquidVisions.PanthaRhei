﻿using System;
using System.Linq;
using LiquidVisions.PanthaRhei.Generator.Domain;
using LiquidVisions.PanthaRhei.Generator.Domain.Entities;
using LiquidVisions.PanthaRhei.Generator.Domain.Gateways;
using LiquidVisions.PanthaRhei.Generator.Domain.Interactors.Dependencies;

namespace LiquidVisions.PanthaRhei.Generator.Application.Interactors.Seeders
{

    internal class RelationshipSeederInteractor : ISeederInteractor<App>
    {
        private readonly IGenericGateway<Relationship> gateway;
        private readonly Parameters parameters;

        public RelationshipSeederInteractor(IDependencyFactoryInteractor dependencyFactory)
        {
            gateway = dependencyFactory.Get<IGenericGateway<Relationship>>();
            parameters = dependencyFactory.Get<Parameters>();
        }

        public int SeedOrder => 7;

        public int ResetOrder => 7;

        public void Seed(App app)
        {
        }

        public void Reset() => gateway.DeleteAll();
    }
}
