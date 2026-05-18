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

		public void UploadArticle(string title, Stream fileStream, string fileName)
		{
			string dirPath = Path.Combine(_webRootPath, "articles");
			Directory.CreateDirectory(dirPath);

			string filePath = Path.Combine(dirPath, title + "_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + Path.GetExtension(fileName));

			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				fileStream.CopyTo(fs);
			}

			//_articleRepository.AddArticle(article);
		}
	}
}
