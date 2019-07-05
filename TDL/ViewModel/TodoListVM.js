var viewModel;
var ENTER_KEY = 13;
var result;
var todoItemsNew;

(function () {
    'use strict';

var ViewModel = function ToDoViewModel(toDoItems) {
    var self = this;
    // store the new todo value being entered
    this.current = ko.observable();

    this.toDoItems = ko.observableArray(toDoItems.map(function (todo) {
        return new ToDoItem(todo.title, todo.completed, todo.id);
    }));

    this.addToDoItem = function () {
        var current = this.current().trim();
        if (current) {
            var todoItemNew = new ToDoItem();

            $.ajax({
                url: "Home/CreateNewTodoListItem?description=" + current,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'JSON',
                success: function (response) {

                    if (response !== null) {
                        todoItemNew = new ToDoItem(response.Description, response.Status, response.Id);
                        viewModel.setValueMethod(todoItemNew);
                    }
                },
                error: function (response) {

                    window.alert(response.responseText);
                }
            });
         }
    };
    
    this.changeStatusToDoItem = function (item) {
        $.ajax({
            url: "Home/ChangeStatusToDoItem?id=" + item.id() + "&status=" + item.completed(),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'JSON',
            success: function (response) {

                if (response !== null) {
                    item.completed(response.Status);
                }
                else {
                    window.alert(response.responseText);
                }
            },
            error: function (response) {

                window.alert(response.responseText);
            }
        });
    }; 

    this.removeToDoItem = function (item) {
        $.ajax({
            url: "Home/RemoveTodoListItem?id=" + item.id(),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'JSON',
            success: function (response) {

                if (response !== null && response === true) { 
                    viewModel.removeValueMethod(item);
                }
                else {
                    window.alert(response.responseText);
                }
            },
            error: function (response) {

                window.alert(response.responseText);
            }
        });
    };    

    this.setValueMethod = function (todoItemNew) {
        this.current('');
        this.toDoItems.push(todoItemNew);
    };

    this.removeValueMethod = function (item) {
        this.toDoItems.remove(item);
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


// check local storage for todos
var toDoItems = ko.utils.parseJson(localStorage.getItem('todos-knockoutjs'));
viewModel = new ViewModel(toDoItems || []);
// a custom binding to handle the enter key
ko.bindingHandlers.enterKey = keyhandlerBindingFactory(ENTER_KEY);

ko.applyBindings(viewModel);
}());


GetAllToDoItem = function () {
    $.ajax({
        url: "Home/GetAllToDoItem",
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'JSON',
        success: function (response) {
            if (response !== null) {
                response.forEach(function (item, index, array) {
                    todoItemNew = new ToDoItem(item.Description,
                        item.Status,
                        item.Id);
                    viewModel.toDoItems.push(todoItemNew);
                });
                  
            }            
        },
        error: function (response) {

            window.alert(response.responseText);
        }
    });
};

function Init() {
    this.todoItemsNew = [];
    GetAllToDoItem();
    
}

// represent a single todo item
var ToDoItem = function (title, completed, id) {
    this.title = ko.observable(title);
    this.completed = ko.observable(completed);
    this.id = ko.observable(id);
};