﻿using Microsoft.QualityTools.Testing.Fakes;

namespace TddBank.Tests
{
    public class OpeningHoursTest
    {
        [Theory]
        [InlineData(2024, 1, 8, 10, 30, true)]//mo
        [InlineData(2024, 1, 8, 10, 29, false)]//mo
        [InlineData(2024, 1, 8, 10, 31, true)] //mo
        [InlineData(2024, 1, 8, 18, 59, true)] //mo
        [InlineData(2024, 1, 8, 19, 00, false)] //mo
        [InlineData(2024, 1, 9, 12, 00, true)] //di
        [InlineData(2024, 1, 13, 10, 30, true)] //sa
        [InlineData(2024, 1, 13, 14, 0, false)] //sa
        [InlineData(2024, 1, 13, 12, 0, true)] //sa
        [InlineData(2024, 1, 13, 9, 0, false)] //sa
        [InlineData(2024, 1, 14, 12, 0, false)] //so
        public void OpeningHours_IsOpen(int y, int M, int d, int h, int m, bool result)
        {
            var dt = new DateTime(y, M, d, h, m, 0);
            var oh = new OpeningHours();

            Assert.Equal(result, oh.IsOpen(dt));
        }


        [Theory]
        [MemberData(nameof(OpeningHours_List))]
        public void OpeningHours_IsOpen_List_of_DateTimes(DateTime dt, bool expResult)
        {
            var oh = new OpeningHours();

            Assert.Equal(expResult, oh.IsOpen(dt));
        }

        public static IEnumerable<object[]> OpeningHours_List
        {
            get
            {
                yield return new object[] { new DateTime(2024, 1, 8, 10, 30, 0), true };
                yield return new object[] { new DateTime(2024, 1, 8, 10, 29, 0), false };
            }
        }


        [Fact]
        public void IsWeekendTests()
        {
            using (ShimsContext.Create())
            {
                var oh = new OpeningHours();

                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 1);
                Assert.False(oh.IsWeekend()); //mo                            
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 2);
                Assert.False(oh.IsWeekend()); //di                            
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 3);
                Assert.False(oh.IsWeekend()); //mi                            
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 4);
                Assert.False(oh.IsWeekend()); //do                            
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 5);
                Assert.False(oh.IsWeekend()); //fr                            
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 6);
                Assert.True(oh.IsWeekend()); //sa                             
                System.Fakes.ShimDateTime.NowGet = () => new DateTime(2024, 1, 7);
                Assert.True(oh.IsWeekend()); //so

            }
        }
    }
}
