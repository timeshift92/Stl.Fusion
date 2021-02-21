using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Stl.Internal;
using Stl.Testing;
using Xunit;

namespace Stl.Tests.Internal
{
    public class ObjectHolderTest
    {
        [Fact]
        public void BasicTest()
        {
            var holder = new ObjectHolder();
            var count = TestRunnerInfo.IsBuildAgent() ? 100 : 1000;
            var objects = Enumerable.Range(0, count).Select(i => i.ToString()).ToArray();

            var holds = new HashSet<IDisposable>();
            for (var i = 0; i < objects.Length; i++)
                holds.Add(holder.Hold(objects[i]));
            holder.IsEmpty.Should().BeFalse();

            // HashSet randomizes the order of disposal
            foreach (var hold in holds)
                hold.Dispose();
            holder.IsEmpty.Should().BeTrue();
        }
    }
}
