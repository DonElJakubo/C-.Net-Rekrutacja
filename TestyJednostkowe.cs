[Test]
public async Task GetTagsAsync_ShouldReturnTags()
{
    // Arrange
    var request = new GetTagsRequest
    {
        Sort = "name",
        PageNumber = 1,
        PageSize = 10
    };

    // Act
    var tags = await _tagService.GetTagsAsync(request.Sort, request.PageNumber, request.PageSize);

    // Assert
    Assert.That(tags, Is.Not.Empty);
}

[Test]
public async Task GetTagByIdAsync_ShouldReturnTag()
{
    // Arrange
    var id = 123;

    // Act
    var tag = await _tagService.GetTagByIdAsync(id);

    // Assert
    Assert.That(tag, Is.Not.Null);
}
