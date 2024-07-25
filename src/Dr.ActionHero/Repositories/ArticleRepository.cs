namespace Dr.ActionHero.Repositories;

public class ArticleRepository
{
    public void Add(Article article)
    {
        throw new NotImplementedException();
    }

    public void Update(Article article)
    {
        // lock file
        // get eTag
        // if mismatch
        //  throw
        // else
        //  write
        //  unlock


        throw new NotImplementedException();
    }

    public IEnumerable<Article> Get(IEnumerable<string> tagFilters)
    {
        throw new NotImplementedException();
    }

    public void Delete(Article article)
    {
        throw new NotImplementedException();
    }
}
