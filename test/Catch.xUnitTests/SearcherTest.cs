using FluentAssertions;
using System;
using System.IO.Abstractions;
using FakeItEasy;
using Xunit;

namespace Catch.xUnitTests
{
    public class SearcherTest
    {
        [Fact]
        public void CtorTest()
        {
            var searcher = new Searcher(A.Fake<IFileSystemReader>());
            searcher.Should().NotBeNull();
        }
    }
}
