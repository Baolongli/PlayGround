using System;
using System.Linq;
using NUnit.Framework;
using PlayGround.Pattern.Services;

namespace PlayGround.Pattern.Tests.Services
{
    public class FindPatternServiceTests
    {
        [Test]
        public void FindPatterns_NotAllowOverlapping()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=", 3);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "zf3")?.Occurrence, Is.EqualTo(3));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "abx")?.Occurrence, Is.EqualTo(2));
        }

        [Test]
        public void FindPatterns_NotAllowOverlapping_1Overlapping_1Founded()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("abcdeabccde", 3);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "abc")?.Occurrence, Is.EqualTo(2));
        }

        [Test]
        public void FindPatterns_NotAllowOverlapping_1Overlapping_1Founded_2()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("abcdecdeabc", 3);
            foreach (var pattern in result)
            {
                Console.WriteLine($"Patterns: {pattern.Pattern} occur {pattern.Occurrence} times.");
            }
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "cde")?.Occurrence, Is.EqualTo(2));
        }

        [Test]
        public void FindPatterns_NotAllowOverlapping_0Overlapping_2Founded()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("abcedcabcik,edcabc", 3);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "abc")?.Occurrence, Is.EqualTo(3));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "edc")?.Occurrence, Is.EqualTo(2));
        }

        [Test]
        public void FindPatterns_NotAllowOverlapping_0Founded()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("asdfghjkl", 3);
            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void FindPatterns_NotAllowOverlapping_0Founded_PatternLenghtTooBig()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("asdfghjkl", 7);
            Assert.That(result.Length, Is.EqualTo(0));
        }

        [Test]
        public void FindPatterns_AllowOverlapping_1Overlapping_2Founded()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("abcdeabccde", 3, true);
            Assert.That(result.Length, Is.EqualTo(2));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "abc")?.Occurrence, Is.EqualTo(2));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "cde")?.Occurrence, Is.EqualTo(2));
        }

        [Test]
        public void FindPatterns_AllowOverlapping_1Overlapping_3Founded()
        {
            var service = new FindPatternService();
            var result = service.FindPatterns("zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=", 3, true);
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "zf3")?.Occurrence, Is.EqualTo(3));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "abx")?.Occurrence, Is.EqualTo(2));
            Assert.That(result.FirstOrDefault(x => x.Pattern == "bxc")?.Occurrence, Is.EqualTo(2));

        }
    }
}