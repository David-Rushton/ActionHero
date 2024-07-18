# Action Hero

Get things done.  Action hero style.


## Thoughts

`ControllerViewHost`
    -> `InputDispatchService`
    -> `ViewRenderingService`

Controllers register with the `cvh`.  

`cvh` -> provides `AppController` with first chance to process input.  Then active controller.

Controllers update views and models.  Controllers mark views as dirty when a refresh is required.

`vds` refreshes the screen whenever a view is dirty or the console has been resized.
