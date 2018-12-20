using System.Threading.Tasks;
using Xunit;

namespace UsersApp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(2, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        [Fact]
        public async Task Serious_Test_One_ShouldBeSuccessful()
        {

        }
    }
}
