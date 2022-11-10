[Fact]
    public void ShouldUpdateIsReserved()
    {
        // Arrange
        var table = A.Dummy<Table>();
        var updatedIsReserved = true;

        // Act
        table.UpdateIsReserved(updatedIsReserved);
        
        // Assert
        table.IsReserved.Should().Be(updatedIsReserved);
    }