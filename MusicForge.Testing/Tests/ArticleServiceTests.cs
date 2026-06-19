using Moq;
using MusicForge.BLL.Services;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.Testing.Tests;

public class ArticleServiceTests
{
	private readonly Mock<IArticleRepository> _articleRepositoryMock;
	private readonly ArticleService _articleService;
	private readonly string _webRootPath = "";

	public ArticleServiceTests()
	{
		_articleRepositoryMock = new Mock<IArticleRepository>();

		_webRootPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		Directory.CreateDirectory(_webRootPath);

		_articleService = new ArticleService(_webRootPath, _articleRepositoryMock.Object);
	}

	public void Dispose()
	{
		if (Directory.Exists(_webRootPath))
			Directory.Delete(_webRootPath, recursive: true);
	}

	private static Stream CreateStream(string content = "test content")
	{
		MemoryStream stream = new();
		StreamWriter writer = new(stream);
		writer.Write(content);
		writer.Flush();
		stream.Position = 0;
		return stream;
	}

	// --- Extension validation ---

	[Fact]
	public void UploadArticle_WithTxtExtension_ReturnsTrue()
	{
		// Arrange
		using Stream stream = CreateStream();
		Guid userId = Guid.NewGuid();

		// Act
		bool result = _articleService.UploadArticle("MyArticle", stream, "article.txt", userId);

		// Assert
		Assert.True(result);

		_articleRepositoryMock.Verify(r => r.AddArticle(It.IsAny<Article>()), Times.Once);
	}

	[Fact]
	public void UploadArticle_WithMdExtension_ReturnsTrue()
	{
		// Arrange
		using Stream stream = CreateStream();
		Guid userId = Guid.NewGuid();

		// Act
		bool result = _articleService.UploadArticle("MyArticle", stream, "article.md", userId);

		// Assert
		Assert.True(result);
		_articleRepositoryMock.Verify(r => r.AddArticle(It.IsAny<Article>()), Times.Once);
	}

	[Theory]
	[InlineData("article.pdf")]
	[InlineData("article.docx")]
	[InlineData("article.png")]
	[InlineData("article")]
	[InlineData("article.TXT")]
	public void UploadArticle_WithInvalidExtension_ReturnsFalse(string fileName)
	{
		// Arrange
		using Stream stream = CreateStream();
		Guid userId = Guid.NewGuid();

		// Act
		bool result = _articleService.UploadArticle("MyArticle", stream, fileName, userId);

		// Assert
		Assert.False(result);
		_articleRepositoryMock.Verify(r => r.AddArticle(It.IsAny<Article>()), Times.Never);
	}

	// --- Repository interaction ---

	[Fact]
	public void UploadArticle_ValidFile_AddArticleWithCorrectUserId()
	{
		// Arrange
		using Stream stream = CreateStream();
		Guid userId = Guid.NewGuid();

		// Act
		_articleService.UploadArticle("MyArticle", stream, "article.txt", userId);

		// Assert
		_articleRepositoryMock.Verify(r =>
			r.AddArticle(It.Is<Article>(a => a.UserId == userId)),
			Times.Once);
	}

	[Fact]
	public void UploadArticle_ValidFile_AddArticleWithCorrectTitle()
	{
		// Arrange
		using Stream stream = CreateStream();
		const string title = "Interesting Article";

		// Act
		_articleService.UploadArticle(title, stream, "article.txt", Guid.NewGuid());

		// Assert
		_articleRepositoryMock.Verify(r =>
			r.AddArticle(It.Is<Article>(a => a.Title == title)),
			Times.Once);
	}

	[Fact]
	public void UploadArticle_ValidFile_ArticleHasUniqueId()
	{
		// Arrange
		using Stream stream1 = CreateStream();
		using Stream stream2 = CreateStream();
		Guid? firstId = null;

		_articleRepositoryMock
			.Setup(r => r.AddArticle(It.IsAny<Article>()))
			.Callback<Article>(a => firstId ??= a.Id);

		// Act
		_articleService.UploadArticle("Article1", stream1, "article.txt", Guid.NewGuid());

		List<Guid> capturedIds = [];

		_articleRepositoryMock
			.Setup(r => r.AddArticle(It.IsAny<Article>()))
			.Callback<Article>(a => capturedIds.Add(a.Id));

		_articleService.UploadArticle("Article2", stream2, "article.txt", Guid.NewGuid());

		// Assert
		Assert.NotEqual(firstId, capturedIds.FirstOrDefault());
	}

	// --- Exception / failure handling ---

	[Fact]
	public void UploadArticle_RepositoryThrows_ReturnsFalse()
	{
		// Arrange
		using Stream stream = CreateStream();
		_articleRepositoryMock
			.Setup(r => r.AddArticle(It.IsAny<Article>()))
			.Throws(new Exception("DB error"));

		// Act
		bool result = _articleService.UploadArticle("MyArticle", stream, "article.txt", Guid.NewGuid());

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void UploadArticle_NullStream_ReturnsFalse()
	{
		// Act
		bool result = _articleService.UploadArticle("MyArticle", null!, "article.txt", Guid.NewGuid());

		// Assert
		Assert.False(result);
		_articleRepositoryMock.Verify(r => r.AddArticle(It.IsAny<Article>()), Times.Never);
	}
}
