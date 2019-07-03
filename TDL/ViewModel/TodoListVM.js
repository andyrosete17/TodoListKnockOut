function ToDo(stuff) {
    this.toDoItem = stuff;
}

function ToDoViewModel() {
    var self = this;

    self.toDoItems = ko.observableArray([
        new ToDo("Watch Person of Interest"),
        new ToDo("Study for Midterm exam"),
        new ToDo("Buy groceries for Luis")
    ]);

    self.addToDoItem = function () {
        self.toDoItems.push(new ToDo($('.txt').val()));
        $('.txt').val('');
    };

    this.removeToDoItem = function (item) {
        self.toDoItems.remove(item);
    };
}

ko.applyBindings(new ToDoViewModel());