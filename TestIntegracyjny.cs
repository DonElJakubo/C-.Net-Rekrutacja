[Test]
public async Task GetTags_ShouldReturnSortedTags()
{
    // Arrange
    var client = new HttpClient();

    // Act
    var response = await client.GetAsync("http://localhost:8000/api/tags?sort=name");

    // Assert
    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    var tags = await response.Content.ReadAsAsync<IEnumerable<Tag>>();
    Assert.That(tags, Is.SortedBy(t => t.Name));
}
