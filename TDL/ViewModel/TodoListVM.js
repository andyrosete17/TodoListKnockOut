(function () {
    'use strict';

var ENTER_KEY = 13;
var todoItemRead = Object();
var ViewModel = function ToDoViewModel(toDoItems) {
    var self = this;
    // store the new todo value being entered
    this.current = ko.observable();

    //self.toDoItems = ko.observableArray([
    //    new ToDoItem("Watch Person of Interest"),
    //    new ToDoItem("Study for Midterm exam"),
    //    new ToDoItem("Buy groceries for Luis")
    //]);

    this.toDoItems = ko.observableArray(toDoItems.map(function (todo) {
        return new ToDoItem(todo.title, todo.completed, todo.id);
    }));

    this.addToDoItem = function () {
        var current = this.current().trim();
        if (current) {

            //var strMethodUrl = '@Url.Action("CreateNewTodoListItem", "Home")?description=' + current;
            //$.getJSON(strMethodUrl, receieveResponse);

            //$.get("@Url.Action("CreateNewTodoListItem", "Home")", function (response) {
            //    todoItemRead = ko.mapping.fromJS(response);

            //});
            var response = null;
            $.ajax({
                async: true,
                url: "Home/CreateNewTodoListItem?description=" + current,
                cache: false,
                dataType: "json",
                success: function (data) { receiveResponse(data); }
            });

            this.toDoItems.push(new ToDoItem(current));
            this.current('');
        }
    };

    this.removeToDoItem = function (item) {
        self.toDoItems.remove(item);
    };
};
    function receiveResponse(response) {

        if (response !== null) {
            todoItemRead = ko.mapping.fromJS(response);
        }
    }
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
var ToDoItem = function (title, completed, id) {
    this.title = ko.observable(title);
    this.completed = ko.observable(completed);
    this.id = ko.observable(id);
};
// check local storage for todos
var toDoItems = ko.utils.parseJson(localStorage.getItem('todos-knockoutjs'));
var viewModel = new ViewModel(toDoItems || []);
// a custom binding to handle the enter key
ko.bindingHandlers.enterKey = keyhandlerBindingFactory(ENTER_KEY);

ko.applyBindings(viewModel);
}());