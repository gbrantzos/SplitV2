using System.Linq;
using Split.Application.Base;
using Xunit;

namespace Split.Application.Tests.Base
{
    public class CommandsTests
    {
        [Fact]
        public void Requests_Should_Override_ToString()
        {
            var asm = typeof(Result).Assembly;
            var allCommands = asm
                .GetTypes()
                .Where(t =>
                {
                    var baseType = t.BaseType;
                    return !t.IsAbstract &&
                           !t.IsInterface &&
                           (baseType == typeof(BaseRequest) || (baseType != null &&
                                                                baseType.IsGenericType &&
                                                                baseType.GetGenericTypeDefinition() ==
                                                                typeof(BaseRequest<>)));
                })
                .ToList();

            foreach (var type in allCommands)
            {
                var isOverriden = type.GetMethod("ToString")?.DeclaringType == type;
                Assert.True(isOverriden, $"{type.Name} should override ToString() method.");
            }
        }
    }
}