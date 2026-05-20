using System.Security.Claims;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.BLL.Services
{
	public class ArticleService
	{
		readonly private IArticleRepository _articleRepository;

		private readonly string _webRootPath;
		public ArticleService(string webRootPath, IArticleRepository articleRepository)
		{
			_webRootPath = webRootPath;
			_articleRepository = articleRepository;
		}

		public bool UploadArticle(string title, Stream fileStream, string fileName, Guid userId)
		{
			string dirPath = string.Empty;
			string filePath = string.Empty;
			try
			{
				dirPath = Path.Combine(_webRootPath, "articles");
				Directory.CreateDirectory(dirPath);

				string dateFormat = "dd-MM-yyyy_HH-mm-ss";
				string date =  DateTime.Now.ToString(dateFormat);

				filePath = Path.Combine(dirPath, title + "_" + date + Path.GetExtension(fileName));

				using (FileStream fs = new FileStream(filePath, FileMode.Create))
				{
					fileStream.CopyTo(fs);
				}

				Article uploadedArticle = new Article(Guid.NewGuid(), userId, title, filePath, 0, DateTime.ParseExact(date, dateFormat, System.Globalization.CultureInfo.InvariantCulture));

				_articleRepository.AddArticle(uploadedArticle);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to upload article in. Exception: {ex}, directory path = {dirPath}, file path = {filePath}");
				return false;
			}
		}
	}
}
