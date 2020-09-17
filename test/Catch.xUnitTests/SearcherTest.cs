using FluentAssertions;
using System;
using Xunit;

namespace Catch.xUnitTests
{
    public class SearcherTest
    {
        [Fact]
        public void CtorTest()
        {
            var searcher = new Searcher();
            searcher.Should().NotBeNull();
        }
    }
}
