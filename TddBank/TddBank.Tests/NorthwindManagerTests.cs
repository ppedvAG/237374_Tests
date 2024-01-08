
using Microsoft.QualityTools.Testing.Fakes;

namespace TddBank.Tests
{
    public class NorthwindManagerTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void CountEmployees_should_be_9()
        {
            var nm = new NorthwindManager();

            var result = nm.CountEmployees();

            Assert.Equal(9, result);
        }

        [Fact]
        public void CountEmployees_should_be_9_faked()
        {
            using (ShimsContext.Create())
            {
                Microsoft.Data.SqlClient.Fakes.ShimSqlConnection.AllInstances.Open = x => { };
                Microsoft.Data.SqlClient.Fakes.ShimSqlCommand.AllInstances.ExecuteScalar = x => 9999; 
                var nm = new NorthwindManager();

                var result = nm.CountEmployees();

                Assert.Equal(9999, result);
            }
        }
    }
}
