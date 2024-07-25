using System.Runtime.InteropServices;

namespace Dr.ActionHero.Model;

public readonly record struct Article(
    Guid Id,
    Guid eTag,
    DateTime Created,
    DateTime LastUpdated,
    string Title,
    string Content);

public class Articles(ArticleRepository repository)
{
    public Article Create(string title, string content)
    {
        if (title.Contains('\n') || title.Contains('\r') || title.Contains('\t'))
            throw new ArgumentOutOfRangeException("Cannot create article.  Title contains invalid characters.  Please remove all line breaks and tabs.");

        if (title.Trim().Length == 0)
            throw new ArgumentOutOfRangeException("Cannot create article.  A title is required.");

        var article = new Article(
            Id: new(),
            eTag: new(),
            Created: DateTime.UtcNow,
            LastUpdated: DateTime.UtcNow,
            title.Trim(),
            content.Trim());

        repository.Add(article);

        return article;
    }

    public Article Update(Article article)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Article> Get()
    {
        throw new NotImplementedException();
    }
}
