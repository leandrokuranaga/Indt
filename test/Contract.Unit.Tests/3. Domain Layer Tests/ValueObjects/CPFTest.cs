using Contract.Domain.Contract.ValueObjects;

namespace TestProject1._3._Domain_Layer_Tests.ValueObjects
{
    public class CPFTest
    {
        [Fact]
        public void Create_WithValidDigits_ReturnsCpf_AndFormatsOnToString()
        {
            // Arrange
            var digits = "07038612042";

            // Act
            var cpf = CPF.Create(digits);

            // Assert
            Assert.Equal(digits, cpf.Value);
            Assert.True(cpf.IsValid);
            Assert.Equal("070.386.120-42", cpf.ToString());
        }

        [Fact]
        public void Create_WithFormattedInput_StripsNonDigits_AndValidates()
        {
            // Arrange
            var formatted = "070.386.120-42";

            // Act
            var cpf = CPF.Create(formatted);

            // Assert
            Assert.Equal("07038612042", cpf.Value);
            Assert.True(cpf.IsValid);
        }

        [Fact]
        public void Create_Null_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => CPF.Create(null!));
        }

        [Theory]
        [InlineData("12345678900")]
        [InlineData("00000000000")]
        [InlineData("0703861204")] 
        [InlineData("070386120420")]
        public void Create_InvalidInput_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => CPF.Create(input));
        }

        [Fact]
        public void IsValid_UsesRawConstructorValue()
        {
            var valid = new CPF("07038612042");
            var invalid = new CPF("00000000000");

            Assert.True(valid.IsValid);
            Assert.False(invalid.IsValid);
        }

    }
}