(function () {
    'use strict';

var ENTER_KEY = 13;

var ViewModel = function ToDoViewModel(toDoItems) {
    var self = this;
    // store the new todo value being entered
    this.current = ko.observable();

    this.toDoItems = ko.observableArray(toDoItems.map(function (todo) {
        return new ToDoItem(todo.title, todo.completed);
    }));

    this.addToDoItem = function () {
        var current = this.current().trim();
        if (current) {
            this.toDoItems.push(new ToDoItem(current, false));
            this.current('');
        }
    };

    this.removeToDoItem = function (item) {
        self.toDoItems.remove(item);
    };
};

function keyhandlerBindingFactory(keyCode) {
    return {
        init: function (element, valueAccessor, allBindingsAccessor, data, bindingContext) {
            var wrappedHandler, newValueAccessor;

            // wrap the handler with a check for the enter key
            wrappedHandler = function (data, event) {
                if (event.keyCode === keyCode) {
                    valueAccessor().call(this, data, event);
                }
            };

            // create a valueAccessor with the options that we would want to pass to the event binding
            newValueAccessor = function () {
                return {
                    keyup: wrappedHandler
                };
            };

            // call the real event binding's init function
            ko.bindingHandlers.event.init(element, newValueAccessor, allBindingsAccessor, data, bindingContext);
        }
    };
}

// represent a single todo item
var ToDoItem = function (title, completed) {
    this.title = ko.observable(title);
    this.completed = ko.observable(completed);
};
// check local storage for todos
var toDoItems = ko.utils.parseJson(localStorage.getItem('todos-knockoutjs'));
var viewModel = new ViewModel(toDoItems || []);
// a custom binding to handle the enter key
ko.bindingHandlers.enterKey = keyhandlerBindingFactory(ENTER_KEY);

ko.applyBindings(viewModel);
}());