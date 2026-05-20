using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.BLL.Services
{
	public class ArticleService
	{
		readonly private IArticleRepository _articleRepository;

		private readonly string _webRootPath;
		public ArticleService(string webRootPath)
		{
			_webRootPath = webRootPath;
		}

		public bool UploadArticle(string title, Stream fileStream, string fileName)
		{
			string dirPath = string.Empty;
			string filePath = string.Empty;
			try
			{
				dirPath = Path.Combine(_webRootPath, "articles");
				Directory.CreateDirectory(dirPath);

				filePath = Path.Combine(dirPath, title + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + Path.GetExtension(fileName));

				using (FileStream fs = new FileStream(filePath, FileMode.Create))
				{
					fileStream.CopyTo(fs);
				}

				//_articleRepository.AddArticle(article);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to store article in wwwwroot. Exception: {ex}, directory path = {dirPath}, file path = {filePath}");
				return false;
			}
		}
	}
}
