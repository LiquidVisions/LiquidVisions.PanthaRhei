using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LiquidVisions.PanthaRhei.Generator.Domain.Dependencies;
using LiquidVisions.PanthaRhei.Generator.Domain.GeneratorUseCases.Handlers;
using LiquidVisions.PanthaRhei.Generator.Domain.Models;

namespace LiquidVisions.PanthaRhei.Expanders.MetaCircular.Handlers
{
    public class Seed : AbstractHandler<MetaCircularExpander>
    {
        public Seed(MetaCircularExpander expander, IDependencyResolver dependencyResolver)
            : base(expander, dependencyResolver)
        {

        }

        public override void Execute()
        {
            var expanderTypes = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblies()
                .Where(x => x.Name.Contains("Expanders"));

            string ns = typeof(App).Namespace;
            Type[] allTypes = AllTypes(ns);

            foreach (Type type in allTypes)
            {
                AddEntity(type);
            }
        }

        public void AddEntity(Type type)
        {
            Entity entity = new Entity
            {
                Id = Guid.NewGuid(),
                Name = type.Name,
            };

            entity.Fields = AddFields(type, entity);
        }

        private List<Field> AddFields(Type type, Entity parent)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(propertyInfo => CreateField(propertyInfo, parent))
                .ToList();
        }

        private Field CreateField(PropertyInfo propertyInfo, Entity parent)
        {
            Field field = new()
            {
                Id = Guid.NewGuid(),
                Name = propertyInfo.Name,
                Entity = parent,
            };

            field.Entity = parent;
            return field;
        }

        private Type[] AllTypes(string ns)
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Namespace == ns)
                .ToArray();
        }

    }
}
