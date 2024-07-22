# Action Hero

Get things done.  Action hero style.


## Thoughts on Commands

- Commands are assigned to Controllers
- HomeController catches commands not handled by active controller
- Controller base provides common commands like
    - close 
    - help
    - exit?
- Commands can be done and undone

Do we want HomeController?  I.e. is the HomeView always the default?




```cs
Command.Build((host, app) => 
{
    host.Controllers.Open(Controllers.TaskList);
    host.RegisterController(Controllers.TaskList);
})


Command.Build((host, app) => 
{
    host.RegisterController(host.GetRequiredService<SomeController>);
})



XController : Controller
{
    void override OnInput(Key key)
    {
        if (key == 'd')
        {
            Presenters.Open(Presenter.DetailPresenter);
            _presenters.Open(Presenter.DetailPresenter);

            Host.Presenters.Open(Presenter.DetailPresenter);
            _host.Presenters.Open(Presenter.DetailPresenter);
            #

            Host.OpenPresenter<AppPresenter>();


            void OpenPresenter<T> where T : IPresenter
            {
                var p = _serviceProvider.GetRequiredService<T>();
                /* ... */
            }
        }
    }
}
```
 