using Bogus;
using System.Security.Cryptography;

namespace TDDBank.Tests
{
    public class CustomerManagementTests
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void SaveToFile_2_test_customers()
        {
            var testFileName = "test.json";
            var c1 = new Customer()
            {
                Name = "Fred Feuerstein",
                Address = "Steinhausen",
                Birthdate = DateTime.Now.AddYears(-50)
            };
            var c2 = new Customer()
            {
                Name = "Wilma Feuerstein",
                Address = "Steinhausen",
                Birthdate = DateTime.Now.AddYears(-40)
            };
            var cm = new CustomerManagement();

            cm.SaveToFile(testFileName, new[] { c1, c2 });

            Assert.True(File.Exists(testFileName));

            File.Delete(testFileName);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void SaveToFile_2_test_customers_with_Bogus()
        {
            var testFileName = "test.json";
            var cm = new CustomerManagement();

            var faker = new Faker<Customer>("de");
            faker.UseSeed(7);
            faker.RuleFor(x => x.Name, x => x.Name.FullName());
            faker.RuleFor(x => x.Address, x => x.Address.FullAddress());
            faker.RuleFor(x => x.Birthdate, x => x.Date.Past(40));

            cm.SaveToFile(testFileName, faker.Generate(100));

            Assert.True(File.Exists(testFileName));

            //File.Delete(testFileName);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetFromFile_TestCustomers()
        {
            var testFileName = "TestCustomers.json";
            var cm = new CustomerManagement();

            var result = cm.GetFromFile(testFileName);

            Assert.Equal(2, result.Count());
        }

    }
}


