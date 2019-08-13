using Framework.Utils;
using Xunit;

namespace Framework.Test.Utils
{
    public class Base36ConverterTests
    {
        [Fact]
        public void DecodeBase36AsInt()
        {
            var input = "z";
            var output = input.DecodeBase36AsInt();
            Assert.Equal(35, output);
        }

        [Fact]
        public void DecodeBase36AsIntWithUpperCase()
        {
            var input = "Z";
            var output = input.DecodeBase36AsInt();
            Assert.Equal(35, output);
        }

        [Fact]
        public void DecodeBase36AsLong()
        {
            var input = "z";
            var output = input.DecodeBase36AsLong();
            Assert.Equal(35, output);
        }

        [Fact]
        public void DecodeBase36AsLongWithUpperCase()
        {
            var input = "Z";
            var output = input.DecodeBase36AsLong();
            Assert.Equal(35, output);
        }

        [Fact]
        public void EncodeBaseIntAsBase36()
        {
            var input = 35;
            var output = input.EncodeIntAsBase36();
            Assert.Equal("z", output);
        }

        [Fact]
        public void EncodeBaseLongAsBase36()
        {
            long input = 35;
            var output = input.EncodeLongAsBase36();
            Assert.Equal("z", output);
        }
    }
}