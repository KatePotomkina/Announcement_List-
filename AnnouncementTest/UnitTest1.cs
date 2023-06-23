
namespace AnnouncementTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }


    public bool CompareStrings(List<string> info)
    {

        List<string[]> stringArrays = info.Select(str => str.Split(',')).ToList();

        for (int i = 0; i <= stringArrays.Count-3; i++)
        {
            foreach (var word1 in stringArrays[i])
            {
                foreach (var word2 in stringArrays[i + 1])
                {
                    foreach (var word3 in stringArrays[i + 2])
                    {
                        if (string.Equals(word1, word2, StringComparison.OrdinalIgnoreCase) &&
                            string.Equals(word1, word3, StringComparison.OrdinalIgnoreCase) ||
                            (string.Equals(word1, word2, StringComparison.OrdinalIgnoreCase) &&
                             string.Equals(word2, word3, StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }

                }

            }

           
        }
        return false;
    }
    [Test]

    public void TestCompareStrings()
    {
        // Arrange
        List<string> inputList = new List<string> { "hello,world", "example,test", "compare,string" };

        // Act
        bool result = CompareStrings(inputList);

        // Assert
        Assert.IsTrue(result);
    }

}