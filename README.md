# Action Hero

Get things done.  Action hero style.

## Things to Do

- [x] `LogService`
- [x] `Log` -> `LogDatabase` || `LogDb`
- [x] Add `LogPresenter` & `LogView`
- [ ] Add basic commands to `HelpView`
- [ ] Make help and core commands discoverable
- [ ] IView to abstractions
- [ ] IPresenter to abstractions 
- [ ] IPresenter and IInputReceiver to be disconnected
- [ ] Add to-do
- [ ] AQL

## Thoughts on ToDo

Start with local storage.  Should be keyboard friendly.  And searchable.

#to-do #doing #done



fs/
    items/
        1-2-3
        4-5-6
    tags/
        todo
            1-2-3
        doing
            4-5-6
            etc

## Thoughts on Actions

Everything is an article.  An article is a stored as a markdown doc with metadata encoded in Yaml
front matter.  Example:

```markdown
---
title:
tags:
  - to do
  - done
---
More content
```
