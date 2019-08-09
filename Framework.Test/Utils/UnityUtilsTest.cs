// <copyright file="UnityUtilsTest.cs" company="">
//
// </copyright>

using System.Linq;
using Framework.Utils;
using Xunit;

namespace Framework.Test.Utils
{
    public class UnityUtilsTest
    {
        [Fact]
        public void TypesInBasePath()
        {
            var typesInBasePath = UnityUtils.FromAssembliesInBasePath();
            var types = typesInBasePath.ToList();
            Assert.NotNull(typesInBasePath);
        }

        [Fact]
        public void TypesInBasePathIncludingSystem()
        {
            var typesInBasePath = UnityUtils.FromAssembliesInBasePath(true, false);
            var types = typesInBasePath.ToList();
            Assert.NotNull(typesInBasePath);
        }

        [Fact]
        public void TypesInBasePathIncludingSystemAndUnity()
        {
            var typesInBasePath = UnityUtils.FromAssembliesInBasePath(true, true);
            var types = typesInBasePath.ToList();
            Assert.NotNull(typesInBasePath);
        }

        [Fact]
        public void TypesInBasePathIncludingUnity()
        {
            var typesInBasePath = UnityUtils.FromAssembliesInBasePath(false, true);
            var types = typesInBasePath.ToList();
            Assert.NotNull(typesInBasePath);
        }
    }
}