
```mermaid
classDiagram
class NoDIGreeter {
    -NoDIGreetingWordsCreator _creator
    +NoDIGreeter()
    +Greeting()
}
class NoDIGreetingWordsCreator {
    +Create()
}
NoDIGreeter --|> NoDIGreetingWordsCreator
```
