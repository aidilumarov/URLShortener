using URLShortener.Lib;
using Xunit;

namespace URLShortener.Tests
{
    public class LinkShortenerTests
    {
        [Theory]
        [InlineData('c', 3, 'a', 'b', 'd', 'c')]
        [InlineData('a', 0, 'a', 'b', 'd', 'c')]
        [InlineData('b', 1, 'a', 'b', 'd', 'c')]
        [InlineData('d', 2, 'a', 'b', 'd', 'c')]
        public void TestCharArrayIndexOf_CharExists_ReturnsCorrectIndex
            (char searched, int expected, params char[] arr)
        {
            var actual = arr.IndexOf(searched);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData('v', 'a', 'b', 'd', 'c')]
        [InlineData('x', 'a', 'b', 'd', 'c')]
        [InlineData('!', 'a', 'b', 'd', 'c')]
        [InlineData('t', 'a', 'b', 'd', 'c')]
        [InlineData('2')]
        public void TestCharArrayIndexOf_CharDoesNotExist_ReturnsNegativeOne
            (char searched, params char[] arr)
        {
            var actual = arr.IndexOf(searched);
            Assert.Equal(-1, actual);
        }
    }
}