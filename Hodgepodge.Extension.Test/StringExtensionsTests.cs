using System;
using Xunit;

namespace Hodgepodge.Extension.Test
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("www.d1.tld/s1", "d1.tld/s1")]
        [InlineData("x.www.d1.tld/s1", "x.www.d1.tld/s1")]
        public void Test1(string input, string output)
        {
            var result = input.TrimSubdomain();

            Assert.Equal(output, result);
        }

        [Theory]
        [InlineData("d1.tld", "d1.tld")]
        [InlineData("d1.tld/s1", "/s1")]
        [InlineData("d1.tld/s1/", "/s1/")]
        [InlineData("d1.tld/s1/s2", "/s1/s2")]
        public void Test2(string input, string output)
        {
            var result = input.AbsolutePath();

            Assert.Equal(output, result);
        }

        [Theory]
        [InlineData("/", "/")]
        [InlineData("d1.tld", "d1.tld")]
        [InlineData("d1.tld/s1/", "d1.tld/s1")]
        public void Test3(string input, string output)
        {
            var result = input.TrimTrailingSlash();

            Assert.Equal(output, result);
        }
    }
}
