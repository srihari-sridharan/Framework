// <copyright file="StringUtilsTest.cs" company="">
//
// </copyright>

using Framework.Utils;
using Xunit;

namespace Framework.Test.Utils
{
    public class StringUtilsTest
    {
        [Fact]
        public void Format()
        {
            var format = "NULL:{0}";
            var formatted = format.FormatIt("Test");
            Assert.Equal("NULL:Test", formatted);
        }

        [Fact]
        public void IsEmpty()
        {
            var input = string.Empty;
            Assert.Equal(true, input.IsNullOrWhiteSpace());
        }

        [Fact]
        public void IsNull()
        {
            string input = null;
            Assert.Equal(true, input.IsNullOrWhiteSpace());
        }

        [Fact]
        public void IsValidString()
        {
            var input = "Sample";
            Assert.Equal(false, input.IsNullOrWhiteSpace());
        }

        [Fact]
        public void IsWhitespace()
        {
            var input = "      ";
            Assert.Equal(true, input.IsNullOrWhiteSpace());
        }
    }
}