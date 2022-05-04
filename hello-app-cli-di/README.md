
```mermaid
classDiagram
class Greeter {
    -GreetingWordsCreator _creator
    +Greeting()
}
class GreetingWordsCreator {
    +Create()
}
Greeter --|> GreetingWordsCreator
```
