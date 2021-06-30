using Xunit;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Services;
using Simbirsoft_Weather.Models.Enums;

namespace Simbirsoft_Weather.Tests.Services
{
    public class RecPatternWritterTests
    {
        private readonly IRecPatternWritter _recPatternWritter;

        public RecPatternWritterTests()
        {
            _recPatternWritter = new RecPatternWritter();
        }

        [Fact]
        public void WriteRec_ActionExecutes_ReturnsNotNullString()
        {
            var clothes = new Clothes() { Name = "-----" };
            var forWhom = ForWhom.ForAll;

            var result = _recPatternWritter.WriteRec(clothes, forWhom);

            Assert.IsType<string>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void WriteRec_ActionExecutes_ReturnsNotEmptyString()
        {
            var clothes = new Clothes() { Name = "-----" };
            var forWhom = ForWhom.ForAll;

            var result = _recPatternWritter.WriteRec(clothes, forWhom);

            Assert.IsType<string>(result);
            Assert.NotEmpty(result);
        }

        [Theory]
        [InlineData("test")]
        public void WriteRec_ActionExecutes_ReturnsStringWithClothesName(string clothesName)
        {
            var clothes = new Clothes() { Name = clothesName };
            var forWhom = ForWhom.ForAll;

            var result = _recPatternWritter.WriteRec(clothes, forWhom);

            Assert.Contains(clothesName, result);
        }
    }
}
